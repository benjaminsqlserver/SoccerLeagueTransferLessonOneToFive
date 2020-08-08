using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("ClubDetails", Schema = "dbo")]
  public partial class ClubDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TeamID
    {
      get;
      set;
    }

    public ICollection<Player> Players { get; set; }
    public ICollection<PlayerTransfer> PlayerTransfers { get; set; }
    public ICollection<PlayerTransfer> PlayerTransfers1 { get; set; }
    public ICollection<User> Users { get; set; }
    public string TeamName
    {
      get;
      set;
    }
    public string HomeGround
    {
      get;
      set;
    }
    public string RegisteredOfficeAddress
    {
      get;
      set;
    }
    public string ContactEmail
    {
      get;
      set;
    }
    public string ContactTelephone
    {
      get;
      set;
    }
    public string ClubLogo
    {
      get;
      set;
    }
  }
}
