using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LatestECommerce.Models;

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

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0703059259");

            entity.ToTable("Customer");

            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FullName).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber1).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber2).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<LatestECommerce.Models.CustomerModel>? CustomerModel { get; set; }
}
