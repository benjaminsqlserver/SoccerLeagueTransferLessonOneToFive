using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using System.Security.Claims;

namespace SoccerLeagueTransferApp.Data
{

    //CustomAuthenticationStateProvider class will be implemented as a derived class of AuthenticationStateProvider class
    public class CustomAuthenticationStateProvider:AuthenticationStateProvider
    {
        //private variable declaration

        private ISessionStorageService _sesionStorageService;

        //class constructor

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sesionStorageService = sessionStorageService;
        }

        //method implementing(overriding) abstract method GetAuthenticationStateAsync() inherited from AuthenticationStateProvider class
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //create a new List of Claims Identity

            List<ClaimsIdentity> identities = new List<ClaimsIdentity>();

            //get username from session storage

            String username = await _sesionStorageService.GetItemAsync<string>("Username");

            //get list of current user roles from session storage

            List<String> roles = await _sesionStorageService.GetItemAsync<List<String>>("Roles");

            //check if username is not null or empty

            if(!string.IsNullOrEmpty(username))
            {
                //iterate over role list and create a new identity for current username and each role
                //add newly created identity to identities list

                foreach (String role in roles)
                {
                    var identity = new ClaimsIdentity(new[]
                     {
                    new Claim(ClaimTypes.Name,username),new Claim(ClaimTypes.Role,role)
                     }, "apiauth_type");
                    identities.Add(identity);
                }

            }


            //create a new claims principal from the identities list

            var user = new ClaimsPrincipal(identities);

            //lastly

            return await Task.FromResult(new AuthenticationState(user));


        }

        //method to mark user as authenticated
        //method takes in current username and associated list of roles as input paramters
        public async Task MarkUserAsAuthenticated(String username, List<String> roleNames)
        {
            //create a new List of Claims Identity
            List<ClaimsIdentity> identities = new List<ClaimsIdentity>();



            //iterating over roleNames list
            foreach (String role in roleNames)
            {
                //create a new claimsidentity from username and current role and add it to identities list
                var identity = new ClaimsIdentity(new[]
                 {
                    new Claim(ClaimTypes.Name,username),new Claim(ClaimTypes.Role,role)
                 }, "apiauth_type");
                identities.Add(identity);
            }
            //create a new ClaimsPrincipal using the identities list
            var user = new ClaimsPrincipal(identities);

            //lastly

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        }

        //method to log out current user
        //takes current username as input parameter
        //method name is MarkUserAsLoggedOut
        public void MarkUserAsLoggedOut(string userName)
        {
            //clear all items in session storage

            _sesionStorageService.ClearAsync();

            //create a new claims identity

            var identity = new ClaimsIdentity();

            //create a new claims principal from newly created claims identity

            var user = new ClaimsPrincipal(identity);

            //lastly, notify that authentication state has changed

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));


        }

    }
}
