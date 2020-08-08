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
    public partial class PlayerPositionsComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.PlayerPosition> grid0;

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.PlayerPosition> _getPlayerPositionsResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.PlayerPosition> getPlayerPositionsResult
        {
            get
            {
                return _getPlayerPositionsResult;
            }
            set
            {
                if (!object.Equals(_getPlayerPositionsResult, value))
                {
                    _getPlayerPositionsResult = value;
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
            var conDataGetPlayerPositionsResult = await ConData.GetPlayerPositions();
            getPlayerPositionsResult = conDataGetPlayerPositionsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddPlayerPosition>("Add Player Position", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.PlayerPosition args)
        {
            var dialogResult = await DialogService.OpenAsync<EditPlayerPosition>("Edit Player Position", new Dictionary<string, object>() { {"PositionID", args.PositionID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeletePlayerPositionResult = await ConData.DeletePlayerPosition(data.PositionID);
                if (conDataDeletePlayerPositionResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeletePlayerPositionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete PlayerPosition");
            }
        }
    }
}
