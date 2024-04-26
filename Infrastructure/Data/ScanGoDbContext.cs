using System;
using System.Collections.Generic;
using api_scango.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace api_scango.Domain.Entities;

public partial class ScanGoDbContext : DbContext
{
    public ScanGoDbContext()
    {
    }

    public ScanGoDbContext(DbContextOptions<ScanGoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administrador { get; set; }

    public virtual DbSet<Carrito> Carrito { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Compra> Compra { get; set; }

    public virtual DbSet<CompraDetalles> CompraDetalles { get; set; }

    public virtual DbSet<Descuento> Descuento { get; set; }

    public virtual DbSet<Establecimiento> Establecimiento { get; set; }

    public virtual DbSet<Inventario> Inventario { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }


    public virtual DbSet<ProductoInventario> ProductoInventario { get; set; }


    public virtual DbSet<ProductosEnCarrito> ProductosEnCarrito { get; set; }

    public virtual DbSet<TipoProducto> TipoProducto { get; set; }
    public virtual DbSet<Venta> Venta { get; set; }
    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:gemDevelopment");

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductoConfiguration());
        modelBuilder.ApplyConfiguration(new EstablecimientoConfiguration());
        modelBuilder.ApplyConfiguration(new ClientetConfiguration());
        modelBuilder.ApplyConfiguration(new CarritoConfiguration());
        modelBuilder.ApplyConfiguration(new ProductoEnCarritoConfiguration());
        // modelBuilder.ApplyConfiguration(new ProductoDescuentoConfiguration());
        modelBuilder.ApplyConfiguration(new CompraConfiguration());
        modelBuilder.ApplyConfiguration(new DetalleCompraConfiguration());
        modelBuilder.ApplyConfiguration(new InventarioConfiguration());
        modelBuilder.ApplyConfiguration(new TipoProductoConfiguration());
        // modelBuilder.ApplyConfiguration(new ProductoTipoPtoductoConfiguration());
        modelBuilder.ApplyConfiguration(new ProductoInventariorConfiguration());
        modelBuilder.ApplyConfiguration(new DescuentoConfiguration());
        modelBuilder.ApplyConfiguration(new AdministradorConfiguration());
        modelBuilder.ApplyConfiguration(new DetalleVentaConfiguration());
        modelBuilder.ApplyConfiguration(new VentaConfiguration());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}