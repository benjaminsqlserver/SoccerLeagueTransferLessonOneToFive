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
    public partial class EditClubDetailComponent : ComponentBase
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
        public dynamic TeamID { get; set; }

        SoccerLeagueTransferApp.Models.ConData.ClubDetail _clubdetail;
        protected SoccerLeagueTransferApp.Models.ConData.ClubDetail clubdetail
        {
            get
            {
                return _clubdetail;
            }
            set
            {
                if (!object.Equals(_clubdetail, value))
                {
                    _clubdetail = value;
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
            var conDataGetClubDetailByTeamIdResult = await ConData.GetClubDetailByTeamId(TeamID);
            clubdetail = conDataGetClubDetailByTeamIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SoccerLeagueTransferApp.Models.ConData.ClubDetail args)
        {
            try
            {
                var conDataUpdateClubDetailResult = await ConData.UpdateClubDetail(TeamID, clubdetail);
                DialogService.Close(clubdetail);
            }
            catch (System.Exception conDataUpdateClubDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update ClubDetail");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
