-- =====================================================
-- SCRIPT DE DATOS DUMMY PARA BASE DE DATOS MANAGEMENTBUSINESS
-- =====================================================
-- Este script inserta datos de prueba realistas para desarrollo y testing
-- Funciona con IDs auto-incrementales (IDENTITY) - NO requiere IDs explícitos
-- Ejecutar en SQL Server Management Studio en la base de datos ManagementBusiness
-- =====================================================

USE ManagementBusiness;
GO

-- =====================================================
-- 1. INSERTAR CLIENTES
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
-- 2. INSERTAR PROVEEDORES
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
-- 3. INSERTAR USUARIOS
-- =====================================================
-- Obtener IDs de sucursales para insertar usuarios
DECLARE @SucursalCentroId INT, @SucursalNorteId INT, @SucursalSurId INT, @SucursalEsteId INT;

SELECT @SucursalCentroId = Id FROM Sucursales WHERE Nombre = 'Sucursal Centro';
SELECT @SucursalNorteId = Id FROM Sucursales WHERE Nombre = 'Sucursal Norte';
SELECT @SucursalSurId = Id FROM Sucursales WHERE Nombre = 'Sucursal Sur';
SELECT @SucursalEsteId = Id FROM Sucursales WHERE Nombre = 'Sucursal Este';

INSERT INTO Usuarios (Nombre, Email, PasswordHash, Rol, SucursalId) VALUES
('Juan Carlos Pérez', 'juan.perez@empresa.com', 'hash_password_123', 'Administrador', @SucursalCentroId),
('María González', 'maria.gonzalez@empresa.com', 'hash_password_456', 'Vendedor', @SucursalCentroId),
('Ana Martínez', 'ana.martinez@empresa.com', 'hash_password_101', 'Contador', @SucursalCentroId),
('Carlos Rodríguez', 'carlos.rodriguez@empresa.com', 'hash_password_789', 'Vendedor', @SucursalNorteId),
('Luis Fernández', 'luis.fernandez@empresa.com', 'hash_password_202', 'Vendedor', @SucursalSurId),
('Carmen Silva', 'carmen.silva@empresa.com', 'hash_password_303', 'Vendedor', @SucursalEsteId);

-- =====================================================
-- 4. INSERTAR PRODUCTOS
-- =====================================================
-- Obtener IDs de categorías e impuestos
DECLARE @CategoriaElectronicaId INT, @CategoriaRopaId INT, @CategoriaHogarId INT, @CategoriaDeportesId INT;
DECLARE @CategoriaLibrosId INT, @CategoriaJuguetesId INT, @CategoriaAutomotrizId INT, @CategoriaJardinId INT;
DECLARE @ImpuestoIVAId INT;

SELECT @CategoriaElectronicaId = Id FROM CategoriasProductos WHERE Nombre = 'Electrónicos';
SELECT @CategoriaRopaId = Id FROM CategoriasProductos WHERE Nombre = 'Ropa';
SELECT @CategoriaHogarId = Id FROM CategoriasProductos WHERE Nombre = 'Hogar';
SELECT @CategoriaDeportesId = Id FROM CategoriasProductos WHERE Nombre = 'Deportes';
SELECT @CategoriaLibrosId = Id FROM CategoriasProductos WHERE Nombre = 'Libros';
SELECT @CategoriaJuguetesId = Id FROM CategoriasProductos WHERE Nombre = 'Juguetes';
SELECT @CategoriaAutomotrizId = Id FROM CategoriasProductos WHERE Nombre = 'Automotriz';
SELECT @CategoriaJardinId = Id FROM CategoriasProductos WHERE Nombre = 'Jardín';
SELECT @ImpuestoIVAId = Id FROM Impuestos WHERE Nombre = 'IVA';

INSERT INTO Productos (Nombre, SKU, PrecioCosto, PrecioVenta, StockMinimo, CategoriaId, ImpuestoId) VALUES
-- Electrónicos
('Laptop HP Pavilion', 'LAP-001', 8500.00, 12999.00, 5, @CategoriaElectronicaId, @ImpuestoIVAId),
('Mouse Inalámbrico Logitech', 'MOU-001', 150.00, 299.00, 10, @CategoriaElectronicaId, @ImpuestoIVAId),
('Teclado Mecánico RGB', 'TEC-001', 800.00, 1499.00, 8, @CategoriaElectronicaId, @ImpuestoIVAId),
-- Ropa
('Camiseta de Algodón', 'CAM-001', 120.00, 299.00, 20, @CategoriaRopaId, @ImpuestoIVAId),
('Jeans Clásicos', 'JEA-001', 350.00, 799.00, 15, @CategoriaRopaId, @ImpuestoIVAId),
('Sudadera con Capucha', 'SUD-001', 280.00, 599.00, 12, @CategoriaRopaId, @ImpuestoIVAId),
-- Hogar
('Lámpara de Mesa LED', 'LAM-001', 180.00, 399.00, 8, @CategoriaHogarId, @ImpuestoIVAId),
('Juego de Sábanas', 'SAB-001', 220.00, 499.00, 10, @CategoriaHogarId, @ImpuestoIVAId),
-- Deportes
('Pelota de Fútbol', 'PEL-001', 95.00, 199.00, 15, @CategoriaDeportesId, @ImpuestoIVAId),
('Raqueta de Tenis', 'RAQ-001', 450.00, 899.00, 8, @CategoriaDeportesId, @ImpuestoIVAId),
-- Libros
('Libro "El Principito"', 'LIB-001', 85.00, 179.00, 25, @CategoriaLibrosId, @ImpuestoIVAId),
-- Juguetes
('Rompecabezas 1000 piezas', 'ROM-001', 120.00, 249.00, 12, @CategoriaJuguetesId, @ImpuestoIVAId),
-- Automotriz
('Aceite de Motor 5W-30', 'ACE-001', 180.00, 299.00, 8, @CategoriaAutomotrizId, @ImpuestoIVAId),
('Filtro de Aire', 'FIL-001', 45.00, 89.00, 20, @CategoriaAutomotrizId, @ImpuestoIVAId),
-- Jardín
('Maceta de Cerámica', 'MAC-001', 95.00, 199.00, 15, @CategoriaJardinId, @ImpuestoIVAId),
('Fertilizante Orgánico', 'FER-001', 75.00, 149.00, 10, @CategoriaJardinId, @ImpuestoIVAId);

-- =====================================================
-- 5. INSERTAR SERVICIOS
-- =====================================================
INSERT INTO Servicios (Nombre, Precio, Descripcion, ImpuestoId) VALUES
('Instalación de Software', 500.00, 'Instalación y configuración de software en computadoras', @ImpuestoIVAId),
('Mantenimiento de Equipos', 800.00, 'Servicio de mantenimiento preventivo y correctivo', @ImpuestoIVAId),
('Diseño Gráfico', 1200.00, 'Diseño de logos, folletos y material publicitario', @ImpuestoIVAId),
('Consultoría IT', 1500.00, 'Asesoría en tecnología de la información', @ImpuestoIVAId),
('Reparación de Hardware', 600.00, 'Reparación de componentes de computadora', @ImpuestoIVAId),
('Capacitación de Personal', 800.00, 'Entrenamiento en uso de sistemas y software', @ImpuestoIVAId),
('Soporte Técnico', 400.00, 'Soporte técnico remoto y presencial', @ImpuestoIVAId),
('Migración de Datos', 2000.00, 'Migración de datos entre sistemas', @ImpuestoIVAId);

PRINT '=== PRIMERA PARTE COMPLETADA ===';
PRINT 'Clientes, Proveedores, Usuarios, Productos y Servicios insertados exitosamente.';
GO
