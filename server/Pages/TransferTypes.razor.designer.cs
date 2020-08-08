using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SoccerLeagueTransferApp.Models.ConData;
using Microsoft.EntityFrameworkCore;

namespace SoccerLeagueTransferApp.Pages
{
    public partial class TransferTypesComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(() => { StateHasChanged(); });
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.TransferType> grid0;

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.TransferType> _getTransferTypesResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.TransferType> getTransferTypesResult
        {
            get
            {
                return _getTransferTypesResult;
            }
            set
            {
                if (!object.Equals(_getTransferTypesResult, value))
                {
                    _getTransferTypesResult = value;
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }

        protected async System.Threading.Tasks.Task Load()
        {
            var conDataGetTransferTypesResult = await ConData.GetTransferTypes();
            getTransferTypesResult = conDataGetTransferTypesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddTransferType>("Add Transfer Type", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.TransferType args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTransferType>("Edit Transfer Type", new Dictionary<string, object>() { {"TransferTypeID", args.TransferTypeID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeleteTransferTypeResult = await ConData.DeleteTransferType(data.TransferTypeID);
                if (conDataDeleteTransferTypeResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeleteTransferTypeException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete TransferType");
            }
        }
    }
}
