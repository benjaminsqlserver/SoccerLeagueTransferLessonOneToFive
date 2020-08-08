using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("PlayerTransfers", Schema = "dbo")]
  public partial class PlayerTransfer
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransferID
    {
      get;
      set;
    }
    public int PlayerID
    {
      get;
      set;
    }
    public Player Player { get; set; }
    public int? OriginatingClubID
    {
      get;
      set;
    }
    public ClubDetail ClubDetail { get; set; }
    public int DestinationClubID
    {
      get;
      set;
    }
    public ClubDetail ClubDetail1 { get; set; }
    public DateTime TransferDate
    {
      get;
      set;
    }
    public int TransferTypeID
    {
      get;
      set;
    }
    public TransferType TransferType { get; set; }
    public decimal? TransferFee
    {
      get;
      set;
    }
    public decimal SignOnFee
    {
      get;
      set;
    }
    public string TransferAgent
    {
      get;
      set;
    }
    public decimal AgentFee
    {
      get;
      set;
    }
  }
}
