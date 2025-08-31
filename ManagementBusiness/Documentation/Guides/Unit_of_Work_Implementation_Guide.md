# **GUÍA DE IMPLEMENTACIÓN DEL PATRÓN UNIT OF WORK**

## **📋 DESCRIPCIÓN GENERAL**

El patrón **Unit of Work** es un patrón de diseño que coordina múltiples repositorios y gestiona transacciones de base de datos de manera eficiente. En nuestro proyecto ManagementBusiness, este patrón es fundamental para mantener la integridad de los datos y coordinar operaciones complejas que involucran múltiples entidades.

---

## **🏗️ ARQUITECTURA IMPLEMENTADA**

### **Interfaces Principales:**

#### **1. IUnitOfWork**
- **Propósito:** Define la interfaz principal para el patrón Unit of Work
- **Responsabilidades:**
  - Gestión de repositorios genéricos
  - Control de transacciones
  - Coordinación de operaciones CRUD
  - Manejo de errores y rollback automático

#### **2. IDbTransaction**
- **Propósito:** Interfaz para transacciones de base de datos
- **Responsabilidades:**
  - Confirmación de transacciones
  - Reversión de transacciones
  - Gestión del ciclo de vida de la transacción

### **Implementaciones:**

#### **1. UnitOfWork**
- **Propósito:** Implementación concreta del patrón Unit of Work
- **Características:**
  - Caché de repositorios para optimización
  - Gestión automática de transacciones
  - Manejo de errores robusto
  - Soporte para operaciones síncronas y asíncronas

#### **2. DbTransactionWrapper**
- **Propósito:** Wrapper para transacciones de Entity Framework
- **Características:**
  - Adapta transacciones de EF a nuestra interfaz
  - Manejo automático de recursos
  - Implementación del patrón Dispose

---

## **🔧 FUNCIONALIDADES IMPLEMENTADAS**

### **1. Gestión de Repositorios**
```csharp
// Obtener repositorio genérico
var clienteRepo = unitOfWork.GetRepository<Cliente>();

// Obtener repositorio con ID
var clienteRepo = unitOfWork.GetRepositoryWithId<Cliente, int>();

// Obtener repositorio con auditoría
var clienteRepo = unitOfWork.GetRepositoryWithAudit<Cliente>();

// Obtener repositorio con eliminación suave
var clienteRepo = unitOfWork.GetRepositoryWithSoftDelete<Cliente>();
```

### **2. Control de Transacciones**
```csharp
// Transacción automática
var resultado = await unitOfWork.ExecuteInTransactionAsync(async () =>
{
    // Operaciones que se ejecutan en transacción
    var cliente = new Cliente { /* ... */ };
    clienteRepo.Add(cliente);
    return cliente;
});

// Transacción manual
var transaction = await unitOfWork.BeginTransactionAsync();
try
{
    // Operaciones
    transaction.Commit();
}
catch
{
    transaction.Rollback();
    throw;
}
```

### **3. Operaciones CRUD Coordinadas**
```csharp
// Ejemplo: Crear pedido completo
var resultado = await unitOfWork.ExecuteInTransactionAsync(async () =>
{
    // 1. Verificar cliente y producto
    // 2. Crear pedido
    // 3. Crear detalles
    // 4. Actualizar inventario
    // 5. Actualizar stock
    
    // Si algo falla, todo se revierte automáticamente
});
```

---

## **📚 EJEMPLOS DE USO**

### **Ejemplo 1: Creación Simple de Cliente**
```csharp
public async Task<Cliente> CrearClienteAsync(string nombre, string email)
{
    var unitOfWork = ServiceContainer.GetRequiredService<IUnitOfWork>();
    
    try
    {
        var cliente = await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var repo = unitOfWork.GetRepository<Cliente>();
            var nuevoCliente = new Cliente
            {
                Nombre = nombre,
                Email = email,
                // ... otras propiedades
            };
            
            repo.Add(nuevoCliente);
            return nuevoCliente;
        });
        
        return cliente;
    }
    catch (Exception ex)
    {
        // La transacción se revierte automáticamente
        throw new InvalidOperationException("Error al crear cliente", ex);
    }
}
```

### **Ejemplo 2: Operación Compleja de Negocio**
```csharp
public async Task<bool> ProcesarVentaAsync(int clienteId, int productoId, int cantidad)
{
    var unitOfWork = ServiceContainer.GetRequiredService<IUnitOfWork>();
    
    try
    {
        return await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            // 1. Verificar stock
            var productoRepo = unitOfWork.GetRepositoryWithId<Producto, int>();
            var producto = await productoRepo.GetByIdAsync(productoId);
            
            if (producto.Stock < cantidad)
                throw new InvalidOperationException("Stock insuficiente");
            
            // 2. Crear venta
            var ventaRepo = unitOfWork.GetRepository<Venta>();
            var venta = new Venta { /* ... */ };
            ventaRepo.Add(venta);
            
            // 3. Actualizar inventario
            producto.Stock -= cantidad;
            productoRepo.Update(producto);
            
            // 4. Crear movimiento de inventario
            var inventarioRepo = unitOfWork.GetRepository<MovimientoInventario>();
            var movimiento = new MovimientoInventario { /* ... */ };
            inventarioRepo.Add(movimiento);
            
            return true;
        });
    }
    catch (Exception ex)
    {
        // Todo se revierte automáticamente
        throw new InvalidOperationException("Error al procesar venta", ex);
    }
}
```

### **Ejemplo 3: Transacción Manual para Operaciones Especiales**
```csharp
public async Task<bool> MigrarDatosAsync()
{
    var unitOfWork = ServiceContainer.GetRequiredService<IUnitOfWork>();
    var transaction = await unitOfWork.BeginTransactionAsync();
    
    try
    {
        // Operaciones complejas que requieren control manual
        await MigrarClientesAsync(unitOfWork);
        await MigrarProductosAsync(unitOfWork);
        await MigrarPedidosAsync(unitOfWork);
        
        // Confirmar solo si todo sale bien
        transaction.Commit();
        return true;
    }
    catch (Exception)
    {
        // Revertir en caso de error
        transaction.Rollback();
        throw;
    }
}
```

---

## **⚡ BENEFICIOS DEL PATRÓN IMPLEMENTADO**

### **1. Integridad de Datos**
- **Transacciones automáticas** para operaciones complejas
- **Rollback automático** en caso de errores
- **Consistencia garantizada** entre múltiples entidades

### **2. Rendimiento**
- **Caché de repositorios** para evitar recreaciones
- **Transacciones optimizadas** para operaciones en lote
- **Manejo eficiente** de recursos de base de datos

### **3. Mantenibilidad**
- **Código limpio** y fácil de entender
- **Separación de responsabilidades** clara
- **Reutilización** de lógica de transacciones

### **4. Escalabilidad**
- **Fácil extensión** para nuevas entidades
- **Soporte para operaciones complejas** de negocio
- **Arquitectura preparada** para crecimiento futuro

---

## **🚨 CONSIDERACIONES IMPORTANTES**

### **1. Gestión de Errores**
- **Siempre usar try-catch** alrededor de operaciones del Unit of Work
- **Las transacciones se revierten automáticamente** en caso de excepción
- **Manejar errores específicos** de negocio apropiadamente

### **2. Ciclo de Vida**
- **El Unit of Work se registra como Scoped** en el contenedor de servicios
- **Se crea una instancia por request** en aplicaciones web
- **Se libera automáticamente** al final del scope

### **3. Transacciones Anidadas**
- **No se soportan transacciones anidadas** en la implementación actual
- **Usar transacciones manuales** para operaciones complejas
- **Considerar el patrón Saga** para operaciones muy complejas

### **4. Rendimiento**
- **Evitar operaciones muy largas** dentro de una transacción
- **Usar transacciones manuales** para operaciones en lote
- **Considerar el uso de SaveChanges** en puntos intermedios para transacciones largas

---

## **🔍 CASOS DE USO RECOMENDADOS**

### **✅ Usar Unit of Work para:**
- **Operaciones CRUD simples** que involucran una sola entidad
- **Operaciones de negocio** que involucran múltiples entidades
- **Operaciones que requieren** consistencia transaccional
- **Operaciones que pueden fallar** y requieren rollback

### **❌ No usar Unit of Work para:**
- **Operaciones de solo lectura** (usar repositorios directamente)
- **Operaciones muy largas** (considerar transacciones manuales)
- **Operaciones que no requieren** consistencia transaccional
- **Operaciones de mantenimiento** de base de datos

---

## **📝 PRÓXIMOS PASOS**

### **1. Implementación de Servicios de Negocio**
- Crear servicios específicos que usen el Unit of Work
- Implementar lógica de negocio compleja
- Agregar validaciones de negocio

### **2. Implementación de Servicios de Logging**
- Agregar logging a las operaciones del Unit of Work
- Implementar auditoría de transacciones
- Crear reportes de operaciones

### **3. Optimizaciones de Rendimiento**
- Implementar caché de consultas frecuentes
- Optimizar transacciones largas
- Agregar métricas de rendimiento

---

## **🔗 ARCHIVOS RELACIONADOS**

- **`IUnitOfWork.cs`** - Interfaz principal del patrón
- **`UnitOfWork.cs`** - Implementación concreta
- **`UnitOfWorkExample.cs`** - Ejemplos de uso
- **`ServiceContainer.cs`** - Registro del servicio
- **`IRepository.cs`** - Interfaces de repositorio base

---

*Esta guía debe actualizarse conforme evolucione la implementación del patrón Unit of Work.*
