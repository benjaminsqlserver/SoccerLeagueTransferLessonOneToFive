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
    public partial class UsersInRolesComponent : ComponentBase
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

        protected RadzenGrid<SoccerLeagueTransferApp.Models.ConData.UsersInRole> grid0;

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.UsersInRole> _getUsersInRolesResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.UsersInRole> getUsersInRolesResult
        {
            get
            {
                return _getUsersInRolesResult;
            }
            set
            {
                if (!object.Equals(_getUsersInRolesResult, value))
                {
                    _getUsersInRolesResult = value;
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
            var conDataGetUsersInRolesResult = await ConData.GetUsersInRoles();
            getUsersInRolesResult = conDataGetUsersInRolesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddUsersInRole>("Add Users In Role", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SoccerLeagueTransferApp.Models.ConData.UsersInRole args)
        {
            var dialogResult = await DialogService.OpenAsync<EditUsersInRole>("Edit Users In Role", new Dictionary<string, object>() { {"ID", args.ID} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var conDataDeleteUsersInRoleResult = await ConData.DeleteUsersInRole(data.ID);
                if (conDataDeleteUsersInRoleResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception conDataDeleteUsersInRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete UsersInRole");
            }
        }
    }
}
