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
    public partial class EditRoleComponent : ComponentBase
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
        public dynamic RoleID { get; set; }

        SoccerLeagueTransferApp.Models.ConData.Role _role;
        protected SoccerLeagueTransferApp.Models.ConData.Role role
        {
            get
            {
                return _role;
            }
            set
            {
                if (!object.Equals(_role, value))
                {
                    _role = value;
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
            var conDataGetRoleByRoleIdResult = await ConData.GetRoleByRoleId(RoleID);
            role = conDataGetRoleByRoleIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SoccerLeagueTransferApp.Models.ConData.Role args)
        {
            try
            {
                var conDataUpdateRoleResult = await ConData.UpdateRole(RoleID, role);
                DialogService.Close(role);
            }
            catch (System.Exception conDataUpdateRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Role");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
