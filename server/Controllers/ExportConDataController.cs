using Microsoft.AspNetCore.Mvc;
using SoccerLeagueTransferApp.Data;

namespace SoccerLeagueTransferApp
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;

        public ExportConDataController(ConDataContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/ConData/clubdetails/csv")]
        public FileStreamResult ExportClubDetailsToCSV()
        {
            return ToCSV(ApplyQuery(context.ClubDetails, Request.Query));
        }

        [HttpGet("/export/ConData/clubdetails/excel")]
        public FileStreamResult ExportClubDetailsToExcel()
        {
            return ToExcel(ApplyQuery(context.ClubDetails, Request.Query));
        }

        [HttpGet("/export/ConData/genders/csv")]
        public FileStreamResult ExportGendersToCSV()
        {
            return ToCSV(ApplyQuery(context.Genders, Request.Query));
        }

        [HttpGet("/export/ConData/genders/excel")]
        public FileStreamResult ExportGendersToExcel()
        {
            return ToExcel(ApplyQuery(context.Genders, Request.Query));
        }

        [HttpGet("/export/ConData/getuserroles/csv")]
        public FileStreamResult ExportGetUserRolesToCSV()
        {
            return ToCSV(ApplyQuery(context.GetUserRoles, Request.Query));
        }

        [HttpGet("/export/ConData/getuserroles/excel")]
        public FileStreamResult ExportGetUserRolesToExcel()
        {
            return ToExcel(ApplyQuery(context.GetUserRoles, Request.Query));
        }

        [HttpGet("/export/ConData/players/csv")]
        public FileStreamResult ExportPlayersToCSV()
        {
            return ToCSV(ApplyQuery(context.Players, Request.Query));
        }

        [HttpGet("/export/ConData/players/excel")]
        public FileStreamResult ExportPlayersToExcel()
        {
            return ToExcel(ApplyQuery(context.Players, Request.Query));
        }

        [HttpGet("/export/ConData/playerpositions/csv")]
        public FileStreamResult ExportPlayerPositionsToCSV()
        {
            return ToCSV(ApplyQuery(context.PlayerPositions, Request.Query));
        }

        [HttpGet("/export/ConData/playerpositions/excel")]
        public FileStreamResult ExportPlayerPositionsToExcel()
        {
            return ToExcel(ApplyQuery(context.PlayerPositions, Request.Query));
        }

        [HttpGet("/export/ConData/playertransfers/csv")]
        public FileStreamResult ExportPlayerTransfersToCSV()
        {
            return ToCSV(ApplyQuery(context.PlayerTransfers, Request.Query));
        }

        [HttpGet("/export/ConData/playertransfers/excel")]
        public FileStreamResult ExportPlayerTransfersToExcel()
        {
            return ToExcel(ApplyQuery(context.PlayerTransfers, Request.Query));
        }

        [HttpGet("/export/ConData/roles/csv")]
        public FileStreamResult ExportRolesToCSV()
        {
            return ToCSV(ApplyQuery(context.Roles, Request.Query));
        }

        [HttpGet("/export/ConData/roles/excel")]
        public FileStreamResult ExportRolesToExcel()
        {
            return ToExcel(ApplyQuery(context.Roles, Request.Query));
        }

        [HttpGet("/export/ConData/transfertypes/csv")]
        public FileStreamResult ExportTransferTypesToCSV()
        {
            return ToCSV(ApplyQuery(context.TransferTypes, Request.Query));
        }

        [HttpGet("/export/ConData/transfertypes/excel")]
        public FileStreamResult ExportTransferTypesToExcel()
        {
            return ToExcel(ApplyQuery(context.TransferTypes, Request.Query));
        }

        [HttpGet("/export/ConData/users/csv")]
        public FileStreamResult ExportUsersToCSV()
        {
            return ToCSV(ApplyQuery(context.Users, Request.Query));
        }

        [HttpGet("/export/ConData/users/excel")]
        public FileStreamResult ExportUsersToExcel()
        {
            return ToExcel(ApplyQuery(context.Users, Request.Query));
        }

        [HttpGet("/export/ConData/usersinroles/csv")]
        public FileStreamResult ExportUsersInRolesToCSV()
        {
            return ToCSV(ApplyQuery(context.UsersInRoles, Request.Query));
        }

        [HttpGet("/export/ConData/usersinroles/excel")]
        public FileStreamResult ExportUsersInRolesToExcel()
        {
            return ToExcel(ApplyQuery(context.UsersInRoles, Request.Query));
        }
    }
}
