using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("Users", Schema = "dbo")]
  public partial class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID
    {
      get;
      set;
    }

    public ICollection<UsersInRole> UsersInRoles { get; set; }
    public string Username
    {
      get;
      set;
    }
    public string Password
    {
      get;
      set;
    }
    public int? TeamID
    {
      get;
      set;
    }
    public ClubDetail ClubDetail { get; set; }
  }
}
