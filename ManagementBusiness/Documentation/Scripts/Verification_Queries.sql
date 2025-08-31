-- =====================================================
-- CONSULTAS DE VERIFICACIÓN PARA DATOS DUMMY
-- =====================================================
-- Este script contiene consultas útiles para verificar que los datos
-- se insertaron correctamente en la base de datos
-- =====================================================

USE ManagementBusiness;
GO

-- =====================================================
-- 1. VERIFICAR TOTALES POR ENTIDAD
-- =====================================================
PRINT '=== TOTALES POR ENTIDAD ===';

SELECT 'Sucursales' AS Entidad, COUNT(*) AS Total FROM Sucursales
UNION ALL
SELECT 'Usuarios', COUNT(*) FROM Usuarios
UNION ALL
SELECT 'Clientes', COUNT(*) FROM Clientes
UNION ALL
SELECT 'Proveedores', COUNT(*) FROM Proveedores
UNION ALL
SELECT 'Productos', COUNT(*) FROM Productos
UNION ALL
SELECT 'Servicios', COUNT(*) FROM Servicios
UNION ALL
SELECT 'Presupuestos', COUNT(*) FROM Presupuestos
UNION ALL
SELECT 'Pedidos', COUNT(*) FROM Pedidos
UNION ALL
SELECT 'Ventas', COUNT(*) FROM Ventas
UNION ALL
SELECT 'Facturas', COUNT(*) FROM Facturas
UNION ALL
SELECT 'Compras', COUNT(*) FROM Compras
UNION ALL
SELECT 'Gastos', COUNT(*) FROM Gastos
UNION ALL
SELECT 'Pagos', COUNT(*) FROM Pagos
UNION ALL
SELECT 'Movimientos Inventario', COUNT(*) FROM MovimientosInventario;

-- =====================================================
-- 2. VERIFICAR RELACIONES ENTRE ENTIDADES
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN DE RELACIONES ===';

-- Clientes con sus facturas
SELECT 
    c.Nombre AS Cliente,
    COUNT(f.Id) AS TotalFacturas,
    SUM(f.Total) AS TotalFacturado
FROM Clientes c
LEFT JOIN Facturas f ON c.Id = f.ClienteId
GROUP BY c.Id, c.Nombre
ORDER BY TotalFacturado DESC;

-- Proveedores con sus compras
SELECT 
    p.Nombre AS Proveedor,
    COUNT(c.Id) AS TotalCompras,
    SUM(c.Total) AS TotalComprado
FROM Proveedores p
LEFT JOIN Compras c ON p.Id = c.ProveedorId
GROUP BY p.Id, p.Nombre
ORDER BY TotalComprado DESC;

-- Productos por categoría
SELECT 
    cp.Nombre AS Categoria,
    COUNT(p.Id) AS TotalProductos,
    AVG(p.PrecioVenta) AS PrecioPromedio
FROM CategoriasProductos cp
LEFT JOIN Productos p ON cp.Id = p.CategoriaId
GROUP BY cp.Id, cp.Nombre
ORDER BY TotalProductos DESC;

-- =====================================================
-- 3. VERIFICAR DATOS FINANCIEROS
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN FINANCIERA ===';

-- Resumen de ventas por mes
SELECT 
    MONTH(v.Fecha) AS Mes,
    YEAR(v.Fecha) AS Año,
    COUNT(v.Id) AS TotalVentas,
    SUM(v.Total) AS MontoTotal,
    AVG(v.Total) AS PromedioVenta
FROM Ventas v
GROUP BY MONTH(v.Fecha), YEAR(v.Fecha)
ORDER BY Año, Mes;

-- Resumen de compras por proveedor
SELECT 
    p.Nombre AS Proveedor,
    COUNT(c.Id) AS TotalCompras,
    SUM(c.Total) AS MontoTotal,
    AVG(c.Total) AS PromedioCompra
FROM Compras c
JOIN Proveedores p ON c.ProveedorId = p.Id
GROUP BY p.Id, p.Nombre
ORDER BY MontoTotal DESC;

-- Gastos por tipo
SELECT 
    tg.Nombre AS TipoGasto,
    COUNT(g.Id) AS TotalGastos,
    SUM(g.Monto) AS MontoTotal,
    AVG(g.Monto) AS PromedioGasto
FROM Gastos g
JOIN TiposGasto tg ON g.TipoGastoId = tg.Id
GROUP BY tg.Id, tg.Nombre
ORDER BY MontoTotal DESC;

-- =====================================================
-- 4. VERIFICAR INVENTARIO
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN DE INVENTARIO ===';

-- Stock actual por producto (calculado desde movimientos)
SELECT 
    p.Nombre AS Producto,
    p.SKU,
    p.StockMinimo,
    COALESCE(SUM(mi.Cantidad), 0) AS StockActual
FROM Productos p
LEFT JOIN MovimientosInventario mi ON p.Id = mi.ProductoId
GROUP BY p.Id, p.Nombre, p.SKU, p.StockMinimo
ORDER BY StockActual ASC;

-- Productos con stock bajo
SELECT 
    p.Nombre AS Producto,
    p.SKU,
    p.StockMinimo,
    COALESCE(SUM(mi.Cantidad), 0) AS StockActual,
    CASE 
        WHEN COALESCE(SUM(mi.Cantidad), 0) <= p.StockMinimo THEN 'STOCK BAJO'
        ELSE 'OK'
    END AS Estado
FROM Productos p
LEFT JOIN MovimientosInventario mi ON p.Id = mi.ProductoId
GROUP BY p.Id, p.Nombre, p.SKU, p.StockMinimo
HAVING COALESCE(SUM(mi.Cantidad), 0) <= p.StockMinimo
ORDER BY StockActual ASC;

-- =====================================================
-- 5. VERIFICAR USUARIOS Y SUCURSALES
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN DE USUARIOS Y SUCURSALES ===';

-- Usuarios por sucursal
SELECT 
    s.Nombre AS Sucursal,
    COUNT(u.Id) AS TotalUsuarios,
    STRING_AGG(u.Nombre, ', ') AS Usuarios
FROM Sucursales s
LEFT JOIN Usuarios u ON s.Id = u.SucursalId
GROUP BY s.Id, s.Nombre
ORDER BY TotalUsuarios DESC;

-- Usuarios por rol
SELECT 
    u.Rol,
    COUNT(u.Id) AS TotalUsuarios,
    STRING_AGG(u.Nombre, ', ') AS Usuarios
FROM Usuarios u
GROUP BY u.Rol
ORDER BY TotalUsuarios DESC;

-- =====================================================
-- 6. VERIFICAR ESTADOS DE TRANSACCIONES
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN DE ESTADOS ===';

-- Presupuestos por estado
SELECT 
    Estado,
    COUNT(*) AS Total,
    SUM(Total) AS MontoTotal
FROM Presupuestos
GROUP BY Estado
ORDER BY Total DESC;

-- Pedidos por estado
SELECT 
    Estado,
    COUNT(*) AS Total
FROM Pedidos
GROUP BY Estado
ORDER BY Total DESC;

-- Compras por estado
SELECT 
    Estado,
    COUNT(*) AS Total,
    SUM(Total) AS MontoTotal
FROM Compras
GROUP BY Estado
ORDER BY Total DESC;

-- =====================================================
-- 7. CONSULTAS ESPECÍFICAS ÚTILES
-- =====================================================
PRINT '';
PRINT '=== CONSULTAS ESPECÍFICAS ===';

-- Top 5 clientes por facturación
SELECT TOP 5
    c.Nombre AS Cliente,
    COUNT(f.Id) AS TotalFacturas,
    SUM(f.Total) AS TotalFacturado
FROM Clientes c
JOIN Facturas f ON c.Id = f.ClienteId
GROUP BY c.Id, c.Nombre
ORDER BY TotalFacturado DESC;

-- Top 5 productos más vendidos
SELECT TOP 5
    p.Nombre AS Producto,
    p.SKU,
    SUM(df.Cantidad) AS TotalVendido,
    SUM(df.Cantidad * df.PrecioUnitario) AS Ingresos
FROM Productos p
JOIN DetallesFactura df ON p.Id = df.ProductoId
GROUP BY p.Id, p.Nombre, p.SKU
ORDER BY TotalVendido DESC;

-- Resumen de pagos por método
SELECT 
    mp.Nombre AS MetodoPago,
    COUNT(p.Id) AS TotalPagos,
    SUM(p.Monto) AS MontoTotal
FROM MetodosPago mp
LEFT JOIN Pagos p ON mp.Id = p.MetodoPagoId
GROUP BY mp.Id, mp.Nombre
ORDER BY MontoTotal DESC;

-- =====================================================
-- 8. VERIFICAR INTEGRIDAD REFERENCIAL
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN DE INTEGRIDAD ===';

-- Verificar que no hay registros huérfanos
SELECT 'Clientes sin facturas' AS Verificacion, COUNT(*) AS Total
FROM Clientes c
LEFT JOIN Facturas f ON c.Id = f.ClienteId
WHERE f.Id IS NULL
UNION ALL
SELECT 'Productos sin movimientos', COUNT(*)
FROM Productos p
LEFT JOIN MovimientosInventario mi ON p.Id = mi.ProductoId
WHERE mi.Id IS NULL
UNION ALL
SELECT 'Pedidos sin detalles', COUNT(*)
FROM Pedidos pe
LEFT JOIN DetallesPedido dp ON pe.Id = dp.PedidoId
WHERE dp.Id IS NULL;

-- =====================================================
-- FINALIZACIÓN
-- =====================================================
PRINT '';
PRINT '=== VERIFICACIÓN COMPLETADA ===';
PRINT 'Si no hay errores en las consultas anteriores,';
PRINT 'los datos dummy se insertaron correctamente.';
PRINT '==========================================';
