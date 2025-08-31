# Scripts de Datos Dummy para ManagementBusiness

## Descripción

Este directorio contiene scripts SQL para insertar datos de prueba realistas en la base de datos ManagementBusiness. Los datos están diseñados para simular un negocio real funcionando, facilitando el desarrollo y testing de la aplicación.

## Archivos Incluidos

### 1. `Dummy_Data_Insert.sql`
**Archivo principal** que inserta todos los datos dummy en la base de datos.

**Contenido:**
- 4 Sucursales (Centro, Norte, Sur, Este)
- 6 Usuarios con diferentes roles
- 8 Categorías de productos
- 5 Tipos de impuestos
- 8 Tipos de gasto
- 7 Métodos de pago
- 10 Clientes realistas
- 10 Proveedores
- 16 Productos variados
- 8 Servicios
- 12 Reportes mensuales
- 10 Presupuestos
- 10 Pedidos
- 10 Compras
- 10 Gastos
- 5 Ventas
- 5 Facturas
- Detalles de pedidos, facturas y compras
- Movimientos de inventario
- Pagos

### 2. `Verification_Queries.sql`
**Script de verificación** que contiene consultas útiles para validar que los datos se insertaron correctamente.

**Consultas incluidas:**
- Totales por entidad
- Verificación de relaciones entre entidades
- Análisis financiero
- Estado del inventario
- Distribución de usuarios y sucursales
- Estados de transacciones
- Consultas específicas útiles
- Verificación de integridad referencial

## Instrucciones de Uso

### Paso 1: Preparación
1. Asegúrate de que la base de datos `ManagementBusiness` esté creada
2. Verifica que las migraciones de Entity Framework estén aplicadas
3. Abre SQL Server Management Studio

### Paso 2: Ejecutar el Script Principal
1. Conecta a tu servidor SQL Server
2. Selecciona la base de datos `ManagementBusiness`
3. Abre el archivo `Dummy_Data_Insert.sql`
4. Ejecuta todo el script (F5 o botón Execute)

### Paso 3: Verificar los Datos
1. Abre el archivo `Verification_Queries.sql`
2. Ejecuta las consultas de verificación
3. Revisa que no haya errores y que los totales coincidan

## Datos Incluidos

### 🏢 **Sucursales**
- **Sucursal Centro**: Principal, ubicada en CDMX
- **Sucursal Norte**: En Monterrey
- **Sucursal Sur**: En Guadalajara
- **Sucursal Este**: En CDMX

### 👥 **Usuarios**
- **Juan Carlos Pérez**: Administrador (Sucursal Centro)
- **María González**: Vendedor (Sucursal Centro)
- **Carlos Rodríguez**: Vendedor (Sucursal Norte)
- **Ana Martínez**: Contador (Sucursal Centro)
- **Luis Fernández**: Vendedor (Sucursal Sur)
- **Carmen Silva**: Vendedor (Sucursal Este)

### 🛍️ **Productos**
- **Electrónicos**: Laptops, mouse, teclados
- **Ropa**: Camisetas, jeans, sudaderas
- **Hogar**: Lámparas, sábanas
- **Deportes**: Pelotas, raquetas
- **Libros**: Variedad de títulos
- **Juguetes**: Rompecabezas
- **Automotriz**: Aceites, filtros
- **Jardín**: Macetas, fertilizantes

### 🏪 **Clientes**
- Empresas realistas (ABC S.A., Restaurante El Sabor, Hotel Plaza Central)
- Personas físicas con RFC válidos
- Diferentes ubicaciones geográficas
- Variedad de tipos de negocio

### 📊 **Transacciones**
- **Presupuestos**: 10 presupuestos con diferentes estados
- **Pedidos**: 10 pedidos con diferentes estados
- **Ventas**: 5 ventas completadas
- **Compras**: 10 compras a proveedores
- **Facturas**: 5 facturas emitidas
- **Gastos**: 10 gastos operativos

## Características de los Datos

### ✅ **Realismo**
- Nombres y direcciones realistas
- RFC válidos para clientes mexicanos
- Precios de mercado realistas
- Fechas coherentes (2024)
- Estados de transacciones variados

### ✅ **Integridad**
- Todas las relaciones están correctamente configuradas
- Los totales coinciden entre entidades principales y detalles
- Los movimientos de inventario reflejan compras y ventas
- Los pagos están vinculados a las transacciones correspondientes

### ✅ **Variedad**
- Diferentes tipos de productos y servicios
- Múltiples métodos de pago
- Variedad de estados de transacciones
- Diferentes ubicaciones geográficas
- Múltiples roles de usuario

## Consultas Útiles para Desarrollo

### 📈 **Análisis de Ventas**
```sql
-- Ventas por mes
SELECT MONTH(Fecha) AS Mes, SUM(Total) AS TotalVentas
FROM Ventas 
GROUP BY MONTH(Fecha)
ORDER BY Mes;
```

### 📦 **Estado del Inventario**
```sql
-- Productos con stock bajo
SELECT p.Nombre, p.StockMinimo, 
       COALESCE(SUM(mi.Cantidad), 0) AS StockActual
FROM Productos p
LEFT JOIN MovimientosInventario mi ON p.Id = mi.ProductoId
GROUP BY p.Id, p.Nombre, p.StockMinimo
HAVING COALESCE(SUM(mi.Cantidad), 0) <= p.StockMinimo;
```

### 💰 **Análisis Financiero**
```sql
-- Resumen de ingresos vs gastos
SELECT 
    'Ingresos' AS Tipo, SUM(Total) AS Monto FROM Ventas
UNION ALL
SELECT 'Gastos', SUM(Monto) FROM Gastos;
```

## Solución de Problemas

### ❌ **Error de Clave Foránea**
Si encuentras errores de clave foránea:
1. Verifica que las migraciones estén aplicadas
2. Asegúrate de que la base de datos esté vacía antes de ejecutar
3. Revisa el orden de inserción en el script

### ❌ **Error de Duplicación**
Si encuentras errores de duplicación:
1. Descomenta las líneas de limpieza al inicio del script
2. Ejecuta el script de limpieza primero
3. Luego ejecuta el script de inserción

### ❌ **Error de Permisos**
Si tienes problemas de permisos:
1. Verifica que tu usuario tenga permisos de INSERT
2. Asegúrate de estar conectado como administrador de la base de datos

## Personalización

### 🔧 **Modificar Datos**
Para personalizar los datos:
1. Edita el archivo `Dummy_Data_Insert.sql`
2. Modifica los valores INSERT según tus necesidades
3. Ajusta las cantidades, precios o nombres

### ➕ **Agregar Más Datos**
Para agregar más registros:
1. Copia y pega las secciones INSERT existentes
2. Modifica los valores para crear nuevos registros
3. Mantén la consistencia en las relaciones

## Notas Importantes

- **Backup**: Siempre haz un backup de tu base de datos antes de ejecutar scripts
- **Testing**: Usa estos datos solo en entornos de desarrollo/testing
- **Producción**: Nunca ejecutes estos scripts en producción
- **Actualizaciones**: Si cambias la estructura de la base de datos, actualiza estos scripts

## Soporte

Si encuentras problemas con estos scripts:
1. Verifica que la versión de SQL Server sea compatible
2. Revisa los logs de error en SQL Server Management Studio
3. Confirma que la estructura de la base de datos coincida con los scripts

---

**Última actualización**: Agosto 2024  
**Versión**: 1.0  
**Compatibilidad**: SQL Server 2019+, Entity Framework Core 9.0
