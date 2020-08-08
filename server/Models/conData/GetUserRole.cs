using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("GetUserRoles", Schema = "dbo")]
  public partial class GetUserRole
  {
    public string RoleName
    {
      get;
      set;
    }
  }
}
