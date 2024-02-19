using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LatestECommerce.DbConfig;

public partial class EcommerceContext : DbContext
{
    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerRole> CustomerRoles { get; set; }

    public virtual DbSet<CustomerRoleMapping> CustomerRoleMappings { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC0742FFBA37");

            entity.ToTable("Cart");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07FBAA2773");

            entity.ToTable("Category");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07942CECCE");

            entity.ToTable("Customer");

            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FullName).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber1).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber2).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<CustomerRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07A81705E4");

            entity.ToTable("CustomerRole");

            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<CustomerRoleMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0796A5E8D3");

            entity.ToTable("CustomerRoleMapping");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRoleMappings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerR__Custo__72C60C4A");

            entity.HasOne(d => d.CustomerRole).WithMany(p => p.CustomerRoleMappings)
                .HasForeignKey(d => d.CustomerRoleId)
                .HasConstraintName("FK__CustomerR__Custo__73BA3083");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0722FA08E6");

            entity.ToTable("Product");

            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductV__3214EC07831C2A8F");

            entity.ToTable("ProductVariant");

            entity.Property(e => e.Key).HasMaxLength(1000);
            entity.Property(e => e.Value).HasMaxLength(1000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
