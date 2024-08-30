using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaGestionDeTaller.Areas.Main.Models;

namespace SistemaGestionDeTaller.Models;

public partial class DbstContext : DbContext
{
    public DbstContext()
    {
    }

    public DbstContext(DbContextOptions<DbstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Concept> Concepts { get; set; }

    public virtual DbSet<Data> Data { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Cart> Cart { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Concept>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Concept__3214EC07AF62D293");

            entity.ToTable("Concept");

            entity.Property(e => e.NameProduct)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([UnitPrice]*[Units])", false)
                .HasColumnType("decimal(21, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.SaleNumberNavigation).WithMany(p => p.Concepts)
                .HasForeignKey(d => d.SaleNumber)
                .HasConstraintName("FK__Concept__SaleNum__5535A963");
        });

        modelBuilder.Entity<Data>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Data__3214EC071127CDBE");

            entity.Property(e => e.Cp).HasColumnName("CP");
            entity.Property(e => e.Locality)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07FAFB8410");

            entity.ToTable("Product");

            entity.Property(e => e.DescriptionProduct)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InvestmentPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SalePrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sale__3214EC07043A3D12");

            entity.ToTable("Sale");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC07F7CBF7A5");

            entity.ToTable("Service");

            entity.Property(e => e.Brand)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Patent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07317B7020");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E4BAC4C170").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(350)
                .IsUnicode(false);
            entity.Property(e => e.IsOnline).HasDefaultValue(false);
            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart");

            entity.HasOne<Product>()
            .WithMany()
            .HasForeignKey(e => e.Product)
            .HasConstraintName("FK_Cart_Product");

            entity.ToTable("Cart");

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
