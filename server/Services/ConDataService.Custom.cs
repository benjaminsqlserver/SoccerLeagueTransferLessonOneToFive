using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SoccerLeagueTransferApp.Models.ConData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerLeagueTransferApp
{
    public partial class ConDataService
    {
        //method to validate user
        //takes in two parameters username and password

        public async Task<User> ValidateUser(string username,string password)
        {
            var user = new User();

            var user1 = context.Users.FirstOrDefault(p => p.Username == username && p.Password == password);

            if(user1!=null)
            {
                user = user1;
            }

            return await Task.FromResult(user);

        }

        public async Task<Role> MuyikGetRoleByName(string roleName)
        {
            var role = new Role();

            try
            {
                var role1 = context.Roles.FirstOrDefault(p => p.RoleName == roleName);
                if(role1!=null && role1.RoleID > 0)
                {
                    role = role1;
                }
            }
            catch(Exception)
            {

            }

            return role;
        }

        public async Task<List<string>> MuyikGetRolesList(int userID)
        {
            var roles = new List<string>();

            var items = context.GetUserRoles.FromSqlRaw("EXEC [dbo].[GetUserRoles] @UserID={0}", userID).ToList();

            if(items.Any())
            {
                foreach(GetUserRole item in items)
                {
                    if(!string.IsNullOrEmpty(item.RoleName))
                    {
                        roles.Add(item.RoleName);
                    }
                }
            }

            return await Task.FromResult(roles);
        }
    }
}
