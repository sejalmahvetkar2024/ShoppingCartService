using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductServices.Models;

public partial class ShoppingCartContext : DbContext
{
    public ShoppingCartContext()
    {
    }

    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LIN-5CG44411Q3\\SQLEXPRESS;Initial Catalog=ShoppingCart;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Carts_ProductId");

            entity.HasIndex(e => e.UserId, "IX_Carts_UserId");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Merchants_UserId").IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.MerchantId, "IX_Products_MerchantId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Products).HasForeignKey(d => d.MerchantId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
