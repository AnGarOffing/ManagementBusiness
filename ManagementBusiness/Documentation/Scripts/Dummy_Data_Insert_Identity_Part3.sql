-- =====================================================
-- TERCERA PARTE: VENTAS, FACTURAS, DETALLES Y MOVIMIENTOS
-- =====================================================
-- Continuación del script de datos dummy
-- Ejecutar después de la segunda parte
-- =====================================================

USE ManagementBusiness;
GO

-- =====================================================
-- 10. INSERTAR VENTAS
-- =====================================================
-- Obtener IDs de pedidos y métodos de pago
DECLARE @Pedido1Id INT, @Pedido3Id INT, @Pedido5Id INT, @Pedido7Id INT, @Pedido9Id INT;
DECLARE @MetodoPagoEfectivoId INT, @MetodoPagoTarjetaCreditoId INT, @MetodoPagoTarjetaDebitoId INT;

SELECT @Pedido1Id = Id FROM Pedidos WHERE Fecha = '2024-01-25' AND ClienteId = (SELECT Id FROM Clientes WHERE Nombre = 'Empresa ABC S.A. de C.V.');
SELECT @Pedido3Id = Id FROM Pedidos WHERE Fecha = '2024-02-20' AND ClienteId = (SELECT Id FROM Clientes WHERE Nombre = 'Carlos Alberto Ruiz');
SELECT @Pedido5Id = Id FROM Pedidos WHERE Fecha = '2024-03-15' AND ClienteId = (SELECT Id FROM Clientes WHERE Nombre = 'Hotel Plaza Central');
SELECT @Pedido7Id = Id FROM Pedidos WHERE Fecha = '2024-04-10' AND ClienteId = (SELECT Id FROM Clientes WHERE Nombre = 'Escuela Primaria Libertad');
SELECT @Pedido9Id = Id FROM Pedidos WHERE Fecha = '2024-05-05' AND ClienteId = (SELECT Id FROM Clientes WHERE Nombre = 'Papelería El Estudiante');

SELECT @MetodoPagoEfectivoId = Id FROM MetodosPago WHERE Nombre = 'Efectivo';
SELECT @MetodoPagoTarjetaCreditoId = Id FROM MetodosPago WHERE Nombre = 'Tarjeta de Crédito';
SELECT @MetodoPagoTarjetaDebitoId = Id FROM MetodosPago WHERE Nombre = 'Tarjeta de Débito';

INSERT INTO Ventas (Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId) VALUES
('2024-01-26', @Pedido1Id, @MetodoPagoEfectivoId, 25000.00, 'Completada', NULL),
('2024-02-21', @Pedido3Id, @MetodoPagoTarjetaCreditoId, 32000.00, 'Completada', NULL),
('2024-03-16', @Pedido5Id, @MetodoPagoTarjetaDebitoId, 45000.00, 'Completada', NULL),
('2024-04-11', @Pedido7Id, @MetodoPagoEfectivoId, 28000.00, 'Completada', NULL),
('2024-05-06', @Pedido9Id, @MetodoPagoTarjetaCreditoId, 35000.00, 'Completada', NULL);

-- =====================================================
-- 11. INSERTAR FACTURAS
-- =====================================================
INSERT INTO Facturas (Fecha, NumeroFactura, ClienteId, Total, Estado) VALUES
('2024-01-26', 'FAC-001-2024', (SELECT Id FROM Clientes WHERE Nombre = 'Empresa ABC S.A. de C.V.'), 25000.00, 'Pagada'),
('2024-02-21', 'FAC-002-2024', (SELECT Id FROM Clientes WHERE Nombre = 'Carlos Alberto Ruiz'), 32000.00, 'Pagada'),
('2024-03-16', 'FAC-003-2024', (SELECT Id FROM Clientes WHERE Nombre = 'Hotel Plaza Central'), 45000.00, 'Pagada'),
('2024-04-11', 'FAC-004-2024', (SELECT Id FROM Clientes WHERE Nombre = 'Escuela Primaria Libertad'), 28000.00, 'Pagada'),
('2024-05-06', 'FAC-005-2024', (SELECT Id FROM Clientes WHERE Nombre = 'Papelería El Estudiante'), 35000.00, 'Pagada');

-- Actualizar las ventas con las facturas correspondientes
UPDATE Ventas SET FacturaId = (SELECT Id FROM Facturas WHERE NumeroFactura = 'FAC-001-2024') WHERE Id = @Pedido1Id;
UPDATE Ventas SET FacturaId = (SELECT Id FROM Facturas WHERE NumeroFactura = 'FAC-002-2024') WHERE Id = @Pedido3Id;
UPDATE Ventas SET FacturaId = (SELECT Id FROM Facturas WHERE NumeroFactura = 'FAC-003-2024') WHERE Id = @Pedido5Id;
UPDATE Ventas SET FacturaId = (SELECT Id FROM Facturas WHERE NumeroFactura = 'FAC-004-2024') WHERE Id = @Pedido7Id;
UPDATE Ventas SET FacturaId = (SELECT Id FROM Facturas WHERE NumeroFactura = 'FAC-005-2024') WHERE Id = @Pedido9Id;

-- =====================================================
-- 12. INSERTAR DETALLES DE PEDIDOS
-- =====================================================
-- Obtener IDs de productos
DECLARE @ProductoLaptopId INT, @ProductoMouseId INT, @ProductoTecladoId INT, @ProductoCamisetaId INT, @ProductoJeansId INT;
DECLARE @ProductoSudaderaId INT, @ProductoLamparaId INT, @ProductoSabanasId INT, @ProductoPelotaId INT, @ProductoRaquetaId INT;
DECLARE @ProductoLibroId INT, @ProductoRompecabezasId INT, @ProductoAceiteId INT, @ProductoFiltroId INT, @ProductoMacetaId INT, @ProductoFertilizanteId INT;

SELECT @ProductoLaptopId = Id FROM Productos WHERE SKU = 'LAP-001';
SELECT @ProductoMouseId = Id FROM Productos WHERE SKU = 'MOU-001';
SELECT @ProductoTecladoId = Id FROM Productos WHERE SKU = 'TEC-001';
SELECT @ProductoCamisetaId = Id FROM Productos WHERE SKU = 'CAM-001';
SELECT @ProductoJeansId = Id FROM Productos WHERE SKU = 'JEA-001';
SELECT @ProductoSudaderaId = Id FROM Productos WHERE SKU = 'SUD-001';
SELECT @ProductoLamparaId = Id FROM Productos WHERE SKU = 'LAM-001';
SELECT @ProductoSabanasId = Id FROM Productos WHERE SKU = 'SAB-001';
SELECT @ProductoPelotaId = Id FROM Productos WHERE SKU = 'PEL-001';
SELECT @ProductoRaquetaId = Id FROM Productos WHERE SKU = 'RAQ-001';
SELECT @ProductoLibroId = Id FROM Productos WHERE SKU = 'LIB-001';
SELECT @ProductoRompecabezasId = Id FROM Productos WHERE SKU = 'ROM-001';
SELECT @ProductoAceiteId = Id FROM Productos WHERE SKU = 'ACE-001';
SELECT @ProductoFiltroId = Id FROM Productos WHERE SKU = 'FIL-001';
SELECT @ProductoMacetaId = Id FROM Productos WHERE SKU = 'MAC-001';
SELECT @ProductoFertilizanteId = Id FROM Productos WHERE SKU = 'FER-001';

INSERT INTO DetallesPedido (PedidoId, ProductoId, Cantidad, PrecioUnitario) VALUES
(@Pedido1Id, @ProductoLaptopId, 2, 12999.00),
(@Pedido1Id, @ProductoMouseId, 5, 299.00),
(@Pedido3Id, @ProductoTecladoId, 3, 1499.00),
(@Pedido3Id, @ProductoCamisetaId, 10, 299.00),
(@Pedido5Id, @ProductoJeansId, 8, 799.00),
(@Pedido5Id, @ProductoSudaderaId, 12, 599.00),
(@Pedido7Id, @ProductoLamparaId, 4, 399.00),
(@Pedido7Id, @ProductoSabanasId, 6, 499.00),
(@Pedido9Id, @ProductoPelotaId, 15, 199.00),
(@Pedido9Id, @ProductoRaquetaId, 5, 899.00);

-- =====================================================
-- 13. INSERTAR DETALLES DE FACTURAS
-- =====================================================
-- Obtener IDs de facturas
DECLARE @Factura1Id INT, @Factura2Id INT, @Factura3Id INT, @Factura4Id INT, @Factura5Id INT;

SELECT @Factura1Id = Id FROM Facturas WHERE NumeroFactura = 'FAC-001-2024';
SELECT @Factura2Id = Id FROM Facturas WHERE NumeroFactura = 'FAC-002-2024';
SELECT @Factura3Id = Id FROM Facturas WHERE NumeroFactura = 'FAC-003-2024';
SELECT @Factura4Id = Id FROM Facturas WHERE NumeroFactura = 'FAC-004-2024';
SELECT @Factura5Id = Id FROM Facturas WHERE NumeroFactura = 'FAC-005-2024';

INSERT INTO DetallesFactura (FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario) VALUES
(@Factura1Id, @ProductoLaptopId, NULL, 2, 12999.00),
(@Factura1Id, @ProductoMouseId, NULL, 5, 299.00),
(@Factura2Id, @ProductoTecladoId, NULL, 3, 1499.00),
(@Factura2Id, @ProductoCamisetaId, NULL, 10, 299.00),
(@Factura3Id, @ProductoJeansId, NULL, 8, 799.00),
(@Factura3Id, @ProductoSudaderaId, NULL, 12, 599.00),
(@Factura4Id, @ProductoLamparaId, NULL, 4, 399.00),
(@Factura4Id, @ProductoSabanasId, NULL, 6, 499.00),
(@Factura5Id, @ProductoPelotaId, NULL, 15, 199.00),
(@Factura5Id, @ProductoRaquetaId, NULL, 5, 899.00);

-- =====================================================
-- 14. INSERTAR DETALLES DE COMPRAS
-- =====================================================
-- Obtener IDs de compras
DECLARE @Compra1Id INT, @Compra2Id INT, @Compra3Id INT, @Compra4Id INT, @Compra5Id INT;

SELECT @Compra1Id = Id FROM Compras WHERE NumeroFactura = 'FAC-001-2024';
SELECT @Compra2Id = Id FROM Compras WHERE NumeroFactura = 'FAC-002-2024';
SELECT @Compra3Id = Id FROM Compras WHERE NumeroFactura = 'FAC-003-2024';
SELECT @Compra4Id = Id FROM Compras WHERE NumeroFactura = 'FAC-004-2024';
SELECT @Compra5Id = Id FROM Compras WHERE NumeroFactura = 'FAC-005-2024';

INSERT INTO DetallesCompra (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES
(@Compra1Id, @ProductoLaptopId, 10, 8500.00),
(@Compra1Id, @ProductoMouseId, 50, 150.00),
(@Compra2Id, @ProductoTecladoId, 20, 800.00),
(@Compra2Id, @ProductoCamisetaId, 100, 120.00),
(@Compra3Id, @ProductoJeansId, 80, 350.00),
(@Compra3Id, @ProductoSudaderaId, 60, 280.00),
(@Compra4Id, @ProductoLamparaId, 30, 180.00),
(@Compra4Id, @ProductoSabanasId, 40, 220.00),
(@Compra5Id, @ProductoPelotaId, 100, 95.00),
(@Compra5Id, @ProductoRaquetaId, 25, 450.00);

-- =====================================================
-- 15. INSERTAR MOVIMIENTOS DE INVENTARIO
-- =====================================================
-- Obtener IDs de sucursales
DECLARE @SucursalCentroId INT, @SucursalNorteId INT, @SucursalSurId INT, @SucursalEsteId INT;

SELECT @SucursalCentroId = Id FROM Sucursales WHERE Nombre = 'Sucursal Centro';
SELECT @SucursalNorteId = Id FROM Sucursales WHERE Nombre = 'Sucursal Norte';
SELECT @SucursalSurId = Id FROM Sucursales WHERE Nombre = 'Sucursal Sur';
SELECT @SucursalEsteId = Id FROM Sucursales WHERE Nombre = 'Sucursal Este';

INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
-- Entradas por compras
('2024-01-10', @ProductoLaptopId, @SucursalCentroId, 10, 'Entrada', 'Compra FAC-001-2024', NULL, @Compra1Id),
('2024-01-10', @ProductoMouseId, @SucursalCentroId, 50, 'Entrada', 'Compra FAC-001-2024', NULL, @Compra1Id),
('2024-01-25', @ProductoTecladoId, @SucursalCentroId, 20, 'Entrada', 'Compra FAC-002-2024', NULL, @Compra2Id),
('2024-01-25', @ProductoCamisetaId, @SucursalCentroId, 100, 'Entrada', 'Compra FAC-002-2024', NULL, @Compra2Id),
('2024-02-10', @ProductoJeansId, @SucursalNorteId, 80, 'Entrada', 'Compra FAC-003-2024', NULL, @Compra3Id),
('2024-02-10', @ProductoSudaderaId, @SucursalNorteId, 60, 'Entrada', 'Compra FAC-003-2024', NULL, @Compra3Id),
-- Salidas por ventas
('2024-01-26', @ProductoLaptopId, @SucursalCentroId, -2, 'Salida', 'Venta PED-001', @Pedido1Id, NULL),
('2024-01-26', @ProductoMouseId, @SucursalCentroId, -5, 'Salida', 'Venta PED-001', @Pedido1Id, NULL),
('2024-02-21', @ProductoTecladoId, @SucursalNorteId, -3, 'Salida', 'Venta PED-003', @Pedido3Id, NULL),
('2024-02-21', @ProductoCamisetaId, @SucursalNorteId, -10, 'Salida', 'Venta PED-003', @Pedido3Id, NULL);

-- =====================================================
-- 16. INSERTAR PAGOS
-- =====================================================
-- Obtener ID del método de pago Transferencia
DECLARE @MetodoPagoTransferenciaId INT;
SELECT @MetodoPagoTransferenciaId = Id FROM MetodosPago WHERE Nombre = 'Transferencia';

INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
-- Pagos de compras
('2024-01-15', 45000.00, @MetodoPagoTransferenciaId, NULL, @Compra1Id, NULL),
('2024-01-30', 32000.00, @MetodoPagoTransferenciaId, NULL, @Compra2Id, NULL),
('2024-02-15', 28000.00, @MetodoPagoTransferenciaId, NULL, @Compra3Id, NULL),
('2024-03-15', 38000.00, @MetodoPagoTransferenciaId, NULL, @Compra5Id, NULL),
('2024-03-30', 22000.00, @MetodoPagoTransferenciaId, NULL, (SELECT Id FROM Compras WHERE NumeroFactura = 'FAC-006-2024'), NULL),
('2024-04-30', 29000.00, @MetodoPagoTransferenciaId, NULL, (SELECT Id FROM Compras WHERE NumeroFactura = 'FAC-008-2024'), NULL),
('2024-05-30', 48000.00, @MetodoPagoTransferenciaId, NULL, (SELECT Id FROM Compras WHERE NumeroFactura = 'FAC-010-2024'), NULL),
-- Pagos de facturas
('2024-01-26', 25000.00, @MetodoPagoEfectivoId, @Factura1Id, NULL, NULL),
('2024-02-21', 32000.00, @MetodoPagoTarjetaCreditoId, @Factura2Id, NULL, NULL),
('2024-03-16', 45000.00, @MetodoPagoTarjetaDebitoId, @Factura3Id, NULL, NULL),
('2024-04-11', 28000.00, @MetodoPagoEfectivoId, @Factura4Id, NULL, NULL),
('2024-05-06', 35000.00, @MetodoPagoTarjetaCreditoId, @Factura5Id, NULL, NULL);

-- =====================================================
-- 17. INSERTAR REPORTES MENSUALES
-- =====================================================
INSERT INTO ReportesMensuales (Mes, Anio, TotalVentas, TotalGastos, UtilidadNeta, SucursalId) VALUES
-- Sucursal Centro
(1, 2024, 125000.00, 85000.00, 40000.00, @SucursalCentroId),
(2, 2024, 138000.00, 92000.00, 46000.00, @SucursalCentroId),
(3, 2024, 145000.00, 98000.00, 47000.00, @SucursalCentroId),
(4, 2024, 132000.00, 89000.00, 43000.00, @SucursalCentroId),
(5, 2024, 158000.00, 105000.00, 53000.00, @SucursalCentroId),
(6, 2024, 142000.00, 95000.00, 47000.00, @SucursalCentroId),
-- Sucursal Norte
(1, 2024, 98000.00, 65000.00, 33000.00, @SucursalNorteId),
(2, 2024, 105000.00, 70000.00, 35000.00, @SucursalNorteId),
(3, 2024, 112000.00, 75000.00, 37000.00, @SucursalNorteId),
(4, 2024, 98000.00, 68000.00, 30000.00, @SucursalNorteId),
(5, 2024, 118000.00, 78000.00, 40000.00, @SucursalNorteId),
(6, 2024, 108000.00, 72000.00, 36000.00, @SucursalNorteId);

-- =====================================================
-- VERIFICACIÓN FINAL
-- =====================================================
PRINT '=== VERIFICACIÓN DE DATOS INSERTADOS ===';

-- Verificar totales usando variables
DECLARE @TotalSucursales INT, @TotalUsuarios INT, @TotalClientes INT, @TotalProveedores INT;
DECLARE @TotalProductos INT, @TotalServicios INT, @TotalPresupuestos INT, @TotalPedidos INT;
DECLARE @TotalVentas INT, @TotalFacturas INT, @TotalCompras INT, @TotalGastos INT;
DECLARE @TotalPagos INT, @TotalMovimientos INT, @TotalReportes INT;

SELECT @TotalSucursales = COUNT(*) FROM Sucursales;
SELECT @TotalUsuarios = COUNT(*) FROM Usuarios;
SELECT @TotalClientes = COUNT(*) FROM Clientes;
SELECT @TotalProveedores = COUNT(*) FROM Proveedores;
SELECT @TotalProductos = COUNT(*) FROM Productos;
SELECT @TotalServicios = COUNT(*) FROM Servicios;
SELECT @TotalPresupuestos = COUNT(*) FROM Presupuestos;
SELECT @TotalPedidos = COUNT(*) FROM Pedidos;
SELECT @TotalVentas = COUNT(*) FROM Ventas;
SELECT @TotalFacturas = COUNT(*) FROM Facturas;
SELECT @TotalCompras = COUNT(*) FROM Compras;
SELECT @TotalGastos = COUNT(*) FROM Gastos;
SELECT @TotalPagos = COUNT(*) FROM Pagos;
SELECT @TotalMovimientos = COUNT(*) FROM MovimientosInventario;
SELECT @TotalReportes = COUNT(*) FROM ReportesMensuales;

PRINT 'Sucursales: ' + CAST(@TotalSucursales AS VARCHAR(10));
PRINT 'Usuarios: ' + CAST(@TotalUsuarios AS VARCHAR(10));
PRINT 'Clientes: ' + CAST(@TotalClientes AS VARCHAR(10));
PRINT 'Proveedores: ' + CAST(@TotalProveedores AS VARCHAR(10));
PRINT 'Productos: ' + CAST(@TotalProductos AS VARCHAR(10));
PRINT 'Servicios: ' + CAST(@TotalServicios AS VARCHAR(10));
PRINT 'Presupuestos: ' + CAST(@TotalPresupuestos AS VARCHAR(10));
PRINT 'Pedidos: ' + CAST(@TotalPedidos AS VARCHAR(10));
PRINT 'Ventas: ' + CAST(@TotalVentas AS VARCHAR(10));
PRINT 'Facturas: ' + CAST(@TotalFacturas AS VARCHAR(10));
PRINT 'Compras: ' + CAST(@TotalCompras AS VARCHAR(10));
PRINT 'Gastos: ' + CAST(@TotalGastos AS VARCHAR(10));
PRINT 'Pagos: ' + CAST(@TotalPagos AS VARCHAR(10));
PRINT 'Movimientos de Inventario: ' + CAST(@TotalMovimientos AS VARCHAR(10));
PRINT 'Reportes Mensuales: ' + CAST(@TotalReportes AS VARCHAR(10));

PRINT '==========================================';
PRINT 'Script ejecutado exitosamente.';
PRINT 'La base de datos ahora contiene datos de prueba realistas.';
PRINT '==========================================';
GO
