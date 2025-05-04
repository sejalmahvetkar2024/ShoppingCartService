using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WalletService.Models;

public partial class ShoppingCartContext : DbContext
{
    public ShoppingCartContext()
    {
    }

    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Statement> Statements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LIN-5CG44411Q3\\SQLEXPRESS;Initial Catalog=ShoppingCart;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Statement>(entity =>
        {
            //entity.HasIndex(e => e.UserId, "IX_Statements_UserId");

            entity.HasIndex(e => e.WalletId, "IX_Statements_WalletId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            //entity.HasOne(d => d.User).WithMany(p => p.Statements).HasForeignKey(d => d.UserId);

            //entity.HasOne(d => d.Wallet).WithMany(p => p.Statements).HasForeignKey(d => d.WalletId);
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Wallets_UserId");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
