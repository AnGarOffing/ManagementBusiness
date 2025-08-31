-- =====================================================
-- Script para crear la base de datos ManagementBusiness
-- Basado en la implementación de Entity Framework Core
-- Todas las tablas usan IDENTITY para IDs auto-incrementales
-- =====================================================

USE master;
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ManagementBusiness')
BEGIN
    CREATE DATABASE ManagementBusiness;
END
GO

USE ManagementBusiness;
GO

-- =====================================================
-- CREAR TABLAS DE CATÁLOGOS (sin dependencias)
-- =====================================================

-- Tabla: MetodosPago
CREATE TABLE MetodosPago (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL
);

-- Tabla: Impuestos
CREATE TABLE Impuestos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Porcentaje DECIMAL(5,2) NOT NULL,
    Pais NVARCHAR(100) NOT NULL
);

-- Tabla: TiposGasto
CREATE TABLE TiposGasto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL
);

-- Tabla: CategoriasProductos
CREATE TABLE CategoriasProductos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL
);

-- Tabla: Sucursales
CREATE TABLE Sucursales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Direccion NVARCHAR(500) NOT NULL,
    EsPrincipal BIT NOT NULL DEFAULT 0
);

-- =====================================================
-- CREAR TABLAS PRINCIPALES
-- =====================================================

-- Tabla: Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    RFC NVARCHAR(13) NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    Direccion NVARCHAR(500) NOT NULL,
    EsActivo BIT NOT NULL DEFAULT 1,
    FechaRegistro DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- Tabla: Proveedores
CREATE TABLE Proveedores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    RFC NVARCHAR(13) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    CuentaBancaria NVARCHAR(50) NULL
);

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    SucursalId INT NOT NULL,
    CONSTRAINT FK_Usuarios_Sucursales FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id)
);

-- Tabla: Productos
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    SKU NVARCHAR(50) NOT NULL,
    PrecioCosto DECIMAL(18,2) NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL,
    StockMinimo INT NOT NULL DEFAULT 5,
    CategoriaId INT NOT NULL,
    ImpuestoId INT NOT NULL,
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaId) REFERENCES CategoriasProductos(Id),
    CONSTRAINT FK_Productos_Impuestos FOREIGN KEY (ImpuestoId) REFERENCES Impuestos(Id)
);

-- Tabla: Servicios
CREATE TABLE Servicios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    ImpuestoId INT NOT NULL,
    CONSTRAINT FK_Servicios_Impuestos FOREIGN KEY (ImpuestoId) REFERENCES Impuestos(Id)
);

-- =====================================================
-- CREAR TABLAS DE TRANSACCIONES
-- =====================================================

-- Tabla: Presupuestos
CREATE TABLE Presupuestos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    ClienteId INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    CONSTRAINT FK_Presupuestos_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla: Pedidos
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    ClienteId INT NOT NULL,
    PresupuestoId INT NULL,
    SucursalId INT NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    CONSTRAINT FK_Pedidos_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    CONSTRAINT FK_Pedidos_Presupuestos FOREIGN KEY (PresupuestoId) REFERENCES Presupuestos(Id),
    CONSTRAINT FK_Pedidos_Sucursales FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id)
);

-- Tabla: Ventas
CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    PedidoId INT NOT NULL,
    MetodoPagoId INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    FacturaId INT NULL,
    CONSTRAINT FK_Ventas_Pedidos FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    CONSTRAINT FK_Ventas_MetodosPago FOREIGN KEY (MetodoPagoId) REFERENCES MetodosPago(Id)
);

-- Tabla: Facturas
CREATE TABLE Facturas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    NumeroFactura NVARCHAR(50) NOT NULL,
    ClienteId INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    CONSTRAINT FK_Facturas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla: Compras
CREATE TABLE Compras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    NumeroFactura NVARCHAR(50) NOT NULL,
    ProveedorId INT NOT NULL,
    SucursalId INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL,
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id),
    CONSTRAINT FK_Compras_Sucursales FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id)
);

-- Tabla: Gastos
CREATE TABLE Gastos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Descripcion NVARCHAR(500) NOT NULL,
    TipoGastoId INT NOT NULL,
    ProveedorId INT NULL,
    CONSTRAINT FK_Gastos_TiposGasto FOREIGN KEY (TipoGastoId) REFERENCES TiposGasto(Id),
    CONSTRAINT FK_Gastos_Proveedores FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id)
);

-- =====================================================
-- CREAR TABLAS DE DETALLES
-- =====================================================

-- Tabla: DetallesPedido
CREATE TABLE DetallesPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_DetallesPedido_Pedidos FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    CONSTRAINT FK_DetallesPedido_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

-- Tabla: DetallesCompra
CREATE TABLE DetallesCompra (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompraId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_DetallesCompra_Compras FOREIGN KEY (CompraId) REFERENCES Compras(Id),
    CONSTRAINT FK_DetallesCompra_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

-- Tabla: DetallesFactura
CREATE TABLE DetallesFactura (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FacturaId INT NOT NULL,
    ProductoId INT NULL,
    ServicioId INT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_DetallesFactura_Facturas FOREIGN KEY (FacturaId) REFERENCES Facturas(Id),
    CONSTRAINT FK_DetallesFactura_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id),
    CONSTRAINT FK_DetallesFactura_Servicios FOREIGN KEY (ServicioId) REFERENCES Servicios(Id)
);

-- =====================================================
-- CREAR TABLAS DE MOVIMIENTOS Y PAGOS
-- =====================================================

-- Tabla: MovimientosInventario
CREATE TABLE MovimientosInventario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    ProductoId INT NOT NULL,
    SucursalId INT NOT NULL,
    Cantidad INT NOT NULL,
    Tipo NVARCHAR(20) NOT NULL,
    Referencia NVARCHAR(200) NULL,
    VentaId INT NULL,
    CompraId INT NULL,
    CONSTRAINT FK_MovimientosInventario_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id),
    CONSTRAINT FK_MovimientosInventario_Sucursales FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id),
    CONSTRAINT FK_MovimientosInventario_Ventas FOREIGN KEY (VentaId) REFERENCES Ventas(Id),
    CONSTRAINT FK_MovimientosInventario_Compras FOREIGN KEY (CompraId) REFERENCES Compras(Id)
);

-- Tabla: Pagos
CREATE TABLE Pagos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    MetodoPagoId INT NOT NULL,
    FacturaId INT NULL,
    CompraId INT NULL,
    GastoId INT NULL,
    CONSTRAINT FK_Pagos_MetodosPago FOREIGN KEY (MetodoPagoId) REFERENCES MetodosPago(Id),
    CONSTRAINT FK_Pagos_Facturas FOREIGN KEY (FacturaId) REFERENCES Facturas(Id),
    CONSTRAINT FK_Pagos_Compras FOREIGN KEY (CompraId) REFERENCES Compras(Id),
    CONSTRAINT FK_Pagos_Gastos FOREIGN KEY (GastoId) REFERENCES Gastos(Id)
);

-- Tabla: ReportesMensuales
CREATE TABLE ReportesMensuales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Mes INT NOT NULL,
    Anio INT NOT NULL,
    TotalVentas DECIMAL(18,2) NOT NULL,
    TotalGastos DECIMAL(18,2) NOT NULL,
    UtilidadNeta DECIMAL(18,2) NOT NULL,
    SucursalId INT NOT NULL,
    CONSTRAINT FK_ReportesMensuales_Sucursales FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id)
);

-- =====================================================
-- CREAR RELACIONES ADICIONALES
-- =====================================================

-- Agregar FK de Facturas a Ventas
ALTER TABLE Ventas ADD CONSTRAINT FK_Ventas_Facturas FOREIGN KEY (FacturaId) REFERENCES Facturas(Id);

-- =====================================================
-- CREAR ÍNDICES ÚNICOS
-- =====================================================

-- Índice único para SKU de productos
CREATE UNIQUE INDEX IX_Productos_SKU ON Productos(SKU);

-- Índice único para NumeroFactura de facturas
CREATE UNIQUE INDEX IX_Facturas_NumeroFactura ON Facturas(NumeroFactura);

-- Índice único para Email de usuarios
CREATE UNIQUE INDEX IX_Usuarios_Email ON Usuarios(Email);

-- Índice compuesto para ReportesMensuales
CREATE INDEX IX_ReportesMensuales_MesAnioSucursal ON ReportesMensuales(Mes, Anio, SucursalId);

-- =====================================================
-- CREAR RESTRICCIONES DE NEGOCIO
-- =====================================================

-- Restricción: Pago debe tener solo una referencia
ALTER TABLE Pagos ADD CONSTRAINT CK_Pago_SingleReference 
CHECK (
    (FacturaId IS NOT NULL AND CompraId IS NULL AND GastoId IS NULL) OR 
    (FacturaId IS NULL AND CompraId IS NOT NULL AND GastoId IS NULL) OR 
    (FacturaId IS NULL AND CompraId IS NULL AND GastoId IS NOT NULL)
);

-- Restricción: DetalleFactura debe tener ProductoId o ServicioId (no ambos nulos)
ALTER TABLE DetallesFactura ADD CONSTRAINT CK_DetalleFactura_ProductoOrServicio 
CHECK (
    (ProductoId IS NOT NULL AND ServicioId IS NULL) OR 
    (ProductoId IS NULL AND ServicioId IS NOT NULL)
);

-- =====================================================
-- INSERTAR DATOS SEMILLA (sin IDs explícitos)
-- =====================================================

-- Métodos de pago
INSERT INTO MetodosPago (Nombre, Descripcion) VALUES
('Efectivo', 'Pago en efectivo'),
('Tarjeta de Crédito', 'Pago con tarjeta de crédito'),
('Tarjeta de Débito', 'Pago con tarjeta de débito'),
('Transferencia', 'Transferencia bancaria'),
('Cheque', 'Pago con cheque'),
('PayPal', 'Pago electrónico PayPal'),
('Mercado Pago', 'Pago a través de Mercado Pago');

-- Impuestos
INSERT INTO Impuestos (Nombre, Porcentaje, Pais) VALUES
('IVA', 16.00, 'México'),
('IGV', 18.00, 'Perú'),
('GST', 10.00, 'Canadá'),
('VAT', 20.00, 'Reino Unido'),
('Sales Tax', 8.25, 'Estados Unidos');

-- Tipos de gasto
INSERT INTO TiposGasto (Nombre, Descripcion) VALUES
('Alquiler', 'Pagos de renta de locales'),
('Sueldos', 'Salarios del personal'),
('Marketing', 'Publicidad y promoción'),
('Servicios Públicos', 'Luz, agua, gas, internet'),
('Mantenimiento', 'Reparaciones y mantenimiento'),
('Seguros', 'Pólizas de seguro'),
('Impuestos', 'Pagos de impuestos gubernamentales'),
('Otros', 'Otros gastos operativos');

-- Categorías de productos
INSERT INTO CategoriasProductos (Nombre, Descripcion) VALUES
('Electrónicos', 'Dispositivos electrónicos y tecnología'),
('Ropa', 'Vestimenta para todas las edades'),
('Hogar', 'Artículos para el hogar y decoración'),
('Deportes', 'Equipamiento y ropa deportiva'),
('Libros', 'Libros de todas las categorías'),
('Juguetes', 'Juguetes para niños y coleccionistas'),
('Automotriz', 'Accesorios y repuestos para vehículos'),
('Jardín', 'Productos para jardín y exteriores');

-- Sucursales
INSERT INTO Sucursales (Nombre, Direccion, EsPrincipal) VALUES
('Sucursal Centro', 'Av. Juárez 123, Centro Histórico, CDMX', 1),
('Sucursal Norte', 'Blvd. Constitución 456, Col. Industrial, Monterrey', 0),
('Sucursal Sur', 'Calle Reforma 789, Col. Moderna, Guadalajara', 0),
('Sucursal Este', 'Av. Insurgentes 321, Col. Del Valle, CDMX', 0);

-- =====================================================
-- VERIFICAR LA CREACIÓN
-- =====================================================

-- Mostrar todas las tablas creadas
SELECT 
    t.name AS TableName,
    s.name AS SchemaName,
    p.rows AS RowCounts
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
INNER JOIN sys.indexes i ON t.object_id = i.object_id
INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
WHERE i.index_id <= 1
ORDER BY t.name;

-- Mostrar todas las restricciones de clave foránea
SELECT 
    fk.name AS FK_Name,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTableName,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ReferencedColumnName
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
ORDER BY TableName, ColumnName;

-- Mostrar datos semilla insertados
PRINT '=== DATOS SEMILLA INSERTADOS ===';

DECLARE @TotalMetodosPago INT, @TotalImpuestos INT, @TotalTiposGasto INT, @TotalCategorias INT, @TotalSucursales INT;

SELECT @TotalMetodosPago = COUNT(*) FROM MetodosPago;
SELECT @TotalImpuestos = COUNT(*) FROM Impuestos;
SELECT @TotalTiposGasto = COUNT(*) FROM TiposGasto;
SELECT @TotalCategorias = COUNT(*) FROM CategoriasProductos;
SELECT @TotalSucursales = COUNT(*) FROM Sucursales;

PRINT 'Métodos de Pago: ' + CAST(@TotalMetodosPago AS VARCHAR(10));
PRINT 'Impuestos: ' + CAST(@TotalImpuestos AS VARCHAR(10));
PRINT 'Tipos de Gasto: ' + CAST(@TotalTiposGasto AS VARCHAR(10));
PRINT 'Categorías de Productos: ' + CAST(@TotalCategorias AS VARCHAR(10));
PRINT 'Sucursales: ' + CAST(@TotalSucursales AS VARCHAR(10));
PRINT '==========================================';

PRINT 'Base de datos ManagementBusiness creada exitosamente con todas las tablas, relaciones y restricciones.';
PRINT 'Se han insertado los datos semilla para catálogos básicos usando IDENTITY auto-incremental.';
GO
