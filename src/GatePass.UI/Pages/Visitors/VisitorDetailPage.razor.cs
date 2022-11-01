using GatePass.Core.PassAggregate;
using GatePass.Core.PassAggregate.Specifications;
using GatePass.Core.VisitorAggregate;
using GatePass.SharedKernel.Interfaces;
using GatePass.UI.Pages.MultiPasses;
using GatePass.UI.Pages.SinglePasses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GatePass.UI.Pages.Visitors
{
    public partial class VisitorDetailPage
    {
        [Parameter] public string Id { get; set; } = string.Empty;

        private MudTable<SinglePass>? singlePassTable;
        private MudTable<MultiplePass>? multiPassTable;
        private Visitor? visitor;
        private string? visitorPhoto;
        private string? editUrl;

        protected override async Task OnParametersSetAsync()
        {
            visitor = await _visitorRepo.GetByIdAsync(Guid.Parse(Id));

            if (visitor == null)
            {
                _snackbar.Add("No visitor found", Severity.Error);
                return;
            }

            visitorPhoto = $"/photos/{visitor.PhotoName}";
            editUrl = $"visitors/upsert/{visitor.Id}";
        }


        async Task<TableData<SinglePass>> ReloadSinglePasses(TableState state)
        {
            var spec = new SinglePassByVisitorIdPaginationSpec(state.Page, state.PageSize, Guid.Parse(Id));
            var singlePasses = await _singlePassRepo.ListAsync(spec);
            var totalCount = await _singlePassRepo.CountAsync();

            return new TableData<SinglePass>() { TotalItems = totalCount, Items = singlePasses };
        }

        async Task<TableData<MultiplePass>> ReloadMultiPasses(TableState state)
        {
            var spec = new MultiPassByVisitorIdPaginationSpec(state.Page, state.PageSize, Guid.Parse(Id));
            var multiPasses = await _multiPassRepo.ListAsync(spec);
            var totalCount = await _multiPassRepo.CountAsync();

            return new TableData<MultiplePass>() { TotalItems = totalCount, Items = multiPasses };
        }

        async Task OnCreateSinglePass()
        {
            var options = new DialogOptions()
            {
                DisableBackdropClick = true,
                Position = DialogPosition.TopCenter,
                FullWidth = true,
                MaxWidth = MaxWidth.Small
            };

            var parameters = new DialogParameters();
            parameters.Add("VisitorId", visitor!.Id);

            var dialog = _dialog.Show<SinglePassFormModal>("Create Single Pass Entry", parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                ReloadSinglePassTable();
            }
        }

        async Task OnCreateMultiPass()
        {
            var options = new DialogOptions()
            {
                DisableBackdropClick = true,
                Position = DialogPosition.TopCenter,
                FullWidth = true,
                MaxWidth = MaxWidth.Small
            };

            var parameters = new DialogParameters();
            parameters.Add("VisitorId", visitor!.Id);

            var dialog = _dialog.Show<MultiPassFormModal>("Create Single Pass Entry", parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                ReloadMultiPassTable();
            }
        }

        void ReloadSinglePassTable()
        {
            singlePassTable?.ReloadServerData();
        }

        void ReloadMultiPassTable()
        {
            multiPassTable?.ReloadServerData();
        }
    }
}
