using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MainPage.Models;

public partial class GameContext : DbContext
{
    public GameContext()
    {
    }

    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Felhasználó> Felhasználós { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Kép> Képs { get; set; }

    public virtual DbSet<Értékelé> Értékelés { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Game;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Felhasználó>(entity =>
        {
            entity.ToTable("Felhasználó");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Jelszó)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Név)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Rang)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GÉrtékelés).HasColumnName("G_értékelés");
            entity.Property(e => e.Készítő)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Megjelenés)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mód)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mód");
            entity.Property(e => e.Név)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Típus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("típus");
        });

        modelBuilder.Entity<Kép>(entity =>
        {
            entity.ToTable("Kép");

            entity.Property(e => e.KépId)
                .ValueGeneratedNever()
                .HasColumnName("KépID");

            entity.Property(e => e.GameId)
                .HasColumnName("GameID");

            entity.Property(e => e.Utvonal)
                .HasMaxLength(255)
                .HasColumnName("Utvonal");

            entity.HasOne(d => d.Game)
                .WithMany(p => p.Képs)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kép_Game");
        });

        modelBuilder.Entity<Értékelé>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FelhasználóId).HasColumnName("Felhasználó.ID");
            entity.Property(e => e.FelhasználóÉrtékelés).HasColumnName("Felhasználó_értékelés");
            entity.Property(e => e.GameId).HasColumnName("Game.ID");

            entity.HasOne(d => d.Felhasználó).WithMany(p => p.Értékelés)
                .HasForeignKey(d => d.FelhasználóId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Értékelés_Felhasználó");

            entity.HasOne(d => d.Game).WithMany(p => p.Értékelés)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Értékelés_Game");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}