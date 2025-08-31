-- =====================================================
-- SEGUNDA PARTE: TRANSACCIONES Y DETALLES
-- =====================================================
-- Continuación del script de datos dummy
-- Ejecutar después de la primera parte
-- =====================================================

USE ManagementBusiness;
GO

-- =====================================================
-- 6. INSERTAR PRESUPUESTOS
-- =====================================================
-- Obtener IDs de clientes
DECLARE @Cliente1Id INT, @Cliente2Id INT, @Cliente3Id INT, @Cliente4Id INT, @Cliente5Id INT;
DECLARE @Cliente6Id INT, @Cliente7Id INT, @Cliente8Id INT, @Cliente9Id INT, @Cliente10Id INT;

SELECT @Cliente1Id = Id FROM Clientes WHERE Nombre = 'Empresa ABC S.A. de C.V.';
SELECT @Cliente2Id = Id FROM Clientes WHERE Nombre = 'María Elena López';
SELECT @Cliente3Id = Id FROM Clientes WHERE Nombre = 'Carlos Alberto Ruiz';
SELECT @Cliente4Id = Id FROM Clientes WHERE Nombre = 'Restaurante El Sabor';
SELECT @Cliente5Id = Id FROM Clientes WHERE Nombre = 'Hotel Plaza Central';
SELECT @Cliente6Id = Id FROM Clientes WHERE Nombre = 'Farmacia San José';
SELECT @Cliente7Id = Id FROM Clientes WHERE Nombre = 'Escuela Primaria Libertad';
SELECT @Cliente8Id = Id FROM Clientes WHERE Nombre = 'Taller Mecánico Rápido';
SELECT @Cliente9Id = Id FROM Clientes WHERE Nombre = 'Papelería El Estudiante';
SELECT @Cliente10Id = Id FROM Clientes WHERE Nombre = 'Gimnasio Power Fit';

INSERT INTO Presupuestos (Fecha, ClienteId, Total, Estado) VALUES
('2024-01-20', @Cliente1Id, 25000.00, 'Aprobado'),
('2024-02-05', @Cliente2Id, 15000.00, 'Pendiente'),
('2024-02-15', @Cliente3Id, 32000.00, 'Aprobado'),
('2024-03-01', @Cliente4Id, 18000.00, 'Rechazado'),
('2024-03-10', @Cliente5Id, 45000.00, 'Aprobado'),
('2024-03-20', @Cliente6Id, 22000.00, 'Pendiente'),
('2024-04-05', @Cliente7Id, 28000.00, 'Aprobado'),
('2024-04-15', @Cliente8Id, 19000.00, 'Pendiente'),
('2024-05-01', @Cliente9Id, 35000.00, 'Aprobado'),
('2024-05-10', @Cliente10Id, 26000.00, 'Pendiente');

-- =====================================================
-- 7. INSERTAR PEDIDOS
-- =====================================================
-- Obtener IDs de presupuestos y sucursales
DECLARE @Presupuesto1Id INT, @Presupuesto2Id INT, @Presupuesto3Id INT, @Presupuesto5Id INT, @Presupuesto7Id INT, @Presupuesto9Id INT;
DECLARE @SucursalCentroId INT, @SucursalNorteId INT, @SucursalSurId INT, @SucursalEsteId INT;

SELECT @Presupuesto1Id = Id FROM Presupuestos WHERE ClienteId = @Cliente1Id AND Total = 25000.00;
SELECT @Presupuesto2Id = Id FROM Presupuestos WHERE ClienteId = @Cliente2Id AND Total = 15000.00;
SELECT @Presupuesto3Id = Id FROM Presupuestos WHERE ClienteId = @Cliente3Id AND Total = 32000.00;
SELECT @Presupuesto5Id = Id FROM Presupuestos WHERE ClienteId = @Cliente5Id AND Total = 45000.00;
SELECT @Presupuesto7Id = Id FROM Presupuestos WHERE ClienteId = @Cliente7Id AND Total = 28000.00;
SELECT @Presupuesto9Id = Id FROM Presupuestos WHERE ClienteId = @Cliente9Id AND Total = 35000.00;

SELECT @SucursalCentroId = Id FROM Sucursales WHERE Nombre = 'Sucursal Centro';
SELECT @SucursalNorteId = Id FROM Sucursales WHERE Nombre = 'Sucursal Norte';
SELECT @SucursalSurId = Id FROM Sucursales WHERE Nombre = 'Sucursal Sur';
SELECT @SucursalEsteId = Id FROM Sucursales WHERE Nombre = 'Sucursal Este';

INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
('2024-01-25', @Cliente1Id, @Presupuesto1Id, @SucursalCentroId, 'Completado'),
('2024-02-10', @Cliente2Id, @Presupuesto2Id, @SucursalCentroId, 'En Proceso'),
('2024-02-20', @Cliente3Id, @Presupuesto3Id, @SucursalNorteId, 'Completado'),
('2024-03-15', @Cliente5Id, @Presupuesto5Id, @SucursalSurId, 'Completado'),
('2024-04-10', @Cliente7Id, @Presupuesto7Id, @SucursalCentroId, 'Completado'),
('2024-05-05', @Cliente9Id, @Presupuesto9Id, @SucursalNorteId, 'Completado');

-- =====================================================
-- 8. INSERTAR COMPRAS
-- =====================================================
-- Obtener IDs de proveedores
DECLARE @Proveedor1Id INT, @Proveedor2Id INT, @Proveedor3Id INT, @Proveedor4Id INT, @Proveedor5Id INT;
DECLARE @Proveedor6Id INT, @Proveedor7Id INT, @Proveedor8Id INT, @Proveedor9Id INT, @Proveedor10Id INT;

SELECT @Proveedor1Id = Id FROM Proveedores WHERE Nombre = 'Distribuidora Nacional S.A.';
SELECT @Proveedor2Id = Id FROM Proveedores WHERE Nombre = 'Electrónicos del Norte';
SELECT @Proveedor3Id = Id FROM Proveedores WHERE Nombre = 'Textiles Mexicanos';
SELECT @Proveedor4Id = Id FROM Proveedores WHERE Nombre = 'Importadora del Pacífico';
SELECT @Proveedor5Id = Id FROM Proveedores WHERE Nombre = 'Suministros Industriales';
SELECT @Proveedor6Id = Id FROM Proveedores WHERE Nombre = 'Papel y Cartón Express';
SELECT @Proveedor7Id = Id FROM Proveedores WHERE Nombre = 'Herramientas Profesionales';
SELECT @Proveedor8Id = Id FROM Proveedores WHERE Nombre = 'Alimentos Frescos S.A.';
SELECT @Proveedor9Id = Id FROM Proveedores WHERE Nombre = 'Servicios de Limpieza Pro';
SELECT @Proveedor10Id = Id FROM Proveedores WHERE Nombre = 'Equipos de Cómputo';

INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
('2024-01-10', 'FAC-001-2024', @Proveedor1Id, @SucursalCentroId, 45000.00, 'Pagada'),
('2024-01-25', 'FAC-002-2024', @Proveedor2Id, @SucursalCentroId, 32000.00, 'Pagada'),
('2024-02-10', 'FAC-003-2024', @Proveedor3Id, @SucursalNorteId, 28000.00, 'Pagada'),
('2024-02-25', 'FAC-004-2024', @Proveedor4Id, @SucursalCentroId, 55000.00, 'Pendiente'),
('2024-03-10', 'FAC-005-2024', @Proveedor5Id, @SucursalSurId, 38000.00, 'Pagada'),
('2024-03-25', 'FAC-006-2024', @Proveedor6Id, @SucursalNorteId, 22000.00, 'Pagada'),
('2024-04-10', 'FAC-007-2024', @Proveedor7Id, @SucursalCentroId, 42000.00, 'Pendiente'),
('2024-04-25', 'FAC-008-2024', @Proveedor8Id, @SucursalEsteId, 29000.00, 'Pagada'),
('2024-05-10', 'FAC-009-2024', @Proveedor9Id, @SucursalNorteId, 35000.00, 'Pendiente'),
('2024-05-25', 'FAC-010-2024', @Proveedor10Id, @SucursalSurId, 48000.00, 'Pagada');

-- =====================================================
-- 9. INSERTAR GASTOS
-- =====================================================
-- Obtener IDs de tipos de gasto
DECLARE @TipoGastoAlquilerId INT, @TipoGastoSueldosId INT, @TipoGastoMarketingId INT, @TipoGastoServiciosId INT;
DECLARE @TipoGastoMantenimientoId INT, @TipoGastoSegurosId INT, @TipoGastoImpuestosId INT, @TipoGastoOtrosId INT;

SELECT @TipoGastoAlquilerId = Id FROM TiposGasto WHERE Nombre = 'Alquiler';
SELECT @TipoGastoSueldosId = Id FROM TiposGasto WHERE Nombre = 'Sueldos';
SELECT @TipoGastoMarketingId = Id FROM TiposGasto WHERE Nombre = 'Marketing';
SELECT @TipoGastoServiciosId = Id FROM TiposGasto WHERE Nombre = 'Servicios Públicos';
SELECT @TipoGastoMantenimientoId = Id FROM TiposGasto WHERE Nombre = 'Mantenimiento';
SELECT @TipoGastoSegurosId = Id FROM TiposGasto WHERE Nombre = 'Seguros';
SELECT @TipoGastoImpuestosId = Id FROM TiposGasto WHERE Nombre = 'Impuestos';
SELECT @TipoGastoOtrosId = Id FROM TiposGasto WHERE Nombre = 'Otros';

INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
('2024-01-05', 15000.00, 'Renta del local principal', @TipoGastoAlquilerId, NULL),
('2024-01-15', 25000.00, 'Nómina del personal', @TipoGastoSueldosId, NULL),
('2024-01-25', 8000.00, 'Publicidad en redes sociales', @TipoGastoMarketingId, @Proveedor9Id),
('2024-02-05', 5000.00, 'Servicios de luz y agua', @TipoGastoServiciosId, NULL),
('2024-02-15', 12000.00, 'Mantenimiento de equipos', @TipoGastoMantenimientoId, @Proveedor7Id),
('2024-02-25', 8000.00, 'Póliza de seguro anual', @TipoGastoSegurosId, NULL),
('2024-03-05', 15000.00, 'Pago de IVA', @TipoGastoImpuestosId, NULL),
('2024-03-15', 3000.00, 'Material de oficina', @TipoGastoOtrosId, @Proveedor6Id),
('2024-04-05', 15000.00, 'Renta del local norte', @TipoGastoAlquilerId, NULL),
('2024-04-15', 18000.00, 'Nómina del personal norte', @TipoGastoSueldosId, NULL);

PRINT '=== SEGUNDA PARTE COMPLETADA ===';
PRINT 'Presupuestos, Pedidos, Compras y Gastos insertados exitosamente.';
GO
