-- =====================================================
-- SCRIPT DE DATOS DUMMY PARA BASE DE DATOS MANAGEMENTBUSINESS
-- =====================================================
-- Este script inserta datos de prueba realistas para desarrollo y testing
-- Ejecutar en SQL Server Management Studio en la base de datos ManagementBusiness
-- =====================================================

USE ManagementBusiness;
GO

-- Limpiar datos existentes (opcional - descomentar si se desea)
-- EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
-- EXEC sp_MSforeachtable "DELETE FROM ?"
-- EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"

-- =====================================================
-- 1. INSERTAR SUCURSALES
-- =====================================================
INSERT INTO Sucursales (Nombre, Direccion, EsPrincipal) VALUES
('Sucursal Centro', 'Av. Juárez 123, Centro Histórico, CDMX', 1),
('Sucursal Norte', 'Blvd. Constitución 456, Col. Industrial, Monterrey', 0),
('Sucursal Sur', 'Calle Reforma 789, Col. Moderna, Guadalajara', 0),
('Sucursal Este', 'Av. Insurgentes 321, Col. Del Valle, CDMX', 0);

-- =====================================================
-- 2. INSERTAR USUARIOS
-- =====================================================
-- Verificar que las sucursales existan antes de insertar usuarios
IF EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Usuarios (Nombre, Email, PasswordHash, Rol, SucursalId) VALUES
    ('Juan Carlos Pérez', 'juan.perez@empresa.com', 'hash_password_123', 'Administrador', 1),
    ('María González', 'maria.gonzalez@empresa.com', 'hash_password_456', 'Vendedor', 1),
    ('Ana Martínez', 'ana.martinez@empresa.com', 'hash_password_101', 'Contador', 1);
END

IF EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Usuarios (Nombre, Email, PasswordHash, Rol, SucursalId) VALUES
    ('Carlos Rodríguez', 'carlos.rodriguez@empresa.com', 'hash_password_789', 'Vendedor', 2);
END

IF EXISTS (SELECT 1 FROM Sucursales WHERE Id = 3)
BEGIN
    INSERT INTO Usuarios (Nombre, Email, PasswordHash, Rol, SucursalId) VALUES
    ('Luis Fernández', 'luis.fernandez@empresa.com', 'hash_password_202', 'Vendedor', 3);
END

IF EXISTS (SELECT 1 FROM Sucursales WHERE Id = 4)
BEGIN
    INSERT INTO Usuarios (Nombre, Email, PasswordHash, Rol, SucursalId) VALUES
    ('Carmen Silva', 'carmen.silva@empresa.com', 'hash_password_303', 'Vendedor', 4);
END

-- =====================================================
-- 3. INSERTAR CATEGORÍAS DE PRODUCTOS
-- =====================================================
INSERT INTO CategoriasProductos (Nombre, Descripcion) VALUES
('Electrónicos', 'Dispositivos electrónicos y tecnología'),
('Ropa', 'Vestimenta para todas las edades'),
('Hogar', 'Artículos para el hogar y decoración'),
('Deportes', 'Equipamiento y ropa deportiva'),
('Libros', 'Libros de todas las categorías'),
('Juguetes', 'Juguetes para niños y coleccionistas'),
('Automotriz', 'Accesorios y repuestos para vehículos'),
('Jardín', 'Productos para jardín y exteriores');

-- =====================================================
-- 4. INSERTAR IMPUESTOS
-- =====================================================
INSERT INTO Impuestos (Nombre, Porcentaje, Pais) VALUES
('IVA', 16.00, 'México'),
('IGV', 18.00, 'Perú'),
('GST', 10.00, 'Canadá'),
('VAT', 20.00, 'Reino Unido'),
('Sales Tax', 8.25, 'Estados Unidos');

-- =====================================================
-- 5. INSERTAR TIPOS DE GASTO
-- =====================================================
INSERT INTO TiposGasto (Nombre, Descripcion) VALUES
('Alquiler', 'Pagos de renta de locales'),
('Sueldos', 'Salarios del personal'),
('Marketing', 'Publicidad y promoción'),
('Servicios Públicos', 'Luz, agua, gas, internet'),
('Mantenimiento', 'Reparaciones y mantenimiento'),
('Seguros', 'Pólizas de seguro'),
('Impuestos', 'Pagos de impuestos gubernamentales'),
('Otros', 'Otros gastos operativos');

-- =====================================================
-- 6. INSERTAR MÉTODOS DE PAGO
-- =====================================================
INSERT INTO MetodosPago (Nombre, Descripcion) VALUES
('Efectivo', 'Pago en efectivo'),
('Tarjeta de Crédito', 'Pago con tarjeta de crédito'),
('Tarjeta de Débito', 'Pago con tarjeta de débito'),
('Transferencia', 'Transferencia bancaria'),
('Cheque', 'Pago con cheque'),
('PayPal', 'Pago electrónico PayPal'),
('Mercado Pago', 'Pago a través de Mercado Pago');

-- =====================================================
-- 7. INSERTAR CLIENTES
-- =====================================================
INSERT INTO Clientes (Nombre, RFC, Email, Telefono, Direccion, EsActivo, FechaRegistro) VALUES
('Empresa ABC S.A. de C.V.', 'ABC123456789', 'contacto@abc.com', '55-1234-5678', 'Av. Insurgentes Sur 1234, Col. Del Valle, CDMX', 1, '2024-01-15'),
('María Elena López', 'LOME800315ABC', 'maria.lopez@email.com', '55-9876-5432', 'Calle Morelos 567, Col. Centro, Guadalajara', 1, '2024-01-20'),
('Carlos Alberto Ruiz', 'RUCA750210DEF', 'carlos.ruiz@email.com', '81-5555-1234', 'Blvd. Universidad 890, Col. Norte, Monterrey', 1, '2024-02-01'),
('Restaurante El Sabor', 'SAB890123456', 'info@elsabor.com', '55-4444-7777', 'Av. Cuauhtémoc 321, Col. Roma, CDMX', 1, '2024-02-10'),
('Hotel Plaza Central', 'PLA670512789', 'reservas@plazacentral.com', '33-3333-4444', 'Av. Hidalgo 456, Col. Centro, Guadalajara', 1, '2024-02-15'),
('Farmacia San José', 'SAN450623012', 'ventas@farmaciasanjose.com', '81-7777-8888', 'Calle Zaragoza 123, Col. Sur, Monterrey', 1, '2024-03-01'),
('Escuela Primaria Libertad', 'LIB780901345', 'admin@libertad.edu.mx', '55-6666-9999', 'Av. Revolución 789, Col. San Ángel, CDMX', 1, '2024-03-10'),
('Taller Mecánico Rápido', 'RAP560712678', 'servicio@tallerrapido.com', '33-2222-3333', 'Calle Independencia 234, Col. Industrial, Guadalajara', 1, '2024-03-15'),
('Papelería El Estudiante', 'EST340823901', 'ventas@elestudiante.com', '81-8888-9999', 'Blvd. Constitución 567, Col. Centro, Monterrey', 1, '2024-04-01'),
('Gimnasio Power Fit', 'POW670934234', 'info@powerfit.com', '55-5555-6666', 'Av. Patriotismo 890, Col. Del Valle, CDMX', 1, '2024-04-10');

-- =====================================================
-- 8. INSERTAR PROVEEDORES
-- =====================================================
INSERT INTO Proveedores (Nombre, RFC, Email, Telefono, CuentaBancaria) VALUES
('Distribuidora Nacional S.A.', 'DIN123456789', 'ventas@distribuidoranacional.com', '55-1111-2222', '012345678901234567'),
('Electrónicos del Norte', 'ELE234567890', 'compras@electronicosnorte.com', '81-2222-3333', '123456789012345678'),
('Textiles Mexicanos', 'TEX345678901', 'pedidos@textilesmexicanos.com', '33-3333-4444', '234567890123456789'),
('Importadora del Pacífico', 'IMP456789012', 'contacto@importadorapacifico.com', '55-4444-5555', '345678901234567890'),
('Suministros Industriales', 'SUM567890123', 'ventas@suministrosindustriales.com', '81-5555-6666', '456789012345678901'),
('Papel y Cartón Express', 'PAP678901234', 'pedidos@papelcartonexpress.com', '33-6666-7777', '567890123456789012'),
('Herramientas Profesionales', 'HER789012345', 'compras@herramientasprofesionales.com', '55-7777-8888', '678901234567890123'),
('Alimentos Frescos S.A.', 'ALI890123456', 'ventas@alimentosfrescos.com', '81-8888-9999', '789012345678901234'),
('Servicios de Limpieza Pro', 'SER901234567', 'contacto@limpiezapro.com', '33-9999-0000', '890123456789012345'),
('Equipos de Cómputo', 'EQU012345678', 'ventas@equiposcomputo.com', '55-0000-1111', '901234567890123456');

-- =====================================================
-- 9. INSERTAR PRODUCTOS
-- =====================================================
-- Verificar que las categorías e impuestos existan antes de insertar productos
IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Laptop HP Pavilion', 'LAP-001', 8500.00, 12999.00, 5, 1, 1),
    ('Mouse Inalámbrico Logitech', 'MOU-001', 150.00, 299.00, 10, 1, 1),
    ('Teclado Mecánico RGB', 'TEC-001', 800.00, 1499.00, 8, 1, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 2) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Camiseta de Algodón', 'CAM-001', 120.00, 299.00, 20, 2, 1),
    ('Jeans Clásicos', 'JEA-001', 350.00, 799.00, 15, 2, 1),
    ('Sudadera con Capucha', 'SUD-001', 280.00, 599.00, 12, 2, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Lámpara de Mesa LED', 'LAM-001', 180.00, 399.00, 8, 3, 1),
    ('Juego de Sábanas', 'SAB-001', 220.00, 499.00, 10, 3, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 4) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Pelota de Fútbol', 'PEL-001', 95.00, 199.00, 15, 4, 1),
    ('Raqueta de Tenis', 'RAQ-001', 450.00, 899.00, 8, 4, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Libro "El Principito"', 'LIB-001', 85.00, 179.00, 25, 5, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 6) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Rompecabezas 1000 piezas', 'ROM-001', 120.00, 249.00, 12, 6, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 7) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Aceite de Motor 5W-30', 'ACE-001', 180.00, 299.00, 8, 7, 1),
    ('Filtro de Aire', 'FIL-001', 45.00, 89.00, 20, 7, 1);
END

IF EXISTS (SELECT 1 FROM CategoriasProductos WHERE Id = 8) AND EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
    ('Maceta de Cerámica', 'MAC-001', 95.00, 199.00, 15, 8, 1),
    ('Fertilizante Orgánico', 'FER-001', 75.00, 149.00, 10, 8, 1);
END

-- =====================================================
-- 10. INSERTAR SERVICIOS
-- =====================================================
-- Verificar que los impuestos existan antes de insertar servicios
IF EXISTS (SELECT 1 FROM Impuestos WHERE Id = 1)
BEGIN
    INSERT INTO Servicios (Nombre, Precio, Descripcion, ImpuestoId) VALUES
    ('Instalación de Software', 500.00, 'Instalación y configuración de software en computadoras', 1),
    ('Mantenimiento de Equipos', 800.00, 'Servicio de mantenimiento preventivo y correctivo', 1),
    ('Diseño Gráfico', 1200.00, 'Diseño de logos, folletos y material publicitario', 1),
    ('Consultoría IT', 1500.00, 'Asesoría en tecnología de la información', 1),
    ('Reparación de Hardware', 600.00, 'Reparación de componentes de computadora', 1),
    ('Capacitación de Personal', 800.00, 'Entrenamiento en uso de sistemas y software', 1),
    ('Soporte Técnico', 400.00, 'Soporte técnico remoto y presencial', 1),
    ('Migración de Datos', 2000.00, 'Migración de datos entre sistemas', 1);
END

-- =====================================================
-- 11. INSERTAR SUCURSALES (verificar que no existan duplicados)
-- =====================================================
-- Las sucursales ya fueron insertadas en el paso 1

-- =====================================================
-- 12. INSERTAR REPORTES MENSUALES
-- =====================================================
-- Verificar que las sucursales existan antes de insertar reportes
IF EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO ReportesMensuales (Mes, Anio, TotalVentas, TotalGastos, UtilidadNeta, SucursalId) VALUES
    (1, 2024, 125000.00, 85000.00, 40000.00, 1),
    (2, 2024, 138000.00, 92000.00, 46000.00, 1),
    (3, 2024, 145000.00, 98000.00, 47000.00, 1),
    (4, 2024, 132000.00, 89000.00, 43000.00, 1),
    (5, 2024, 158000.00, 105000.00, 53000.00, 1),
    (6, 2024, 142000.00, 95000.00, 47000.00, 1);
END

IF EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO ReportesMensuales (Mes, Anio, TotalVentas, TotalGastos, UtilidadNeta, SucursalId) VALUES
    (1, 2024, 98000.00, 65000.00, 33000.00, 2),
    (2, 2024, 105000.00, 70000.00, 35000.00, 2),
    (3, 2024, 112000.00, 75000.00, 37000.00, 2),
    (4, 2024, 98000.00, 68000.00, 30000.00, 2),
    (5, 2024, 118000.00, 78000.00, 40000.00, 2),
    (6, 2024, 108000.00, 72000.00, 36000.00, 2);
END

-- =====================================================
-- 13. INSERTAR PRESUPUESTOS
-- =====================================================
INSERT INTO Presupuestos (Fecha, ClienteId, Total, Estado) VALUES
('2024-01-20', 1, 25000.00, 'Aprobado'),
('2024-02-05', 2, 15000.00, 'Pendiente'),
('2024-02-15', 3, 32000.00, 'Aprobado'),
('2024-03-01', 4, 18000.00, 'Rechazado'),
('2024-03-10', 5, 45000.00, 'Aprobado'),
('2024-03-20', 6, 22000.00, 'Pendiente'),
('2024-04-05', 7, 28000.00, 'Aprobado'),
('2024-04-15', 8, 19000.00, 'Pendiente'),
('2024-05-01', 9, 35000.00, 'Aprobado'),
('2024-05-10', 10, 26000.00, 'Pendiente');

-- =====================================================
-- 14. INSERTAR PEDIDOS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar pedidos
IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 1) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-01-25', 1, 1, 1, 'Completado'),
    ('2024-02-10', 2, 2, 1, 'En Proceso');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 3) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-02-20', 3, 3, 2, 'Completado');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 4) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 4) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-03-05', 4, 4, 1, 'Cancelado');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 5) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 3)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-03-15', 5, 5, 3, 'Completado');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 6) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 6) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-03-25', 6, 6, 2, 'En Proceso');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 7) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 7) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-04-10', 7, 7, 1, 'Completado');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 8) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 8) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 4)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-04-20', 8, 8, 4, 'En Proceso');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 9) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 9) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-05-05', 9, 9, 2, 'Completado');
END

IF EXISTS (SELECT 1 FROM Clientes WHERE Id = 10) AND EXISTS (SELECT 1 FROM Presupuestos WHERE Id = 10) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 3)
BEGIN
    INSERT INTO Pedidos (Fecha, ClienteId, PresupuestoId, SucursalId, Estado) VALUES
    ('2024-05-15', 10, 10, 3, 'En Proceso');
END

-- =====================================================
-- 15. INSERTAR COMPRAS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar compras
IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 1) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-01-10', 'FAC-001-2024', 1, 1, 45000.00, 'Pagada'),
    ('2024-01-25', 'FAC-002-2024', 2, 1, 32000.00, 'Pagada');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 3) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-02-10', 'FAC-003-2024', 3, 2, 28000.00, 'Pagada');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 4) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-02-25', 'FAC-004-2024', 4, 1, 55000.00, 'Pendiente');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 5) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 3)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-03-10', 'FAC-005-2024', 5, 3, 38000.00, 'Pagada');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 6) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-03-25', 'FAC-006-2024', 6, 2, 22000.00, 'Pagada');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 7) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-04-10', 'FAC-007-2024', 7, 1, 42000.00, 'Pendiente');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 8) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 4)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-04-25', 'FAC-008-2024', 8, 4, 29000.00, 'Pagada');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 9) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-05-10', 'FAC-009-2024', 9, 2, 35000.00, 'Pendiente');
END

IF EXISTS (SELECT 1 FROM Proveedores WHERE Id = 10) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 3)
BEGIN
    INSERT INTO Compras (Fecha, NumeroFactura, ProveedorId, SucursalId, Total, Estado) VALUES
    ('2024-05-25', 'FAC-010-2024', 10, 3, 48000.00, 'Pagada');
END

-- =====================================================
-- 16. INSERTAR GASTOS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar gastos
IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 1)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-01-05', 15000.00, 'Renta del local principal', 1, NULL),
    ('2024-04-05', 15000.00, 'Renta del local norte', 1, NULL);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 2)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-01-15', 25000.00, 'Nómina del personal', 2, NULL),
    ('2024-04-15', 18000.00, 'Nómina del personal norte', 2, NULL);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 3) AND EXISTS (SELECT 1 FROM Proveedores WHERE Id = 9)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-01-25', 8000.00, 'Publicidad en redes sociales', 3, 9);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 4)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-02-05', 5000.00, 'Servicios de luz y agua', 4, NULL);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 5) AND EXISTS (SELECT 1 FROM Proveedores WHERE Id = 7)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-02-15', 12000.00, 'Mantenimiento de equipos', 5, 7);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 6)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-02-25', 8000.00, 'Póliza de seguro anual', 6, NULL);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 7)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-03-05', 15000.00, 'Pago de IVA', 7, NULL);
END

IF EXISTS (SELECT 1 FROM TiposGasto WHERE Id = 8) AND EXISTS (SELECT 1 FROM Proveedores WHERE Id = 6)
BEGIN
    INSERT INTO Gastos (Fecha, Monto, Descripcion, TipoGastoId, ProveedorId) VALUES
    ('2024-03-15', 3000.00, 'Material de oficina', 8, 6);
END

-- =====================================================
-- 17. INSERTAR VENTAS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar ventas
IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 1) AND EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 1)
BEGIN
    INSERT INTO Ventas (Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId) VALUES
    ('2024-01-26', 1, 1, 25000.00, 'Completada', NULL);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 3) AND EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 2)
BEGIN
    INSERT INTO Ventas (Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId) VALUES
    ('2024-02-21', 3, 2, 32000.00, 'Completada', NULL);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 5) AND EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 3)
BEGIN
    INSERT INTO Ventas (Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId) VALUES
    ('2024-03-16', 5, 3, 45000.00, 'Completada', NULL);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 7) AND EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 1)
BEGIN
    INSERT INTO Ventas (Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId) VALUES
    ('2024-04-11', 7, 1, 28000.00, 'Completada', NULL);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 9) AND EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 2)
BEGIN
    INSERT INTO Ventas (Fecha, PedidoId, MetodoPagoId, Total, Estado, FacturaId) VALUES
    ('2024-05-06', 9, 2, 35000.00, 'Completada', NULL);
END

-- =====================================================
-- 18. INSERTAR FACTURAS
-- =====================================================
INSERT INTO Facturas (Fecha, NumeroFactura, ClienteId, Total, Estado) VALUES
('2024-01-26', 'FAC-001-2024', 1, 25000.00, 'Pagada'),
('2024-02-21', 'FAC-002-2024', 3, 32000.00, 'Pagada'),
('2024-03-16', 'FAC-003-2024', 5, 45000.00, 'Pagada'),
('2024-04-11', 'FAC-004-2024', 7, 28000.00, 'Pagada'),
('2024-05-06', 'FAC-005-2024', 9, 35000.00, 'Pagada');

-- Actualizar las ventas con las facturas correspondientes
UPDATE Ventas SET FacturaId = 1 WHERE Id = 1;
UPDATE Ventas SET FacturaId = 2 WHERE Id = 2;
UPDATE Ventas SET FacturaId = 3 WHERE Id = 3;
UPDATE Ventas SET FacturaId = 4 WHERE Id = 4;
UPDATE Ventas SET FacturaId = 5 WHERE Id = 5;

-- =====================================================
-- 19. INSERTAR DETALLES DE PEDIDOS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar detalles
IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 2)
BEGIN
    INSERT INTO DetallesPedido (PedidoId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (1, 1, 2, 12999.00),
    (1, 2, 5, 299.00);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 4)
BEGIN
    INSERT INTO DetallesPedido (PedidoId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (3, 3, 3, 1499.00),
    (3, 4, 10, 299.00);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 6)
BEGIN
    INSERT INTO DetallesPedido (PedidoId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (5, 5, 8, 799.00),
    (5, 6, 12, 599.00);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 7) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 7) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 8)
BEGIN
    INSERT INTO DetallesPedido (PedidoId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (7, 7, 4, 399.00),
    (7, 8, 6, 499.00);
END

IF EXISTS (SELECT 1 FROM Pedidos WHERE Id = 9) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 9) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 10)
BEGIN
    INSERT INTO DetallesPedido (PedidoId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (9, 9, 15, 199.00),
    (9, 10, 5, 899.00);
END

-- =====================================================
-- 20. INSERTAR DETALLES DE FACTURAS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar detalles
IF EXISTS (SELECT 1 FROM Facturas WHERE Id = 1) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 2)
BEGIN
    INSERT INTO DetallesFactura (FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario) VALUES
    (1, 1, NULL, 2, 12999.00),
    (1, 2, NULL, 5, 299.00);
END

IF EXISTS (SELECT 1 FROM Facturas WHERE Id = 2) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 4)
BEGIN
    INSERT INTO DetallesFactura (FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario) VALUES
    (2, 3, NULL, 3, 1499.00),
    (2, 4, NULL, 10, 299.00);
END

IF EXISTS (SELECT 1 FROM Facturas WHERE Id = 3) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 6)
BEGIN
    INSERT INTO DetallesFactura (FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario) VALUES
    (3, 5, NULL, 8, 799.00),
    (3, 6, NULL, 12, 599.00);
END

IF EXISTS (SELECT 1 FROM Facturas WHERE Id = 4) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 7) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 8)
BEGIN
    INSERT INTO DetallesFactura (FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario) VALUES
    (4, 7, NULL, 4, 399.00),
    (4, 8, NULL, 6, 499.00);
END

IF EXISTS (SELECT 1 FROM Facturas WHERE Id = 5) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 9) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 10)
BEGIN
    INSERT INTO DetallesFactura (FacturaId, ProductoId, ServicioId, Cantidad, PrecioUnitario) VALUES
    (5, 9, NULL, 15, 199.00),
    (5, 10, NULL, 5, 899.00);
END

-- =====================================================
-- 21. INSERTAR DETALLES DE COMPRAS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar detalles
IF EXISTS (SELECT 1 FROM Compras WHERE Id = 1) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 2)
BEGIN
    INSERT INTO DetallesCompra (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (1, 1, 10, 8500.00),
    (1, 2, 50, 150.00);
END

IF EXISTS (SELECT 1 FROM Compras WHERE Id = 2) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 4)
BEGIN
    INSERT INTO DetallesCompra (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (2, 3, 20, 800.00),
    (2, 4, 100, 120.00);
END

IF EXISTS (SELECT 1 FROM Compras WHERE Id = 3) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 6)
BEGIN
    INSERT INTO DetallesCompra (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (3, 5, 80, 350.00),
    (3, 6, 60, 280.00);
END

IF EXISTS (SELECT 1 FROM Compras WHERE Id = 4) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 7) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 8)
BEGIN
    INSERT INTO DetallesCompra (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (4, 7, 30, 180.00),
    (4, 8, 40, 220.00);
END

IF EXISTS (SELECT 1 FROM Compras WHERE Id = 5) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 9) AND EXISTS (SELECT 1 FROM Productos WHERE Id = 10)
BEGIN
    INSERT INTO DetallesCompra (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES
    (5, 9, 100, 95.00),
    (5, 10, 25, 450.00);
END

-- =====================================================
-- 22. INSERTAR MOVIMIENTOS DE INVENTARIO
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar movimientos
IF EXISTS (SELECT 1 FROM Productos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 1)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-01-10', 1, 1, 10, 'Entrada', 'Compra FAC-001-2024', NULL, 1);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 2) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 1)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-01-10', 2, 1, 50, 'Entrada', 'Compra FAC-001-2024', NULL, 1);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 2)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-01-25', 3, 1, 20, 'Entrada', 'Compra FAC-002-2024', NULL, 2);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 4) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 2)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-01-25', 4, 1, 100, 'Entrada', 'Compra FAC-002-2024', NULL, 2);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 1) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1) AND EXISTS (SELECT 1 FROM Ventas WHERE Id = 1)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-01-26', 1, 1, -2, 'Salida', 'Venta PED-001', 1, NULL);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 2) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 1) AND EXISTS (SELECT 1 FROM Ventas WHERE Id = 1)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-01-26', 2, 1, -5, 'Salida', 'Venta PED-001', 1, NULL);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 5) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 3)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-02-10', 5, 2, 80, 'Entrada', 'Compra FAC-003-2024', NULL, 3);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 6) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 3)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-02-10', 6, 2, 60, 'Entrada', 'Compra FAC-003-2024', NULL, 3);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 3) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2) AND EXISTS (SELECT 1 FROM Ventas WHERE Id = 2)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-02-21', 3, 2, -3, 'Salida', 'Venta PED-003', 2, NULL);
END

IF EXISTS (SELECT 1 FROM Productos WHERE Id = 4) AND EXISTS (SELECT 1 FROM Sucursales WHERE Id = 2) AND EXISTS (SELECT 1 FROM Ventas WHERE Id = 2)
BEGIN
    INSERT INTO MovimientosInventario (Fecha, ProductoId, SucursalId, Cantidad, Tipo, Referencia, VentaId, CompraId) VALUES
    ('2024-02-21', 4, 2, -10, 'Salida', 'Venta PED-003', 2, NULL);
END

-- =====================================================
-- 23. INSERTAR PAGOS
-- =====================================================
-- Verificar que las entidades dependientes existan antes de insertar pagos
IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 1)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-01-15', 45000.00, 4, NULL, 1, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 2)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-01-30', 32000.00, 4, NULL, 2, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 3)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-02-15', 28000.00, 4, NULL, 3, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 5)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-03-15', 38000.00, 4, NULL, 5, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 6)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-03-30', 22000.00, 4, NULL, 6, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 8)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-04-30', 29000.00, 4, NULL, 8, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 4) AND EXISTS (SELECT 1 FROM Compras WHERE Id = 10)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-05-30', 48000.00, 4, NULL, 10, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 1) AND EXISTS (SELECT 1 FROM Facturas WHERE Id = 1)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-01-26', 25000.00, 1, 1, NULL, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 2) AND EXISTS (SELECT 1 FROM Facturas WHERE Id = 2)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-02-21', 32000.00, 2, 2, NULL, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 3) AND EXISTS (SELECT 1 FROM Facturas WHERE Id = 3)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-03-16', 45000.00, 3, 3, NULL, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 1) AND EXISTS (SELECT 1 FROM Facturas WHERE Id = 4)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-04-11', 28000.00, 1, 4, NULL, NULL);
END

IF EXISTS (SELECT 1 FROM MetodosPago WHERE Id = 2) AND EXISTS (SELECT 1 FROM Facturas WHERE Id = 5)
BEGIN
    INSERT INTO Pagos (Fecha, Monto, MetodoPagoId, FacturaId, CompraId, GastoId) VALUES
    ('2024-05-06', 35000.00, 2, 5, NULL, NULL);
END

-- =====================================================
-- VERIFICACIÓN FINAL
-- =====================================================
PRINT '=== VERIFICACIÓN DE DATOS INSERTADOS ===';

-- Verificar totales usando variables
DECLARE @TotalSucursales INT, @TotalUsuarios INT, @TotalClientes INT, @TotalProveedores INT;
DECLARE @TotalProductos INT, @TotalServicios INT, @TotalPresupuestos INT, @TotalPedidos INT;
DECLARE @TotalVentas INT, @TotalFacturas INT, @TotalCompras INT, @TotalGastos INT;
DECLARE @TotalPagos INT, @TotalMovimientos INT;

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

PRINT '==========================================';
PRINT 'Script ejecutado exitosamente.';
PRINT 'La base de datos ahora contiene datos de prueba realistas.';
PRINT '==========================================';
