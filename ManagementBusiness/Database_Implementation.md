# Implementación de Base de Datos con Entity Framework Core

## Descripción General

Se ha implementado completamente la base de datos para el sistema de gestión empresarial utilizando Entity Framework Core 9.0 y SQL Server. La base de datos incluye todas las entidades solicitadas con sus relaciones y restricciones.

## Configuración de la Base de Datos

### 🔗 Cadena de Conexión
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=Offing;Database=ManagementBusiness;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

- **Servidor**: Offing
- **Base de Datos**: ManagementBusiness
- **Autenticación**: Windows Authentication
- **TrustServerCertificate**: true (para desarrollo)

### 📦 Paquetes NuGet Instalados
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.8)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.8)

## Estructura de Entidades Implementadas

### 🏢 **Entidades Principales**

#### 1. **Cliente**
- **Campos**: Id, Nombre, RFC, Email, Telefono, Direccion, EsActivo, FechaRegistro
- **Relaciones**: 
  - → Facturas (1:N)
  - → Presupuestos (1:N)
  - → Pedidos (1:N)

#### 2. **Proveedor**
- **Campos**: Id, Nombre, RFC, Email, Telefono, CuentaBancaria
- **Relaciones**:
  - → Compras (1:N)
  - → Gastos (1:N)

#### 3. **Factura**
- **Campos**: Id, Fecha, NumeroFactura, ClienteId, Total, Estado
- **Relaciones**:
  - ← Cliente (N:1)
  - → DetallesFactura (1:N)
  - → Pagos (1:N)
  - → Venta (1:1 opcional)

#### 4. **Presupuesto**
- **Campos**: Id, Fecha, ClienteId, Total, Estado
- **Relaciones**:
  - ← Cliente (N:1)
  - → Pedido (1:1 opcional)

#### 5. **Pedido**
- **Campos**: Id, Fecha, ClienteId, PresupuestoId, SucursalId, Estado
- **Relaciones**:
  - ← Cliente (N:1)
  - ← Presupuesto (N:1 opcional)
  - ← Sucursal (N:1)
  - → DetallesPedido (1:N)
  - → Venta (1:1)

#### 6. **Venta**
- **Campos**: Id, Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId
- **Relaciones**:
  - ← Pedido (N:1)
  - ← MetodoPago (N:1)
  - ← Factura (N:1 opcional)
  - → MovimientosInventario (1:N)

#### 7. **Compra**
- **Campos**: Id, Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado
- **Relaciones**:
  - ← Proveedor (N:1)
  - ← Sucursal (N:1)
  - → DetallesCompra (1:N)
  - → Pagos (1:N)

#### 8. **Gasto**
- **Campos**: Id, Fecha, Monto, Descripcion, TipoGastoId, ProveedorId
- **Relaciones**:
  - ← TipoGasto (N:1)
  - ← Proveedor (N:1 opcional)
  - → Pagos (1:N)

#### 9. **Producto**
- **Campos**: Id, Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId
- **Relaciones**:
  - ← CategoriaProducto (N:1)
  - ← Impuesto (N:1)
  - → DetallesFactura (1:N)
  - → DetallesCompra (1:N)
  - → DetallesPedido (1:N)
  - → MovimientosInventario (1:N)

#### 10. **Servicio**
- **Campos**: Id, Nombre, Precio, Descripcion, ImpuestoId
- **Relaciones**:
  - ← Impuesto (N:1)
  - → DetallesFactura (1:N)

### 🏗️ **Entidades de Soporte**

#### 11. **CategoriaProducto**
- **Campos**: Id, Nombre, Descripcion
- **Relaciones**: → Productos (1:N)

#### 12. **MovimientoInventario**
- **Campos**: Id, Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId
- **Relaciones**:
  - ← Producto (N:1)
  - ← Sucursal (N:1)
  - ← Venta (N:1 opcional)
  - ← Compra (N:1 opcional)

#### 13. **Pago**
- **Campos**: Id, Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId
- **Relaciones**:
  - ← MetodoPago (N:1)
  - ← Factura (N:1 opcional)
  - ← Compra (N:1 opcional)
  - ← Gasto (N:1 opcional)

#### 14. **MetodoPago**
- **Campos**: Id, Nombre, Descripcion
- **Relaciones**: → Pagos (1:N), → Ventas (1:N)

#### 15. **Impuesto**
- **Campos**: Id, Nombre, Porcentaje, Pais
- **Relaciones**: → Productos (1:N), → Servicios (1:N)

#### 16. **TipoGasto**
- **Campos**: Id, Nombre, Descripcion
- **Relaciones**: → Gastos (1:N)

#### 17. **Sucursal**
- **Campos**: Id, Nombre, Direccion, EsPrincipal
- **Relaciones**:
  - → Compras (1:N)
  - → MovimientosInventario (1:N)
  - → Pedidos (1:N)
  - → ReportesMensuales (1:N)
  - → Usuarios (1:N)

#### 18. **Usuario**
- **Campos**: Id, Nombre, Email, PasswordHash, Rol, SucursalId
- **Relaciones**: ← Sucursal (N:1)

#### 19. **ReporteMensual**
- **Campos**: Id, Mes, Anio, TotalVentas, TotalGastos, UtilidadNeta, SucursalId
- **Relaciones**: ← Sucursal (N:1)

### 📋 **Entidades de Detalle**

#### 20. **DetalleFactura**
- **Campos**: Id, FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario
- **Relaciones**:
  - ← Factura (N:1)
  - ← Producto (N:1 opcional)
  - ← Servicio (N:1 opcional)

#### 21. **DetalleCompra**
- **Campos**: Id, CompraId, ProductoId, Cantidad, PrecioUnitario
- **Relaciones**:
  - ← Compra (N:1)
  - ← Producto (N:1)

#### 22. **DetallePedido**
- **Campos**: Id, PedidoId, ProductoId, Cantidad, PrecioUnitario
- **Relaciones**:
  - ← Pedido (N:1)
  - ← Producto (N:1)

## Características de Implementación

### ✅ **Restricciones y Validaciones**
- **Claves primarias**: Todas las entidades tienen Id como clave primaria auto-incremental
- **Claves foráneas**: Todas las relaciones están correctamente configuradas
- **Restricciones de integridad**: DeleteBehavior configurado apropiadamente para cada relación
- **Validaciones**: Anotaciones de DataAnnotations para validación de datos

### ✅ **Índices y Optimización**
- **SKU único**: Producto.SKU tiene índice único
- **NumeroFactura único**: Factura.NumeroFactura tiene índice único
- **Email único**: Usuario.Email tiene índice único
- **Índice compuesto**: ReporteMensual (Mes, Anio, SucursalId)

### ✅ **Restricciones de Negocio**
- **Pago**: Solo una referencia puede tener valor (FacturaId, CompraId o GastoId)
- **DetalleFactura**: ProductoId o ServicioId debe tener valor (no ambos nulos)
- **Stock mínimo**: Producto.StockMinimo tiene valor por defecto 5

### ✅ **Datos Semilla**
- **Métodos de pago**: Efectivo, Tarjeta de Crédito, Tarjeta de Débito, Transferencia, Cheque
- **Impuestos**: IVA (16%), IGV (18%), GST (10%)
- **Tipos de gasto**: Alquiler, Sueldos, Marketing, Servicios Públicos, Mantenimiento
- **Categorías de productos**: Electrónicos, Ropa, Hogar, Deportes, Libros
- **Sucursal principal**: Sucursal Principal creada por defecto

## Migraciones

### 📝 **Migración Inicial**
- **Nombre**: InitialCreate
- **Fecha**: 2025-08-27
- **Estado**: ✅ Aplicada exitosamente
- **Base de datos**: Creada en el servidor Offing

### 🔧 **Comandos de Migración**
```bash
# Crear nueva migración
dotnet ef migrations add NombreMigracion

# Aplicar migraciones pendientes
dotnet ef database update

# Revertir última migración
dotnet ef database update NombreMigracionAnterior

# Eliminar migración no aplicada
dotnet ef migrations remove
```

## Estructura de Archivos

```
ManagementBusiness/
├── Data/
│   ├── ManagementBusinessContext.cs          # Contexto principal de EF Core
│   └── DesignTimeDbContextFactory.cs        # Fábrica para migraciones
├── Models/                                   # Todas las entidades del modelo
│   ├── BaseModel.cs                         # Clase base con INotifyPropertyChanged
│   ├── Cliente.cs                           # Entidad Cliente
│   ├── Proveedor.cs                         # Entidad Proveedor
│   ├── Factura.cs                           # Entidad Factura
│   ├── Presupuesto.cs                       # Entidad Presupuesto
│   ├── Pedido.cs                            # Entidad Pedido
│   ├── Venta.cs                             # Entidad Venta
│   ├── Compra.cs                            # Entidad Compra
│   ├── Gasto.cs                             # Entidad Gasto
│   ├── Producto.cs                          # Entidad Producto
│   ├── Servicio.cs                          # Entidad Servicio
│   ├── CategoriaProducto.cs                 # Entidad CategoriaProducto
│   ├── MovimientoInventario.cs              # Entidad MovimientoInventario
│   ├── Pago.cs                              # Entidad Pago
│   ├── MetodoPago.cs                        # Entidad MetodoPago
│   ├── Impuesto.cs                          # Entidad Impuesto
│   ├── TipoGasto.cs                         # Entidad TipoGasto
│   ├── Sucursal.cs                          # Entidad Sucursal
│   ├── Usuario.cs                           # Entidad Usuario
│   ├── ReporteMensual.cs                    # Entidad ReporteMensual
│   ├── DetalleFactura.cs                    # Entidad DetalleFactura
│   ├── DetalleCompra.cs                     # Entidad DetalleCompra
│   └── DetallePedido.cs                     # Entidad DetallePedido
├── Migrations/                               # Migraciones de EF Core
│   ├── 20250827001135_InitialCreate.cs      # Migración inicial
│   ├── 20250827001135_InitialCreate.Designer.cs
│   └── ManagementBusinessContextModelSnapshot.cs
├── appsettings.json                          # Configuración de conexión
└── Database_Implementation.md                # Esta documentación
```

## Próximos Pasos Recomendados

### 🔄 **Desarrollo de Funcionalidades**
1. **Repositorios**: Implementar patrón Repository para acceso a datos
2. **Servicios**: Crear servicios de negocio para cada entidad
3. **Validaciones**: Implementar validaciones de negocio adicionales
4. **Auditoría**: Agregar campos de auditoría (CreadoPor, ModificadoPor, etc.)

### 🧪 **Testing**
1. **Pruebas unitarias**: Crear tests para cada entidad y servicio
2. **Pruebas de integración**: Validar operaciones de base de datos
3. **Pruebas de migración**: Verificar que las migraciones funcionen correctamente

### 📊 **Monitoreo y Optimización**
1. **Logging**: Implementar logging de operaciones de base de datos
2. **Performance**: Monitorear consultas lentas y optimizar índices
3. **Backup**: Configurar estrategia de respaldo y recuperación

## Estado de la Implementación

✅ **Base de datos creada exitosamente**
✅ **Todas las entidades implementadas**
✅ **Relaciones configuradas correctamente**
✅ **Migración inicial aplicada**
✅ **Datos semilla insertados**
✅ **Restricciones de integridad implementadas**

La base de datos está lista para ser utilizada por la aplicación WPF con MVVM implementado anteriormente.
