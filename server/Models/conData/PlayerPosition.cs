using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("PlayerPositions", Schema = "dbo")]
  public partial class PlayerPosition
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PositionID
    {
      get;
      set;
    }

    public ICollection<Player> Players { get; set; }
    public string PositionName
    {
      get;
      set;
    }
  }
}
