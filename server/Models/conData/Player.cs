using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("Players", Schema = "dbo")]
  public partial class Player
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlayerID
    {
      get;
      set;
    }

    public ICollection<PlayerTransfer> PlayerTransfers { get; set; }
    public string FirstName
    {
      get;
      set;
    }
    public string MiddleName
    {
      get;
      set;
    }
    public string Surname
    {
      get;
      set;
    }
    public int GenderID
    {
      get;
      set;
    }
    public Gender Gender { get; set; }
    public int? CurrentClubID
    {
      get;
      set;
    }
    public ClubDetail ClubDetail { get; set; }
    public string PlayerPhoto
    {
      get;
      set;
    }
    public string ContactEmail
    {
      get;
      set;
    }
    public string ContactPhone
    {
      get;
      set;
    }
    public int PositionID
    {
      get;
      set;
    }
    public PlayerPosition PlayerPosition { get; set; }
  }
}
