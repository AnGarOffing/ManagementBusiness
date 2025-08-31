# Scripts de Datos Dummy para ManagementBusiness

## 📋 Descripción

Este conjunto de scripts inserta datos de prueba realistas en la base de datos `ManagementBusiness` para permitir el desarrollo y testing de la aplicación. Los scripts están diseñados para funcionar con IDs auto-incrementales (IDENTITY) y respetan todas las dependencias de claves foráneas.

## 🗂️ Archivos del Script

### 1. **Dummy_Data_Insert_Identity.sql** (Primera Parte)
- **Entidades Base**: Clientes, Proveedores, Usuarios, Productos, Servicios
- **Datos**: 10 clientes, 10 proveedores, 6 usuarios, 16 productos, 8 servicios
- **Características**: Usa variables para obtener IDs de sucursales, categorías e impuestos

### 2. **Dummy_Data_Insert_Identity_Part2.sql** (Segunda Parte)
- **Transacciones**: Presupuestos, Pedidos, Compras, Gastos
- **Datos**: 10 presupuestos, 6 pedidos, 10 compras, 10 gastos
- **Características**: Obtiene IDs de clientes, presupuestos y proveedores dinámicamente

### 3. **Dummy_Data_Insert_Identity_Part3.sql** (Tercera Parte)
- **Completar Transacciones**: Ventas, Facturas, Detalles, Movimientos, Pagos, Reportes
- **Datos**: 5 ventas, 5 facturas, 10 detalles de pedido, 10 detalles de factura, 10 detalles de compra, 10 movimientos de inventario, 17 pagos, 12 reportes mensuales
- **Características**: Completa el ciclo completo de transacciones

## 🚀 Instrucciones de Uso

### **Paso 1: Verificar Base de Datos**
Asegúrate de que la base de datos `ManagementBusiness` esté creada y contenga las tablas con datos semilla:
```sql
USE ManagementBusiness;
GO

-- Verificar que existan datos semilla
SELECT COUNT(*) FROM MetodosPago;
SELECT COUNT(*) FROM Impuestos;
SELECT COUNT(*) FROM TiposGasto;
SELECT COUNT(*) FROM CategoriasProductos;
SELECT COUNT(*) FROM Sucursales;
```

### **Paso 2: Ejecutar Scripts en Orden**
**IMPORTANTE**: Ejecutar los scripts en el orden especificado para evitar errores de dependencias.

#### **Script 1: Primera Parte**
```sql
-- Ejecutar en SQL Server Management Studio
-- Archivo: Dummy_Data_Insert_Identity.sql
```
**Resultado esperado**: "=== PRIMERA PARTE COMPLETADA ==="

#### **Script 2: Segunda Parte**
```sql
-- Ejecutar en SQL Server Management Studio
-- Archivo: Dummy_Data_Insert_Identity_Part2.sql
```
**Resultado esperado**: "=== SEGUNDA PARTE COMPLETADA ==="

#### **Script 3: Tercera Parte**
```sql
-- Ejecutar en SQL Server Management Studio
-- Archivo: Dummy_Data_Insert_Identity_Part3.sql
```
**Resultado esperado**: Verificación completa con conteos de todas las tablas

## 📊 Datos Incluidos

### **Entidades de Catálogo**
- **7 Métodos de Pago**: Efectivo, Tarjeta de Crédito/Débito, Transferencia, Cheque, PayPal, Mercado Pago
- **5 Impuestos**: IVA (16%), IGV (18%), GST (10%), VAT (20%), Sales Tax (8.25%)
- **8 Tipos de Gasto**: Alquiler, Sueldos, Marketing, Servicios Públicos, Mantenimiento, Seguros, Impuestos, Otros
- **8 Categorías de Productos**: Electrónicos, Ropa, Hogar, Deportes, Libros, Juguetes, Automotriz, Jardín
- **4 Sucursales**: Centro (Principal), Norte, Sur, Este

### **Entidades de Negocio**
- **10 Clientes**: Empresas y personas físicas con RFCs realistas
- **10 Proveedores**: Distribuidoras y empresas de servicios
- **6 Usuarios**: Administrador, Vendedores y Contador distribuidos en sucursales
- **16 Productos**: Variedad de productos con precios realistas y SKUs únicos
- **8 Servicios**: Servicios IT y de consultoría

### **Transacciones Completas**
- **10 Presupuestos**: Estados variados (Aprobado, Pendiente, Rechazado)
- **6 Pedidos**: Vinculados a presupuestos y clientes
- **10 Compras**: Con diferentes proveedores y estados
- **10 Gastos**: Diferentes tipos y montos
- **5 Ventas**: Completadas con diferentes métodos de pago
- **5 Facturas**: Vinculadas a ventas y clientes
- **Detalles Completos**: Pedidos, Facturas y Compras
- **Movimientos de Inventario**: Entradas por compras y salidas por ventas
- **17 Pagos**: Compras, facturas y gastos
- **12 Reportes Mensuales**: 6 meses para 2 sucursales principales

## 🔍 Verificación de Datos

### **Consulta de Verificación Rápida**
```sql
-- Verificar totales de todas las tablas
SELECT 'Sucursales' AS Tabla, COUNT(*) AS Total FROM Sucursales
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
SELECT 'Movimientos Inventario', COUNT(*) FROM MovimientosInventario
UNION ALL
SELECT 'Reportes Mensuales', COUNT(*) FROM ReportesMensuales;
```

### **Verificar Relaciones**
```sql
-- Verificar que las ventas tengan facturas
SELECT v.Id, v.Total, f.NumeroFactura, f.Estado
FROM Ventas v
INNER JOIN Facturas f ON v.FacturaId = f.Id;

-- Verificar movimientos de inventario
SELECT mi.Fecha, p.Nombre AS Producto, s.Nombre AS Sucursal, 
       mi.Cantidad, mi.Tipo, mi.Referencia
FROM MovimientosInventario mi
INNER JOIN Productos p ON mi.ProductoId = p.Id
INNER JOIN Sucursales s ON mi.SucursalId = s.Id
ORDER BY mi.Fecha;
```

## ⚠️ Solución de Problemas

### **Error: "Cannot insert explicit value for identity column"**
- **Causa**: El script está intentando insertar IDs explícitos
- **Solución**: Verificar que estés usando la versión correcta del script (con IDENTITY)

### **Error: "The INSERT statement conflicted with the FOREIGN KEY constraint"**
- **Causa**: Dependencias no resueltas o scripts ejecutados en orden incorrecto
- **Solución**: 
  1. Ejecutar scripts en el orden especificado
  2. Verificar que existan datos semilla en las tablas de catálogo
  3. Revisar que las variables de ID se estén obteniendo correctamente

### **Error: "Subqueries are not allowed in this context"**
- **Causa**: PRINT statements con subqueries
- **Solución**: Usar variables para almacenar conteos antes de imprimir

## 🧪 Casos de Prueba Sugeridos

### **1. Gestión de Clientes**
- Crear nuevo cliente
- Modificar cliente existente
- Cambiar estado activo/inactivo

### **2. Gestión de Productos**
- Agregar nuevo producto
- Modificar precios
- Cambiar categoría

### **3. Proceso de Venta Completo**
- Crear presupuesto
- Convertir a pedido
- Procesar venta
- Generar factura
- Registrar pago

### **4. Gestión de Inventario**
- Registrar compra
- Actualizar stock
- Procesar venta
- Verificar movimientos

### **5. Reportes Financieros**
- Reportes mensuales por sucursal
- Análisis de ventas por período
- Control de gastos por tipo

## 📝 Notas Importantes

- **IDs Auto-incrementales**: Todos los IDs se generan automáticamente por SQL Server
- **Dependencias Respetadas**: El script respeta todas las restricciones de integridad referencial
- **Datos Realistas**: Nombres, direcciones, RFCs y precios son realistas para México
- **Transacciones Completas**: Se crean ciclos completos de negocio para testing
- **Verificación Automática**: Cada parte incluye verificación de datos insertados

## 🔄 Personalización

### **Modificar Cantidades**
Para cambiar la cantidad de datos insertados, modifica los valores en los `INSERT INTO` statements.

### **Agregar Nuevos Productos**
Para agregar productos, inserta en la tabla `Productos` y asegúrate de que existan las categorías e impuestos correspondientes.

### **Modificar Precios**
Los precios están en pesos mexicanos (MXN). Ajusta según tus necesidades de testing.

## 📞 Soporte

Si encuentras problemas al ejecutar los scripts:
1. Verifica el orden de ejecución
2. Revisa que la base de datos tenga la estructura correcta
3. Confirma que existan los datos semilla básicos
4. Revisa los mensajes de error específicos

Los scripts están diseñados para ser robustos y proporcionar información clara sobre cualquier problema que pueda surgir.
