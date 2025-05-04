using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Models;

public partial class ShoppingCartContext : DbContext
{
    public ShoppingCartContext()
    {
    }

    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LIN-5CG44411Q3\\SQLEXPRESS;Initial Catalog=ShoppingCart;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Addresses_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_Items_CartId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.AddressId, "IX_Orders_AddressId");

            entity.HasIndex(e => e.ItemId, "IX_Orders_ItemId");

            entity.HasIndex(e => e.MerchantId, "IX_Orders_MerchantId");

            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.HasIndex(e => e.WalletId, "IX_Orders_WalletId");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders).HasForeignKey(d => d.AddressId);

            entity.HasOne(d => d.Item).WithMany(p => p.Orders).HasForeignKey(d => d.ItemId);

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);

            entity.HasOne(d => d.Wallet).WithMany(p => p.Orders).HasForeignKey(d => d.WalletId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.MerchantId, "IX_Products_MerchantId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Wallets_UserId");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");

            //entity.HasOne(d => d.User).WithMany(p => p.Wallets).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
