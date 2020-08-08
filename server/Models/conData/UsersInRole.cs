using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("UsersInRoles", Schema = "dbo")]
  public partial class UsersInRole
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID
    {
      get;
      set;
    }
    public int UserID
    {
      get;
      set;
    }
    public User User { get; set; }
    public int RoleID
    {
      get;
      set;
    }
    public Role Role { get; set; }
  }
}
