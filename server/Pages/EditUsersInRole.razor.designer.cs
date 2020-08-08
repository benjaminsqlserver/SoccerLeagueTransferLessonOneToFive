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
    public partial class EditUsersInRoleComponent : ComponentBase
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
        public dynamic ID { get; set; }

        SoccerLeagueTransferApp.Models.ConData.UsersInRole _usersinrole;
        protected SoccerLeagueTransferApp.Models.ConData.UsersInRole usersinrole
        {
            get
            {
                return _usersinrole;
            }
            set
            {
                if (!object.Equals(_usersinrole, value))
                {
                    _usersinrole = value;
                    Reload();
                }
            }
        }

        IEnumerable<SoccerLeagueTransferApp.Models.ConData.User> _getUsersResult;
        protected IEnumerable<SoccerLeagueTransferApp.Models.ConData.User> getUsersResult
        {
            get
            {
                return _getUsersResult;
            }
            set
            {
                if (!object.Equals(_getUsersResult, value))
                {
                    _getUsersResult = value;
                    Reload();
                }
            }
        }

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
            var conDataGetUsersInRoleByIdResult = await ConData.GetUsersInRoleById(ID);
            usersinrole = conDataGetUsersInRoleByIdResult;

            var conDataGetUsersResult = await ConData.GetUsers();
            getUsersResult = conDataGetUsersResult;

            var conDataGetRolesResult = await ConData.GetRoles();
            getRolesResult = conDataGetRolesResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SoccerLeagueTransferApp.Models.ConData.UsersInRole args)
        {
            try
            {
                var conDataUpdateUsersInRoleResult = await ConData.UpdateUsersInRole(ID, usersinrole);
                DialogService.Close(usersinrole);
            }
            catch (System.Exception conDataUpdateUsersInRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update UsersInRole");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
