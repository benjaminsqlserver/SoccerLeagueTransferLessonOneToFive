using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SoccerLeagueTransferApp.Data;

namespace SoccerLeagueTransferApp.Pages
{
    public partial class ClubDetailComponent
    {

        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public async Task CheckUserStatus()
        {
            var userState = await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();

            if (userState.User.Claims.Any())
            {


            }
            else
            {
                UriHelper.NavigateTo("login");
            }
        }

    }
}
