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
    public partial class PlayerTransfersComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer> grid0;

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer> _getPlayerTransfersResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer> getPlayerTransfersResult
        {
            get
            {
                return _getPlayerTransfersResult;
            }
            set
            {
                if (!object.Equals(_getPlayerTransfersResult, value))
                {
                    _getPlayerTransfersResult = value;
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
            var conDataGetPlayerTransfersResult = await ConData.GetPlayerTransfers();
            getPlayerTransfersResult = conDataGetPlayerTransfersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddPlayerTransfer>("Add Player Transfer", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.PlayerTransfer args)
        {
            var dialogResult = await DialogService.OpenAsync<EditPlayerTransfer>("Edit Player Transfer", new Dictionary<string, object>() { {"TransferID", args.TransferID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeletePlayerTransferResult = await ConData.DeletePlayerTransfer(data.TransferID);
                if (conDataDeletePlayerTransferResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeletePlayerTransferException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete PlayerTransfer");
            }
        }
    }
}
