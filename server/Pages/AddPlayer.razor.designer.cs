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
    public partial class AddPlayerComponent : ComponentBase
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

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.Gender> _getGendersResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.Gender> getGendersResult
        {
            get
            {
                return _getGendersResult;
            }
            set
            {
                if (!object.Equals(_getGendersResult, value))
                {
                    _getGendersResult = value;
                    Reload();
                }
            }
        }

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.ClubDetail> _getClubDetailsResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.ClubDetail> getClubDetailsResult
        {
            get
            {
                return _getClubDetailsResult;
            }
            set
            {
                if (!object.Equals(_getClubDetailsResult, value))
                {
                    _getClubDetailsResult = value;
                    Reload();
                }
            }
        }

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

        SoccerLeagueTransferApp.Models.ConData.Player _player;
        protected SoccerLeagueTransferApp.Models.ConData.Player player
        {
            get
            {
                return _player;
            }
            set
            {
                if (!object.Equals(_player, value))
                {
                    _player = value;
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
            var conDataGetGendersResult = await ConData.GetGenders();
            getGendersResult = conDataGetGendersResult;

            var conDataGetClubDetailsResult = await ConData.GetClubDetails();
            getClubDetailsResult = conDataGetClubDetailsResult;

            var conDataGetPlayerPositionsResult = await ConData.GetPlayerPositions();
            getPlayerPositionsResult = conDataGetPlayerPositionsResult;

            player = new SoccerLeagueTransferApp.Models.ConData.Player(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(SoccerLeagueTransferApp.Models.ConData.Player args)
        {
            try
            {
                var conDataCreatePlayerResult = await ConData.CreatePlayer(player);
                DialogService.Close(player);
            }
            catch (System.Exception conDataCreatePlayerException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Player!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
