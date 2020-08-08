using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using SoccerLeagueTransferApp.Models.ConData;

namespace SoccerLeagueTransferApp.Data
{
  public partial class ConDataContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public ConDataContext(DbContextOptions<ConDataContext> options):base(options)
    {
    }

    public ConDataContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SoccerLeagueTransferApp.Models.ConData.GetUserRole>().HasNoKey();
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.Player>()
              .HasOne(i => i.Gender)
              .WithMany(i => i.Players)
              .HasForeignKey(i => i.GenderID)
              .HasPrincipalKey(i => i.GenderID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.Player>()
              .HasOne(i => i.ClubDetail)
              .WithMany(i => i.Players)
              .HasForeignKey(i => i.CurrentClubID)
              .HasPrincipalKey(i => i.TeamID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.Player>()
              .HasOne(i => i.PlayerPosition)
              .WithMany(i => i.Players)
              .HasForeignKey(i => i.PositionID)
              .HasPrincipalKey(i => i.PositionID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer>()
              .HasOne(i => i.Player)
              .WithMany(i => i.PlayerTransfers)
              .HasForeignKey(i => i.PlayerID)
              .HasPrincipalKey(i => i.PlayerID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer>()
              .HasOne(i => i.ClubDetail)
              .WithMany(i => i.PlayerTransfers)
              .HasForeignKey(i => i.OriginatingClubID)
              .HasPrincipalKey(i => i.TeamID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer>()
              .HasOne(i => i.ClubDetail1)
              .WithMany(i => i.PlayerTransfers1)
              .HasForeignKey(i => i.DestinationClubID)
              .HasPrincipalKey(i => i.TeamID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer>()
              .HasOne(i => i.TransferType)
              .WithMany(i => i.PlayerTransfers)
              .HasForeignKey(i => i.TransferTypeID)
              .HasPrincipalKey(i => i.TransferTypeID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.User>()
              .HasOne(i => i.ClubDetail)
              .WithMany(i => i.Users)
              .HasForeignKey(i => i.TeamID)
              .HasPrincipalKey(i => i.TeamID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.UsersInRole>()
              .HasOne(i => i.User)
              .WithMany(i => i.UsersInRoles)
              .HasForeignKey(i => i.UserID)
              .HasPrincipalKey(i => i.UserID);
        builder.Entity<SoccerLeagueTransferApp.Models.ConData.UsersInRole>()
              .HasOne(i => i.Role)
              .WithMany(i => i.UsersInRoles)
              .HasForeignKey(i => i.RoleID)
              .HasPrincipalKey(i => i.RoleID);


        builder.Entity<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer>()
              .Property(p => p.TransferDate)
              .HasColumnType("datetime");

        this.OnModelBuilding(builder);
    }


    public DbSet<SoccerLeagueTransferApp.Models.ConData.ClubDetail> ClubDetails
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.Gender> Genders
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.GetUserRole> GetUserRoles
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.Player> Players
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.PlayerPosition> PlayerPositions
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.PlayerTransfer> PlayerTransfers
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.Role> Roles
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.TransferType> TransferTypes
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.User> Users
    {
      get;
      set;
    }

    public DbSet<SoccerLeagueTransferApp.Models.ConData.UsersInRole> UsersInRoles
    {
      get;
      set;
    }
  }
}
