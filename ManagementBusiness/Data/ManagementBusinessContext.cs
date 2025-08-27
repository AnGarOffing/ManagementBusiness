using Microsoft.EntityFrameworkCore;
using ManagementBusiness.Models;

namespace ManagementBusiness.Data
{
    public class ManagementBusinessContext : DbContext
    {
        public ManagementBusinessContext(DbContextOptions<ManagementBusinessContext> options)
            : base(options)
        {
        }

        // Entidades principales
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Impuesto> Impuestos { get; set; }
        public DbSet<TipoGasto> TiposGasto { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ReporteMensual> ReportesMensuales { get; set; }

        // Entidades de detalle
        public DbSet<DetalleFactura> DetallesFactura { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones específicas de entidades
            ConfigureCliente(modelBuilder);
            ConfigureProveedor(modelBuilder);
            ConfigureFactura(modelBuilder);
            ConfigurePresupuesto(modelBuilder);
            ConfigurePedido(modelBuilder);
            ConfigureVenta(modelBuilder);
            ConfigureCompra(modelBuilder);
            ConfigureGasto(modelBuilder);
            ConfigureProducto(modelBuilder);
            ConfigureServicio(modelBuilder);
            ConfigureMovimientoInventario(modelBuilder);
            ConfigurePago(modelBuilder);
            ConfigureDetalleFactura(modelBuilder);
            ConfigureDetalleCompra(modelBuilder);
            ConfigureDetallePedido(modelBuilder);
            ConfigureSucursal(modelBuilder);
            ConfigureUsuario(modelBuilder);
            ConfigureReporteMensual(modelBuilder);

            // Datos semilla
            SeedData(modelBuilder);
        }

        private void ConfigureCliente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.RFC).HasMaxLength(13);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.EsActivo).HasDefaultValue(true);
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("GETDATE()");

                // Relaciones
                entity.HasMany(e => e.Facturas)
                      .WithOne(e => e.Cliente)
                      .HasForeignKey(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Presupuestos)
                      .WithOne(e => e.Cliente)
                      .HasForeignKey(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Pedidos)
                      .WithOne(e => e.Cliente)
                      .HasForeignKey(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureProveedor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.RFC).IsRequired().HasMaxLength(13);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CuentaBancaria).HasMaxLength(50);

                // Relaciones
                entity.HasMany(e => e.Compras)
                      .WithOne(e => e.Proveedor)
                      .HasForeignKey(e => e.ProveedorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Gastos)
                      .WithOne(e => e.Proveedor)
                      .HasForeignKey(e => e.ProveedorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureFactura(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.NumeroFactura).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // Índice único para NumeroFactura
                entity.HasIndex(e => e.NumeroFactura).IsUnique();

                // Relaciones
                entity.HasMany(e => e.Detalles)
                      .WithOne(e => e.Factura)
                      .HasForeignKey(e => e.FacturaId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Pagos)
                      .WithOne(e => e.Factura)
                      .HasForeignKey(e => e.FacturaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Venta)
                      .WithOne(e => e.Factura)
                      .HasForeignKey<Venta>(e => e.FacturaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigurePresupuesto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // Relaciones
                entity.HasOne(e => e.Pedido)
                      .WithOne(e => e.Presupuesto)
                      .HasForeignKey<Pedido>(e => e.PresupuestoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigurePedido(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // Relaciones
                entity.HasMany(e => e.Detalles)
                      .WithOne(e => e.Pedido)
                      .HasForeignKey(e => e.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Venta)
                      .WithOne(e => e.Pedido)
                      .HasForeignKey<Venta>(e => e.PedidoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureVenta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // Relaciones
                entity.HasMany(e => e.MovimientosInventario)
                      .WithOne(e => e.Venta)
                      .HasForeignKey(e => e.VentaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureCompra(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.NumeroFactura).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                // Relaciones
                entity.HasMany(e => e.Detalles)
                      .WithOne(e => e.Compra)
                      .HasForeignKey(e => e.CompraId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Pagos)
                      .WithOne(e => e.Compra)
                      .HasForeignKey(e => e.CompraId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureGasto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);

                // Relaciones
                entity.HasMany(e => e.Pagos)
                      .WithOne(e => e.Gasto)
                      .HasForeignKey(e => e.GastoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureProducto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.StockMinimo).HasDefaultValue(5);

                // Índice único para SKU
                entity.HasIndex(e => e.SKU).IsUnique();

                // Relaciones
                entity.HasMany(e => e.DetallesFactura)
                      .WithOne(e => e.Producto)
                      .HasForeignKey(e => e.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DetallesCompra)
                      .WithOne(e => e.Producto)
                      .HasForeignKey(e => e.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DetallesPedido)
                      .WithOne(e => e.Producto)
                      .HasForeignKey(e => e.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.MovimientosInventario)
                      .WithOne(e => e.Producto)
                      .HasForeignKey(e => e.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureServicio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(500);

                // Relaciones
                entity.HasMany(e => e.DetallesFactura)
                      .WithOne(e => e.Servicio)
                      .HasForeignKey(e => e.ServicioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureMovimientoInventario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovimientoInventario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Referencia).HasMaxLength(200);
            });
        }

        private void ConfigurePago(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)").IsRequired();

                // Restricción: solo una de las tres relaciones puede tener valor
                entity.HasCheckConstraint("CK_Pago_SingleReference", 
                    "(FacturaId IS NOT NULL AND CompraId IS NULL AND GastoId IS NULL) OR " +
                    "(FacturaId IS NULL AND CompraId IS NOT NULL AND GastoId IS NULL) OR " +
                    "(FacturaId IS NULL AND CompraId IS NULL AND GastoId IS NOT NULL)");
            });
        }

        private void ConfigureDetalleFactura(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();

                // Restricción: ProductoId o ServicioId debe tener valor (no ambos nulos)
                entity.HasCheckConstraint("CK_DetalleFactura_ProductoOrServicio", 
                    "(ProductoId IS NOT NULL AND ServicioId IS NULL) OR " +
                    "(ProductoId IS NULL AND ServicioId IS NOT NULL)");
            });
        }

        private void ConfigureDetalleCompra(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();
            });
        }

        private void ConfigureDetallePedido(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();
            });
        }

        private void ConfigureSucursal(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.EsPrincipal).HasDefaultValue(false);

                // Relaciones
                entity.HasMany(e => e.Compras)
                      .WithOne(e => e.Sucursal)
                      .HasForeignKey(e => e.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.MovimientosInventario)
                      .WithOne(e => e.Sucursal)
                      .HasForeignKey(e => e.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Pedidos)
                      .WithOne(e => e.Sucursal)
                      .HasForeignKey(e => e.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.ReportesMensuales)
                      .WithOne(e => e.Sucursal)
                      .HasForeignKey(e => e.SucursalId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Usuarios)
                      .WithOne(e => e.Sucursal)
                      .HasForeignKey(e => e.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Rol).IsRequired().HasMaxLength(20);

                // Índice único para Email
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }

        private void ConfigureReporteMensual(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReporteMensual>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Mes).IsRequired();
                entity.Property(e => e.Anio).IsRequired();
                entity.Property(e => e.TotalVentas).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.TotalGastos).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.UtilidadNeta).HasColumnType("decimal(18,2)").IsRequired();

                // Índice compuesto para Mes y Año
                entity.HasIndex(e => new { e.Mes, e.Anio, e.SucursalId });
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Datos semilla para métodos de pago
            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { Id = 1, Nombre = "Efectivo", Descripcion = "Pago en efectivo" },
                new MetodoPago { Id = 2, Nombre = "Tarjeta de Crédito", Descripcion = "Pago con tarjeta de crédito" },
                new MetodoPago { Id = 3, Nombre = "Tarjeta de Débito", Descripcion = "Pago con tarjeta de débito" },
                new MetodoPago { Id = 4, Nombre = "Transferencia", Descripcion = "Transferencia bancaria" },
                new MetodoPago { Id = 5, Nombre = "Cheque", Descripcion = "Pago con cheque" }
            );

            // Datos semilla para impuestos
            modelBuilder.Entity<Impuesto>().HasData(
                new Impuesto { Id = 1, Nombre = "IVA", Porcentaje = 16.0m, Pais = "México" },
                new Impuesto { Id = 2, Nombre = "IGV", Porcentaje = 18.0m, Pais = "Perú" },
                new Impuesto { Id = 3, Nombre = "GST", Porcentaje = 10.0m, Pais = "Australia" }
            );

            // Datos semilla para tipos de gasto
            modelBuilder.Entity<TipoGasto>().HasData(
                new TipoGasto { Id = 1, Nombre = "Alquiler", Descripcion = "Gastos de alquiler de oficinas o locales" },
                new TipoGasto { Id = 2, Nombre = "Sueldos", Descripcion = "Gastos de personal y nómina" },
                new TipoGasto { Id = 3, Nombre = "Marketing", Descripcion = "Gastos de publicidad y marketing" },
                new TipoGasto { Id = 4, Nombre = "Servicios Públicos", Descripcion = "Luz, agua, gas, internet, etc." },
                new TipoGasto { Id = 5, Nombre = "Mantenimiento", Descripcion = "Gastos de mantenimiento de equipos y locales" }
            );

            // Datos semilla para categorías de productos
            modelBuilder.Entity<CategoriaProducto>().HasData(
                new CategoriaProducto { Id = 1, Nombre = "Electrónicos", Descripcion = "Productos electrónicos y tecnológicos" },
                new CategoriaProducto { Id = 2, Nombre = "Ropa", Descripcion = "Vestimenta y accesorios" },
                new CategoriaProducto { Id = 3, Nombre = "Hogar", Descripcion = "Artículos para el hogar" },
                new CategoriaProducto { Id = 4, Nombre = "Deportes", Descripcion = "Artículos deportivos" },
                new CategoriaProducto { Id = 5, Nombre = "Libros", Descripcion = "Libros y material educativo" }
            );

            // Datos semilla para sucursal principal
            modelBuilder.Entity<Sucursal>().HasData(
                new Sucursal { Id = 1, Nombre = "Sucursal Principal", Direccion = "Av. Principal 123, Ciudad", EsPrincipal = true }
            );
        }
    }
}
