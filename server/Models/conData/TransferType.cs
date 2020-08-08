using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerLeagueTransferApp.Models.ConData
{
  [Table("TransferTypes", Schema = "dbo")]
  public partial class TransferType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransferTypeID
    {
      get;
      set;
    }

    public ICollection<PlayerTransfer> PlayerTransfers { get; set; }
    public string TransferTypeName
    {
      get;
      set;
    }
  }
}
