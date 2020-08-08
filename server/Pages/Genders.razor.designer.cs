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
    public partial class GendersComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.Gender> grid0;

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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }

        protected async System.Threading.Tasks.Task Load()
        {
            var conDataGetGendersResult = await ConData.GetGenders();
            getGendersResult = conDataGetGendersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddGender>("Add Gender", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.Gender args)
        {
            var dialogResult = await DialogService.OpenAsync<EditGender>("Edit Gender", new Dictionary<string, object>() { {"GenderID", args.GenderID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeleteGenderResult = await ConData.DeleteGender(data.GenderID);
                if (conDataDeleteGenderResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeleteGenderException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Gender");
            }
        }
    }
}
