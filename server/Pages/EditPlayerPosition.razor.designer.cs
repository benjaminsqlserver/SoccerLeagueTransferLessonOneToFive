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
    public partial class EditPlayerPositionComponent : ComponentBase
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

        [Parameter]
        public dynamic PositionID { get; set; }

        SoccerLeagueTransferApp.Models.ConData.PlayerPosition _playerposition;
        protected SoccerLeagueTransferApp.Models.ConData.PlayerPosition playerposition
        {
            get
            {
                return _playerposition;
            }
            set
            {
                if (!object.Equals(_playerposition, value))
                {
                    _playerposition = value;
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
            var conDataGetPlayerPositionByPositionIdResult = await ConData.GetPlayerPositionByPositionId(PositionID);
            playerposition = conDataGetPlayerPositionByPositionIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SoccerLeagueTransferApp.Models.ConData.PlayerPosition args)
        {
            try
            {
                var conDataUpdatePlayerPositionResult = await ConData.UpdatePlayerPosition(PositionID, playerposition);
                DialogService.Close(playerposition);
            }
            catch (System.Exception conDataUpdatePlayerPositionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update PlayerPosition");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
