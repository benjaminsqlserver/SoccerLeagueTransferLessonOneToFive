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
    public partial class PlayersComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.Player> grid0;

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.Player> _getPlayersResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.Player> getPlayersResult
        {
            get
            {
                return _getPlayersResult;
            }
            set
            {
                if (!object.Equals(_getPlayersResult, value))
                {
                    _getPlayersResult = value;
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
            var conDataGetPlayersResult = await ConData.GetPlayers();
            getPlayersResult = conDataGetPlayersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddPlayer>("Add Player", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.Player args)
        {
            var dialogResult = await DialogService.OpenAsync<EditPlayer>("Edit Player", new Dictionary<string, object>() { {"PlayerID", args.PlayerID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeletePlayerResult = await ConData.DeletePlayer(data.PlayerID);
                if (conDataDeletePlayerResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeletePlayerException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Player");
            }
        }
    }
}
