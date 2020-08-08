using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("Roles", Schema = "dbo")]
  public partial class Role
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleID
    {
      get;
      set;
    }

    public ICollection<UsersInRole> UsersInRoles { get; set; }
    public string RoleName
    {
      get;
      set;
    }
  }
}
