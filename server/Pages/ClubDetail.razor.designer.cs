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
    public partial class ClubDetailComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.ClubDetail> grid0;

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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }

        protected async System.Threading.Tasks.Task Load()
        {
            await CheckUserStatus();

            var conDataGetClubDetailsResult = await ConData.GetClubDetails();
            getClubDetailsResult = conDataGetClubDetailsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddClubDetail>("Add Club Detail", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.ClubDetail args)
        {
            var dialogResult = await DialogService.OpenAsync<EditClubDetail>("Edit Club Detail", new Dictionary<string, object>() { {"TeamID", args.TeamID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeleteClubDetailResult = await ConData.DeleteClubDetail(data.TeamID);
                if (conDataDeleteClubDetailResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeleteClubDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete ClubDetail");
            }
        }
    }
}
