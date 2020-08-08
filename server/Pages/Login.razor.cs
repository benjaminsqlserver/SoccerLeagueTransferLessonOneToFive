using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

using SoccerLeagueTransferApp.Data;
using Microsoft.AspNetCore.Components.Authorization;

namespace SoccerLeagueTransferApp.Pages
{
    public partial class LoginComponent
    {
        //method to clear form values
        private async Task ResetForm()
        {
            user = new Models.ConData.User();
        }

        [Inject]

        ISessionStorageService sessionStorage { get; set; }

        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        //method to login user
        private async Task SignInUser()
        {

            //call ValidateUser method in the service layer
            var myUser = await ConData.ValidateUser(user.Username, user.Password);
            if(myUser.UserID > 0)//checking if user was found in the datastore
            {
                //get user roles list

                List<String> rolesList = await ConData.MuyikGetRolesList(myUser.UserID);

                //check if any role was returned

                if(rolesList.Any())
                {
                    //store role list inside my session manager

                    await sessionStorage.SetItemAsync("Roles", rolesList);

                    //store userID inside session manager

                    await sessionStorage.SetItemAsync("UserID", myUser.UserID);

                    //store username inside session manager

                    await sessionStorage.SetItemAsync("Username", myUser.Username);

                    //store TeamID inside session manager

                    await sessionStorage.SetItemAsync("TeamID", myUser.TeamID);


                    //mark user as authenticated

                    await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(myUser.Username, rolesList);

                    //redirect authenticated user to players list page

                    UriHelper.NavigateTo("players");

                }

                //if no role was returned
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "No Role Assignment", "You are a valid application user,but you have not been assigned any role.Please contact system admin.", 5000);
                }

                
            }

            //if user was not found in datastore
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Login Failure", "Invalid Username or Password.", 5000);
            }


        }
    }
}
