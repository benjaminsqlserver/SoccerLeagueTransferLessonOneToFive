using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using SoccerLeagueTransferApp.Data;

namespace SoccerLeagueTransferApp
{
    public partial class ConDataService
    {
        private readonly ConDataContext context;
        private readonly NavigationManager navigationManager;

        public ConDataService(ConDataContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public async Task ExportClubDetailsToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/clubdetails/excel") : "export/condata/clubdetails/excel", true);
        }

        public async Task ExportClubDetailsToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/clubdetails/csv") : "export/condata/clubdetails/csv", true);
        }

        partial void OnClubDetailsRead(ref IQueryable<Models.ConData.ClubDetail> items);

        public async Task<IQueryable<Models.ConData.ClubDetail>> GetClubDetails(Query query = null)
        {
            var items = context.ClubDetails.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnClubDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnClubDetailCreated(Models.ConData.ClubDetail item);

        public async Task<Models.ConData.ClubDetail> CreateClubDetail(Models.ConData.ClubDetail clubDetail)
        {
            OnClubDetailCreated(clubDetail);

            context.ClubDetails.Add(clubDetail);
            context.SaveChanges();

            return clubDetail;
        }
        public async Task ExportGendersToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/genders/excel") : "export/condata/genders/excel", true);
        }

        public async Task ExportGendersToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/genders/csv") : "export/condata/genders/csv", true);
        }

        partial void OnGendersRead(ref IQueryable<Models.ConData.Gender> items);

        public async Task<IQueryable<Models.ConData.Gender>> GetGenders(Query query = null)
        {
            var items = context.Genders.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnGendersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGenderCreated(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> CreateGender(Models.ConData.Gender gender)
        {
            OnGenderCreated(gender);

            context.Genders.Add(gender);
            context.SaveChanges();

            return gender;
        }

      public async Task<IQueryable<Models.ConData.GetUserRole>> GetGetUserRoles(int? UserID)
      {
          OnGetUserRolesDefaultParams(ref UserID);

          var items = context.GetUserRoles.FromSqlRaw("EXEC [dbo].[GetUserRoles] @UserID={0}", UserID).ToList().AsQueryable();

          OnGetUserRolesInvoke(ref items);

          return await Task.FromResult(items);
      }

      partial void OnGetUserRolesDefaultParams(ref int? UserID);

      partial void OnGetUserRolesInvoke(ref IQueryable<Models.ConData.GetUserRole> items);
        public async Task ExportPlayersToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/players/excel") : "export/condata/players/excel", true);
        }

        public async Task ExportPlayersToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/players/csv") : "export/condata/players/csv", true);
        }

        partial void OnPlayersRead(ref IQueryable<Models.ConData.Player> items);

        public async Task<IQueryable<Models.ConData.Player>> GetPlayers(Query query = null)
        {
            var items = context.Players.AsQueryable();

            items = items.Include(i => i.Gender);

            items = items.Include(i => i.ClubDetail);

            items = items.Include(i => i.PlayerPosition);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPlayersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPlayerCreated(Models.ConData.Player item);

        public async Task<Models.ConData.Player> CreatePlayer(Models.ConData.Player player)
        {
            OnPlayerCreated(player);

            context.Players.Add(player);
            context.SaveChanges();

            return player;
        }
        public async Task ExportPlayerPositionsToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/playerpositions/excel") : "export/condata/playerpositions/excel", true);
        }

        public async Task ExportPlayerPositionsToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/playerpositions/csv") : "export/condata/playerpositions/csv", true);
        }

        partial void OnPlayerPositionsRead(ref IQueryable<Models.ConData.PlayerPosition> items);

        public async Task<IQueryable<Models.ConData.PlayerPosition>> GetPlayerPositions(Query query = null)
        {
            var items = context.PlayerPositions.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPlayerPositionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPlayerPositionCreated(Models.ConData.PlayerPosition item);

        public async Task<Models.ConData.PlayerPosition> CreatePlayerPosition(Models.ConData.PlayerPosition playerPosition)
        {
            OnPlayerPositionCreated(playerPosition);

            context.PlayerPositions.Add(playerPosition);
            context.SaveChanges();

            return playerPosition;
        }
        public async Task ExportPlayerTransfersToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/playertransfers/excel") : "export/condata/playertransfers/excel", true);
        }

        public async Task ExportPlayerTransfersToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/playertransfers/csv") : "export/condata/playertransfers/csv", true);
        }

        partial void OnPlayerTransfersRead(ref IQueryable<Models.ConData.PlayerTransfer> items);

        public async Task<IQueryable<Models.ConData.PlayerTransfer>> GetPlayerTransfers(Query query = null)
        {
            var items = context.PlayerTransfers.AsQueryable();

            items = items.Include(i => i.Player);

            items = items.Include(i => i.ClubDetail);

            items = items.Include(i => i.ClubDetail1);

            items = items.Include(i => i.TransferType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPlayerTransfersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPlayerTransferCreated(Models.ConData.PlayerTransfer item);

        public async Task<Models.ConData.PlayerTransfer> CreatePlayerTransfer(Models.ConData.PlayerTransfer playerTransfer)
        {
            OnPlayerTransferCreated(playerTransfer);

            context.PlayerTransfers.Add(playerTransfer);
            context.SaveChanges();

            return playerTransfer;
        }
        public async Task ExportRolesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/roles/excel") : "export/condata/roles/excel", true);
        }

        public async Task ExportRolesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/roles/csv") : "export/condata/roles/csv", true);
        }

        partial void OnRolesRead(ref IQueryable<Models.ConData.Role> items);

        public async Task<IQueryable<Models.ConData.Role>> GetRoles(Query query = null)
        {
            var items = context.Roles.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoleCreated(Models.ConData.Role item);

        public async Task<Models.ConData.Role> CreateRole(Models.ConData.Role role)
        {
            OnRoleCreated(role);

            context.Roles.Add(role);
            context.SaveChanges();

            return role;
        }
        public async Task ExportTransferTypesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/transfertypes/excel") : "export/condata/transfertypes/excel", true);
        }

        public async Task ExportTransferTypesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/transfertypes/csv") : "export/condata/transfertypes/csv", true);
        }

        partial void OnTransferTypesRead(ref IQueryable<Models.ConData.TransferType> items);

        public async Task<IQueryable<Models.ConData.TransferType>> GetTransferTypes(Query query = null)
        {
            var items = context.TransferTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTransferTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTransferTypeCreated(Models.ConData.TransferType item);

        public async Task<Models.ConData.TransferType> CreateTransferType(Models.ConData.TransferType transferType)
        {
            OnTransferTypeCreated(transferType);

            context.TransferTypes.Add(transferType);
            context.SaveChanges();

            return transferType;
        }
        public async Task ExportUsersToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/users/excel") : "export/condata/users/excel", true);
        }

        public async Task ExportUsersToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/users/csv") : "export/condata/users/csv", true);
        }

        partial void OnUsersRead(ref IQueryable<Models.ConData.User> items);

        public async Task<IQueryable<Models.ConData.User>> GetUsers(Query query = null)
        {
            var items = context.Users.AsQueryable();

            items = items.Include(i => i.ClubDetail);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserCreated(Models.ConData.User item);

        public async Task<Models.ConData.User> CreateUser(Models.ConData.User user)
        {
            OnUserCreated(user);

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
        public async Task ExportUsersInRolesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/usersinroles/excel") : "export/condata/usersinroles/excel", true);
        }

        public async Task ExportUsersInRolesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/condata/usersinroles/csv") : "export/condata/usersinroles/csv", true);
        }

        partial void OnUsersInRolesRead(ref IQueryable<Models.ConData.UsersInRole> items);

        public async Task<IQueryable<Models.ConData.UsersInRole>> GetUsersInRoles(Query query = null)
        {
            var items = context.UsersInRoles.AsQueryable();

            items = items.Include(i => i.User);

            items = items.Include(i => i.Role);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUsersInRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsersInRoleCreated(Models.ConData.UsersInRole item);

        public async Task<Models.ConData.UsersInRole> CreateUsersInRole(Models.ConData.UsersInRole usersInRole)
        {
            OnUsersInRoleCreated(usersInRole);

            context.UsersInRoles.Add(usersInRole);
            context.SaveChanges();

            return usersInRole;
        }

        partial void OnClubDetailDeleted(Models.ConData.ClubDetail item);

        public async Task<Models.ConData.ClubDetail> DeleteClubDetail(int? teamId)
        {
            var item = context.ClubDetails
                              .Where(i => i.TeamID == teamId)
                              .Include(i => i.Players)
                              .Include(i => i.PlayerTransfers)
                              .Include(i => i.PlayerTransfers1)
                              .Include(i => i.Users)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnClubDetailDeleted(item);

            context.ClubDetails.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnClubDetailGet(Models.ConData.ClubDetail item);

        public async Task<Models.ConData.ClubDetail> GetClubDetailByTeamId(int? teamId)
        {
            var items = context.ClubDetails
                              .AsNoTracking()
                              .Where(i => i.TeamID == teamId);

            var item = items.FirstOrDefault();

            OnClubDetailGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.ClubDetail> CancelClubDetailChanges(Models.ConData.ClubDetail item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnClubDetailUpdated(Models.ConData.ClubDetail item);

        public async Task<Models.ConData.ClubDetail> UpdateClubDetail(int? teamId, Models.ConData.ClubDetail clubDetail)
        {
            OnClubDetailUpdated(clubDetail);

            var item = context.ClubDetails
                              .Where(i => i.TeamID == teamId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(clubDetail);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return clubDetail;
        }

        partial void OnGenderDeleted(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> DeleteGender(int? genderId)
        {
            var item = context.Genders
                              .Where(i => i.GenderID == genderId)
                              .Include(i => i.Players)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGenderDeleted(item);

            context.Genders.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnGenderGet(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> GetGenderByGenderId(int? genderId)
        {
            var items = context.Genders
                              .AsNoTracking()
                              .Where(i => i.GenderID == genderId);

            var item = items.FirstOrDefault();

            OnGenderGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.Gender> CancelGenderChanges(Models.ConData.Gender item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnGenderUpdated(Models.ConData.Gender item);

        public async Task<Models.ConData.Gender> UpdateGender(int? genderId, Models.ConData.Gender gender)
        {
            OnGenderUpdated(gender);

            var item = context.Genders
                              .Where(i => i.GenderID == genderId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(gender);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return gender;
        }

        partial void OnPlayerDeleted(Models.ConData.Player item);

        public async Task<Models.ConData.Player> DeletePlayer(int? playerId)
        {
            var item = context.Players
                              .Where(i => i.PlayerID == playerId)
                              .Include(i => i.PlayerTransfers)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPlayerDeleted(item);

            context.Players.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnPlayerGet(Models.ConData.Player item);

        public async Task<Models.ConData.Player> GetPlayerByPlayerId(int? playerId)
        {
            var items = context.Players
                              .AsNoTracking()
                              .Where(i => i.PlayerID == playerId);

            items = items.Include(i => i.Gender);

            items = items.Include(i => i.ClubDetail);

            items = items.Include(i => i.PlayerPosition);

            var item = items.FirstOrDefault();

            OnPlayerGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.Player> CancelPlayerChanges(Models.ConData.Player item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPlayerUpdated(Models.ConData.Player item);

        public async Task<Models.ConData.Player> UpdatePlayer(int? playerId, Models.ConData.Player player)
        {
            OnPlayerUpdated(player);

            var item = context.Players
                              .Where(i => i.PlayerID == playerId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(player);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return player;
        }

        partial void OnPlayerPositionDeleted(Models.ConData.PlayerPosition item);

        public async Task<Models.ConData.PlayerPosition> DeletePlayerPosition(int? positionId)
        {
            var item = context.PlayerPositions
                              .Where(i => i.PositionID == positionId)
                              .Include(i => i.Players)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPlayerPositionDeleted(item);

            context.PlayerPositions.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnPlayerPositionGet(Models.ConData.PlayerPosition item);

        public async Task<Models.ConData.PlayerPosition> GetPlayerPositionByPositionId(int? positionId)
        {
            var items = context.PlayerPositions
                              .AsNoTracking()
                              .Where(i => i.PositionID == positionId);

            var item = items.FirstOrDefault();

            OnPlayerPositionGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.PlayerPosition> CancelPlayerPositionChanges(Models.ConData.PlayerPosition item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPlayerPositionUpdated(Models.ConData.PlayerPosition item);

        public async Task<Models.ConData.PlayerPosition> UpdatePlayerPosition(int? positionId, Models.ConData.PlayerPosition playerPosition)
        {
            OnPlayerPositionUpdated(playerPosition);

            var item = context.PlayerPositions
                              .Where(i => i.PositionID == positionId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(playerPosition);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return playerPosition;
        }

        partial void OnPlayerTransferDeleted(Models.ConData.PlayerTransfer item);

        public async Task<Models.ConData.PlayerTransfer> DeletePlayerTransfer(int? transferId)
        {
            var item = context.PlayerTransfers
                              .Where(i => i.TransferID == transferId)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPlayerTransferDeleted(item);

            context.PlayerTransfers.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnPlayerTransferGet(Models.ConData.PlayerTransfer item);

        public async Task<Models.ConData.PlayerTransfer> GetPlayerTransferByTransferId(int? transferId)
        {
            var items = context.PlayerTransfers
                              .AsNoTracking()
                              .Where(i => i.TransferID == transferId);

            items = items.Include(i => i.Player);

            items = items.Include(i => i.ClubDetail);

            items = items.Include(i => i.ClubDetail1);

            items = items.Include(i => i.TransferType);

            var item = items.FirstOrDefault();

            OnPlayerTransferGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.PlayerTransfer> CancelPlayerTransferChanges(Models.ConData.PlayerTransfer item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnPlayerTransferUpdated(Models.ConData.PlayerTransfer item);

        public async Task<Models.ConData.PlayerTransfer> UpdatePlayerTransfer(int? transferId, Models.ConData.PlayerTransfer playerTransfer)
        {
            OnPlayerTransferUpdated(playerTransfer);

            var item = context.PlayerTransfers
                              .Where(i => i.TransferID == transferId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(playerTransfer);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return playerTransfer;
        }

        partial void OnRoleDeleted(Models.ConData.Role item);

        public async Task<Models.ConData.Role> DeleteRole(int? roleId)
        {
            var item = context.Roles
                              .Where(i => i.RoleID == roleId)
                              .Include(i => i.UsersInRoles)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoleDeleted(item);

            context.Roles.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnRoleGet(Models.ConData.Role item);

        public async Task<Models.ConData.Role> GetRoleByRoleId(int? roleId)
        {
            var items = context.Roles
                              .AsNoTracking()
                              .Where(i => i.RoleID == roleId);

            var item = items.FirstOrDefault();

            OnRoleGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.Role> CancelRoleChanges(Models.ConData.Role item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnRoleUpdated(Models.ConData.Role item);

        public async Task<Models.ConData.Role> UpdateRole(int? roleId, Models.ConData.Role role)
        {
            OnRoleUpdated(role);

            var item = context.Roles
                              .Where(i => i.RoleID == roleId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(role);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return role;
        }

        partial void OnTransferTypeDeleted(Models.ConData.TransferType item);

        public async Task<Models.ConData.TransferType> DeleteTransferType(int? transferTypeId)
        {
            var item = context.TransferTypes
                              .Where(i => i.TransferTypeID == transferTypeId)
                              .Include(i => i.PlayerTransfers)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTransferTypeDeleted(item);

            context.TransferTypes.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnTransferTypeGet(Models.ConData.TransferType item);

        public async Task<Models.ConData.TransferType> GetTransferTypeByTransferTypeId(int? transferTypeId)
        {
            var items = context.TransferTypes
                              .AsNoTracking()
                              .Where(i => i.TransferTypeID == transferTypeId);

            var item = items.FirstOrDefault();

            OnTransferTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.TransferType> CancelTransferTypeChanges(Models.ConData.TransferType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTransferTypeUpdated(Models.ConData.TransferType item);

        public async Task<Models.ConData.TransferType> UpdateTransferType(int? transferTypeId, Models.ConData.TransferType transferType)
        {
            OnTransferTypeUpdated(transferType);

            var item = context.TransferTypes
                              .Where(i => i.TransferTypeID == transferTypeId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(transferType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return transferType;
        }

        partial void OnUserDeleted(Models.ConData.User item);

        public async Task<Models.ConData.User> DeleteUser(int? userId)
        {
            var item = context.Users
                              .Where(i => i.UserID == userId)
                              .Include(i => i.UsersInRoles)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(item);

            context.Users.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnUserGet(Models.ConData.User item);

        public async Task<Models.ConData.User> GetUserByUserId(int? userId)
        {
            var items = context.Users
                              .AsNoTracking()
                              .Where(i => i.UserID == userId);

            items = items.Include(i => i.ClubDetail);

            var item = items.FirstOrDefault();

            OnUserGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.User> CancelUserChanges(Models.ConData.User item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnUserUpdated(Models.ConData.User item);

        public async Task<Models.ConData.User> UpdateUser(int? userId, Models.ConData.User user)
        {
            OnUserUpdated(user);

            var item = context.Users
                              .Where(i => i.UserID == userId)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(user);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return user;
        }

        partial void OnUsersInRoleDeleted(Models.ConData.UsersInRole item);

        public async Task<Models.ConData.UsersInRole> DeleteUsersInRole(int? id)
        {
            var item = context.UsersInRoles
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsersInRoleDeleted(item);

            context.UsersInRoles.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnUsersInRoleGet(Models.ConData.UsersInRole item);

        public async Task<Models.ConData.UsersInRole> GetUsersInRoleById(int? id)
        {
            var items = context.UsersInRoles
                              .AsNoTracking()
                              .Where(i => i.ID == id);

            items = items.Include(i => i.User);

            items = items.Include(i => i.Role);

            var item = items.FirstOrDefault();

            OnUsersInRoleGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.ConData.UsersInRole> CancelUsersInRoleChanges(Models.ConData.UsersInRole item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnUsersInRoleUpdated(Models.ConData.UsersInRole item);

        public async Task<Models.ConData.UsersInRole> UpdateUsersInRole(int? id, Models.ConData.UsersInRole usersInRole)
        {
            OnUsersInRoleUpdated(usersInRole);

            var item = context.UsersInRoles
                              .Where(i => i.ID == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(usersInRole);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return usersInRole;
        }
    }
}
