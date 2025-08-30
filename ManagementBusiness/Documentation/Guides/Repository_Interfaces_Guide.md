# **GUÍA DE INTERFACES DE REPOSITORIO - MANAGEMENT BUSINESS**

## **📋 RESUMEN**

Este documento describe las interfaces de repositorio genérico implementadas para el patrón Repository en la aplicación Management Business. Estas interfaces proporcionan una base sólida y extensible para todas las operaciones de datos.

---

## **🏗️ ARQUITECTURA DE INTERFACES**

### **Jerarquía de Interfaces:**

```
IRepository<T> (Base)
├── IRepositoryWithId<T, TId> (Entidades con ID específico)
├── IRepositoryWithAudit<T> (Entidades con auditoría)
└── IRepositoryWithSoftDelete<T> (Entidades con soft delete)
```

### **Características Principales:**

- **Operaciones CRUD completas** (Create, Read, Update, Delete)
- **Soporte asíncrono** para todas las operaciones
- **Filtrado avanzado** con expresiones lambda
- **Paginación y ordenamiento** integrados
- **Inclusión de propiedades de navegación** (eager loading)
- **Proyección de datos** para optimizar consultas
- **Operaciones especializadas** según el tipo de entidad

---

## **🔧 INTERFACES IMPLEMENTADAS**

### **1. IRepository<T> - Interfaz Base**

**Propósito:** Proporciona operaciones CRUD básicas para cualquier entidad.

**Operaciones Principales:**
- **CRUD Básico:** Add, Update, Remove, GetById, GetAll
- **Filtrado:** Get(predicate), GetFirst(predicate)
- **Paginación:** GetPaged(pageNumber, pageSize)
- **Ordenamiento:** GetOrdered<TKey>(keySelector, ascending)
- **Conteo:** Count(), Any(), Count(predicate)
- **Inclusión:** GetWithIncludes(includes)
- **Proyección:** GetProjected<TResult>(selector)

**Ejemplo de Uso:**
```csharp
public interface IClienteRepository : IRepository<Cliente>
{
    // Operaciones específicas de cliente
    IEnumerable<Cliente> GetByCategoria(string categoria);
}

public class ClienteRepository : IClienteRepository
{
    // Implementación de todas las operaciones base + específicas
}
```

---

### **2. IRepositoryWithId<T, TId> - Entidades con ID Específico**

**Propósito:** Extiende la funcionalidad base para entidades con ID tipado.

**Operaciones Adicionales:**
- **Operaciones por ID:** GetById(TId), Exists(TId), RemoveById(TId)
- **Operaciones múltiples:** GetByIds(ids), RemoveByIds(ids)
- **Búsqueda por rango:** GetByIdRange(startId, endId)
- **Operaciones de ID:** GetNextId(), GetMaxId()

**Ejemplo de Uso:**
```csharp
public interface IProductoRepository : IRepositoryWithId<Producto, int>
{
    // Operaciones específicas de producto
    IEnumerable<Producto> GetByCategoria(int categoriaId);
}

public class ProductoRepository : IProductoRepository
{
    // Implementación con operaciones de ID tipado
    public Producto GetById(int id) { /* implementación */ }
    public bool Exists(int id) { /* implementación */ }
}
```

---

### **3. IRepositoryWithAudit<T> - Entidades con Auditoría**

**Propósito:** Extiende la funcionalidad para entidades que registran fechas y usuarios de creación/modificación.

**Operaciones Adicionales:**
- **Por fecha:** GetByCreatedDate(date), GetByModifiedDate(date)
- **Por usuario:** GetByCreatedBy(user), GetByModifiedBy(user)
- **Rangos de fecha:** GetByCreatedDateRange(start, end)
- **Períodos:** GetCreatedToday(), GetCreatedLastWeek(), GetCreatedLastMonth()
- **Historial:** GetChangeHistory(entityId)

**Ejemplo de Uso:**
```csharp
public interface IPedidoRepository : IRepositoryWithAudit<Pedido>
{
    // Operaciones específicas de pedido
    IEnumerable<Pedido> GetByEstado(string estado);
}

public class PedidoRepository : IRepositoryWithAudit<Pedido>
{
    // Implementación con operaciones de auditoría
    public IEnumerable<Pedido> GetByCreatedDate(DateTime date) { /* implementación */ }
    public IEnumerable<Pedido> GetByCreatedBy(string user) { /* implementación */ }
}
```

---

### **4. IRepositoryWithSoftDelete<T> - Entidades con Eliminación Lógica**

**Propósito:** Extiende la funcionalidad para entidades que soportan eliminación lógica (soft delete).

**Operaciones Adicionales:**
- **Soft Delete:** SoftDelete(entity), SoftDeleteRange(entities)
- **Restauración:** Restore(entity), RestoreRange(entities)
- **Consultas separadas:** GetActive(), GetDeleted()
- **Limpieza:** PermanentlyDelete(entities), CleanupDeletedOlderThan(date)

**Ejemplo de Uso:**
```csharp
public interface IClienteRepository : IRepositoryWithSoftDelete<Cliente>
{
    // Operaciones específicas de cliente
    IEnumerable<Cliente> GetByCategoria(string categoria);
}

public class ClienteRepository : IRepositoryWithSoftDelete<Cliente>
{
    // Implementación con operaciones de soft delete
    public void SoftDelete(Cliente cliente) { /* implementación */ }
    public IEnumerable<Cliente> GetActive() { /* implementación */ }
}
```

---

## **💡 PATRONES DE IMPLEMENTACIÓN**

### **Patrón 1: Repositorio Simple**
```csharp
public interface IClienteRepository : IRepository<Cliente>
{
    // Solo operaciones base + específicas de cliente
}

public class ClienteRepository : IClienteRepository
{
    private readonly ManagementBusinessContext _context;
    
    public ClienteRepository(ManagementBusinessContext context)
    {
        _context = context;
    }
    
    // Implementar todas las operaciones de IRepository<T>
}
```

### **Patrón 2: Repositorio con ID Tipado**
```csharp
public interface IProductoRepository : IRepositoryWithId<Producto, int>
{
    // Operaciones base + operaciones de ID + específicas
}

public class ProductoRepository : IRepositoryWithId<Producto, int>
{
    // Implementar operaciones base + operaciones de ID
}
```

### **Patrón 3: Repositorio con Auditoría**
```csharp
public interface IPedidoRepository : IRepositoryWithAudit<Pedido>
{
    // Operaciones base + auditoría + específicas
}

public class PedidoRepository : IRepositoryWithAudit<Pedido>
{
    // Implementar operaciones base + auditoría
}
```

### **Patrón 4: Repositorio Completo**
```csharp
public interface IClienteRepository : 
    IRepositoryWithId<Cliente, int>,
    IRepositoryWithAudit<Cliente>,
    IRepositoryWithSoftDelete<Cliente>
{
    // Operaciones base + ID + auditoría + soft delete + específicas
}

public class ClienteRepository : IClienteRepository
{
    // Implementar todas las operaciones de las interfaces
}
```

---

## **🚀 VENTAJAS DE ESTA ARQUITECTURA**

### **1. Flexibilidad**
- **Composición de interfaces** según necesidades específicas
- **Implementación gradual** de funcionalidades
- **Reutilización** de código común

### **2. Mantenibilidad**
- **Separación clara** de responsabilidades
- **Documentación completa** con XML comments
- **Consistencia** en todas las implementaciones

### **3. Escalabilidad**
- **Fácil extensión** con nuevas funcionalidades
- **Patrones establecidos** para nuevos repositorios
- **Testing simplificado** con interfaces bien definidas

### **4. Rendimiento**
- **Operaciones asíncronas** para operaciones de larga duración
- **Filtrado eficiente** con expresiones lambda
- **Paginación integrada** para grandes volúmenes de datos

---

## **📝 EJEMPLOS DE USO PRÁCTICO**

### **Ejemplo 1: Cliente con Auditoría y Soft Delete**
```csharp
public interface IClienteRepository : 
    IRepositoryWithId<Cliente, int>,
    IRepositoryWithAudit<Cliente>,
    IRepositoryWithSoftDelete<Cliente>
{
    // Operaciones específicas de cliente
    IEnumerable<Cliente> GetByCategoria(string categoria);
    IEnumerable<Cliente> GetByCiudad(string ciudad);
    Cliente GetByEmail(string email);
}

public class ClienteRepository : IClienteRepository
{
    private readonly ManagementBusinessContext _context;
    
    public ClienteRepository(ManagementBusinessContext context)
    {
        _context = context;
    }
    
    // Implementar todas las operaciones...
    
    public IEnumerable<Cliente> GetByCategoria(string categoria)
    {
        return GetActive(c => c.Categoria == categoria);
    }
}
```

### **Ejemplo 2: Producto con ID Tipado**
```csharp
public interface IProductoRepository : IRepositoryWithId<Producto, int>
{
    IEnumerable<Producto> GetByCategoria(int categoriaId);
    IEnumerable<Producto> GetByProveedor(int proveedorId);
    IEnumerable<Producto> GetEnStock();
}

public class ProductoRepository : IRepositoryWithId<Producto, int>
{
    // Implementación...
    
    public IEnumerable<Producto> GetEnStock()
    {
        return Get(p => p.Stock > 0);
    }
}
```

---

## **🔍 CONSIDERACIONES DE IMPLEMENTACIÓN**

### **1. Contexto de Base de Datos**
- **Inyectar** `ManagementBusinessContext` en el constructor
- **Usar** `DbSet<T>` para operaciones de Entity Framework
- **Implementar** `IDisposable` si es necesario

### **2. Manejo de Errores**
- **Capturar** excepciones de Entity Framework
- **Logging** de errores para debugging
- **Retornar** resultados apropiados en caso de error

### **3. Transacciones**
- **Usar** `Unit of Work` para operaciones complejas
- **Manejar** rollback en caso de error
- **Considerar** operaciones atómicas

### **4. Validaciones**
- **Validar** entidades antes de operaciones de escritura
- **Verificar** existencia antes de operaciones de eliminación
- **Validar** parámetros de entrada

---

## **📊 ESTADO DE IMPLEMENTACIÓN**

### **✅ COMPLETADO:**
- [x] **IRepository<T>** - Interfaz base completa
- [x] **IRepositoryWithId<T, TId>** - Operaciones con ID tipado
- [x] **IRepositoryWithAudit<T>** - Operaciones de auditoría
- [x] **IRepositoryWithSoftDelete<T>** - Operaciones de soft delete
- [x] **Documentación completa** de todas las interfaces

### **🔄 PRÓXIMOS PASOS:**
- [ ] **Implementar** repositorio base genérico
- [ ] **Crear** repositorios específicos para cada entidad
- [ ] **Implementar** Unit of Work pattern
- [ ] **Crear** servicios de negocio que usen estos repositorios

---

## **🔗 ENLACES RELACIONADOS**

- **Documentación del Proyecto:** `Documentation/`
- **Implementación MVVM:** `Documentation/MVVM_Implementation.md`
- **Implementación de Base de Datos:** `Documentation/Database_Implementation.md`
- **Guía de Desarrollo:** `Documentation/Guides/Development_StepbyStep.md`

---

*Esta guía debe actualizarse conforme se implementen los repositorios concretos y se descubran nuevos patrones de uso.*
