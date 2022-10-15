using Autofac;
using Autofac.Extensions.DependencyInjection;
using GatePass.Core;
using GatePass.Infrastructure;
using GatePass.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using GatePass.UI.Data;
using Serilog;
using Microsoft.EntityFrameworkCore;
using GatePass.UI.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

string connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext(connectionString);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

builder.Services.AddScoped<IGeneratePDFService, GeneratePDFService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        context.Database.EnsureCreated();
        await AppDbContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();