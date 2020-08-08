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
    public partial class AddPlayerTransferComponent : ComponentBase
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

        SoccerLeagueTransferApp.Models.ConData.PlayerTransfer _playertransfer;
        protected SoccerLeagueTransferApp.Models.ConData.PlayerTransfer playertransfer
        {
            get
            {
                return _playertransfer;
            }
            set
            {
                if (!object.Equals(_playertransfer, value))
                {
                    _playertransfer = value;
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

            var conDataGetClubDetailsResult = await ConData.GetClubDetails();
            getClubDetailsResult = conDataGetClubDetailsResult;

            var conDataGetTransferTypesResult = await ConData.GetTransferTypes();
            getTransferTypesResult = conDataGetTransferTypesResult;

            playertransfer = new SoccerLeagueTransferApp.Models.ConData.PlayerTransfer(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(SoccerLeagueTransferApp.Models.ConData.PlayerTransfer args)
        {
            try
            {
                var conDataCreatePlayerTransferResult = await ConData.CreatePlayerTransfer(playertransfer);
                DialogService.Close(playertransfer);
            }
            catch (System.Exception conDataCreatePlayerTransferException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new PlayerTransfer!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
