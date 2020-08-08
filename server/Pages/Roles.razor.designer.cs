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
    public partial class RolesComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.Role> grid0;

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.Role> _getRolesResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.Role> getRolesResult
        {
            get
            {
                return _getRolesResult;
            }
            set
            {
                if (!object.Equals(_getRolesResult, value))
                {
                    _getRolesResult = value;
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
            var conDataGetRolesResult = await ConData.GetRoles();
            getRolesResult = conDataGetRolesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddRole>("Add Role", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.Role args)
        {
            var dialogResult = await DialogService.OpenAsync<EditRole>("Edit Role", new Dictionary<string, object>() { {"RoleID", args.RoleID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeleteRoleResult = await ConData.DeleteRole(data.RoleID);
                if (conDataDeleteRoleResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeleteRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Role");
            }
        }
    }
}
