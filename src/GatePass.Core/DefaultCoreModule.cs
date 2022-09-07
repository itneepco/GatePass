using Autofac;
using GatePass.Core.Interfaces;
using GatePass.Core.Services;

namespace GatePass.Core;
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
