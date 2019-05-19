using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class TreinProjectContext : DbContext
    {
        public TreinProjectContext()
        {
        }

        public TreinProjectContext(DbContextOptions<TreinProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Boeking> Boeking { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Rit> Rit { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<Traject> Traject { get; set; }
        public virtual DbSet<TreinType> TreinType { get; set; }
        public virtual DbSet<VakantieDagen> VakantieDagen { get; set; }
        public virtual DbSet<Zetels> Zetels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQL_VIVES;Database=TreinProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Boeking>(entity =>
            {
                entity.Property(e => e.BoekingId).HasColumnName("BoekingID");

                entity.Property(e => e.BoekingsDatum).HasColumnType("date");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Klasse)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasColumnName("LoginID")
                    .HasMaxLength(450);

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TrajectId).HasColumnName("TrajectID");

                entity.Property(e => e.VertrekDatum).HasColumnType("date");

                entity.Property(e => e.Voornaam)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ZetelId).HasColumnName("ZetelID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Boeking)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Hotel");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Boeking)
                    .HasForeignKey(d => d.LoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users");

                entity.HasOne(d => d.Traject)
                    .WithMany(p => p.Boeking)
                    .HasForeignKey(d => d.TrajectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Traject");

                entity.HasOne(d => d.Zetel)
                    .WithMany(p => p.Boeking)
                    .HasForeignKey(d => d.ZetelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zetels");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.HotelNaam)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Hotel)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Station");
            });

            modelBuilder.Entity<Rit>(entity =>
            {
                entity.Property(e => e.RitId).HasColumnName("RitID");

                entity.Property(e => e.AankomstStationId).HasColumnName("AankomstStationID");

                entity.Property(e => e.AankomstUur).HasColumnType("time(0)");

                entity.Property(e => e.Duur).HasColumnType("time(0)");

                entity.Property(e => e.TreinTypeId).HasColumnName("TreinTypeID");

                entity.Property(e => e.VertrekStationId).HasColumnName("VertrekStationID");

                entity.Property(e => e.VertrekUur).HasColumnType("time(0)");

                entity.HasOne(d => d.AankomstStation)
                    .WithMany(p => p.RitAankomstStation)
                    .HasForeignKey(d => d.AankomstStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AankomstStation");

                entity.HasOne(d => d.TreinType)
                    .WithMany(p => p.Rit)
                    .HasForeignKey(d => d.TreinTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK2_TreinType");

                entity.HasOne(d => d.VertrekStation)
                    .WithMany(p => p.RitVertrekStation)
                    .HasForeignKey(d => d.VertrekStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VertrekStation");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.Property(e => e.StationNaam)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Traject>(entity =>
            {
                entity.Property(e => e.TrajectId).HasColumnName("TrajectID");

                entity.Property(e => e.Rit1Id).HasColumnName("Rit1ID");

                entity.Property(e => e.Rit2Id).HasColumnName("Rit2ID");

                entity.Property(e => e.Rit3Id).HasColumnName("Rit3ID");

                entity.HasOne(d => d.Rit1)
                    .WithMany(p => p.TrajectRit1)
                    .HasForeignKey(d => d.Rit1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rit1");

                entity.HasOne(d => d.Rit2)
                    .WithMany(p => p.TrajectRit2)
                    .HasForeignKey(d => d.Rit2Id)
                    .HasConstraintName("FK_Rit2");

                entity.HasOne(d => d.Rit3)
                    .WithMany(p => p.TrajectRit3)
                    .HasForeignKey(d => d.Rit3Id)
                    .HasConstraintName("FK_Rit3");
            });

            modelBuilder.Entity<TreinType>(entity =>
            {
                entity.Property(e => e.TreinTypeId).HasColumnName("TreinTypeID");
            });

            modelBuilder.Entity<VakantieDagen>(entity =>
            {
                entity.HasKey(e => e.VakantieDag);

                entity.Property(e => e.VakantieDag).HasColumnType("date");
            });

            modelBuilder.Entity<Zetels>(entity =>
            {
                entity.HasKey(e => e.ZetelId);

                entity.Property(e => e.ZetelId).HasColumnName("ZetelID");
            });
        }
    }
}
