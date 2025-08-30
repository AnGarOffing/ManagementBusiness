# **GUÍA DE IMPLEMENTACIÓN DEL REPOSITORIO BASE - MANAGEMENT BUSINESS**

## **📋 RESUMEN**

Este documento describe la implementación completa del repositorio base genérico usando Entity Framework Core. Se han implementado **4 clases de repositorio especializadas** que proporcionan una base sólida para todas las operaciones de datos de la aplicación.

---

## **🏗️ ARQUITECTURA IMPLEMENTADA**

### **Jerarquía de Implementaciones:**

```
Repository<T> (Base)
├── RepositoryWithId<T, TId> (Entidades con ID tipado)
├── RepositoryWithAudit<T> (Entidades con auditoría)
└── RepositoryWithSoftDelete<T> (Entidades con soft delete)
```

### **Características Principales:**

- **Implementación completa** de todas las interfaces definidas
- **Uso de Entity Framework Core** para operaciones de base de datos
- **Manejo robusto de errores** con try-catch y logging preparado
- **Soporte asíncrono** para todas las operaciones
- **Expresiones lambda dinámicas** para consultas flexibles
- **Validación de parámetros** en todas las operaciones
- **Métodos virtuales** para permitir sobrescritura en repositorios específicos

---

## **🔧 CLASES IMPLEMENTADAS**

### **1. Repository<T> - Repositorio Base**

**Archivo:** `Services/Repository.cs`

**Propósito:** Implementación base que implementa `IRepository<T>` usando Entity Framework Core.

**Características:**
- **Constructor protegido** que recibe `ManagementBusinessContext`
- **DbSet<T> protegido** para acceso a la tabla de entidades
- **Manejo de errores** con try-catch en todas las operaciones
- **Validación de parámetros** antes de ejecutar operaciones
- **Métodos virtuales** para permitir extensibilidad

**Operaciones Implementadas:**
- ✅ **CRUD Básico:** Add, Update, Remove, GetById, GetAll
- ✅ **Filtrado:** Get(predicate), GetFirst(predicate), GetFirstOrDefault(predicate)
- ✅ **Paginación:** GetPaged(pageNumber, pageSize)
- ✅ **Ordenamiento:** GetOrdered<TKey>(keySelector, ascending)
- ✅ **Conteo:** Count(), Any(), Count(predicate)
- ✅ **Inclusión:** GetWithIncludes(includes)
- ✅ **Proyección:** GetProjected<TResult>(selector)

**Ejemplo de Uso:**
```csharp
public class ClienteRepository : Repository<Cliente>
{
    public ClienteRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    // Operaciones base ya implementadas
    // Solo agregar operaciones específicas de cliente
}
```

---

### **2. RepositoryWithId<T, TId> - Repositorio con ID Tipado**

**Archivo:** `Services/RepositoryWithId.cs`

**Propósito:** Extiende `Repository<T>` para implementar `IRepositoryWithId<T, TId>` con operaciones específicas para entidades con ID tipado.

**Características:**
- **Herencia de Repository<T>** para reutilizar operaciones base
- **Uso de expresiones lambda dinámicas** para consultas por ID
- **Operaciones de rango** para IDs numéricos
- **Generación automática** de siguiente ID y ID máximo

**Operaciones Implementadas:**
- ✅ **Por ID:** GetById(TId), Exists(TId), RemoveById(TId)
- ✅ **Múltiples IDs:** GetByIds(ids), RemoveByIds(ids)
- ✅ **Rangos:** GetByIdRange(startId, endId), GetByIdGreaterThan(id), GetByIdLessThan(id)
- ✅ **Conteo por ID:** CountByIdRange, CountByIdGreaterThan, CountByIdLessThan
- ✅ **Operaciones de ID:** GetNextId(), GetMaxId()

**Ejemplo de Uso:**
```csharp
public class ProductoRepository : RepositoryWithId<Producto, int>
{
    public ProductoRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    // Operaciones base + operaciones de ID ya implementadas
    public IEnumerable<Producto> GetEnStock()
    {
        return GetActive(p => p.Stock > 0);
    }
}
```

---

### **3. RepositoryWithAudit<T> - Repositorio con Auditoría**

**Archivo:** `Services/RepositoryWithAudit.cs`

**Propósito:** Extiende `Repository<T>` para implementar `IRepositoryWithAudit<T>` con operaciones de auditoría por fecha y usuario.

**Características:**
- **Consultas por fecha** con manejo de rangos de día
- **Filtros por usuario** creador y modificador
- **Períodos predefinidos** (hoy, última semana, último mes)
- **Historial de cambios** básico (preparado para extensión)

**Operaciones Implementadas:**
- ✅ **Por Fecha:** GetByCreatedDate(date), GetByModifiedDate(date)
- ✅ **Rangos de Fecha:** GetByCreatedDateRange(start, end), GetByModifiedDateRange(start, end)
- ✅ **Períodos:** GetCreatedToday(), GetCreatedLastWeek(), GetCreatedLastMonth()
- ✅ **Por Usuario:** GetByCreatedBy(user), GetByModifiedBy(user)
- ✅ **Combinadas:** GetByCreatedByAndDateRange(user, start, end)
- ✅ **Conteo:** CountByCreatedDate, CountByCreatedBy, CountByModifiedBy
- ✅ **Avanzadas:** GetChangeHistory, GetNotModifiedSince, GetCreatedButNeverModified

**Ejemplo de Uso:**
```csharp
public class PedidoRepository : RepositoryWithAudit<Pedido>
{
    public PedidoRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    // Operaciones base + auditoría ya implementadas
    public IEnumerable<Pedido> GetPedidosDelMes()
    {
        return GetCreatedLastMonth();
    }
}
```

---

### **4. RepositoryWithSoftDelete<T> - Repositorio con Soft Delete**

**Archivo:** `Services/RepositoryWithSoftDelete.cs`

**Propósito:** Extiende `Repository<T>` para implementar `IRepositoryWithSoftDelete<T>` con operaciones de eliminación lógica.

**Características:**
- **Soft Delete automático** con marcado de propiedades
- **Restauración** de entidades eliminadas
- **Consultas separadas** para entidades activas y eliminadas
- **Limpieza automática** de entidades eliminadas antiguas
- **Auditoría de soft delete** por fecha y usuario

**Operaciones Implementadas:**
- ✅ **Soft Delete:** SoftDelete(entity), SoftDeleteRange(entities), SoftDeleteWhere(predicate)
- ✅ **Restauración:** Restore(entity), RestoreRange(entities), RestoreWhere(predicate)
- ✅ **Consultas:** GetActive(), GetDeleted(), GetActive(predicate), GetDeleted(predicate)
- ✅ **Paginación:** GetActivePaged(), GetDeletedPaged()
- ✅ **Conteo:** CountActive(), CountDeleted(), CountActive(predicate), CountDeleted(predicate)
- ✅ **Verificación:** AnyActive(), AnyDeleted(), AnyActive(predicate), AnyDeleted(predicate)
- ✅ **Limpieza:** PermanentlyDelete(), CleanupDeletedOlderThan(date)
- ✅ **Auditoría:** GetDeletedOnDate(), GetRestoredOnDate(), GetDeletedBy(), GetRestoredBy()

**Ejemplo de Uso:**
```csharp
public class ClienteRepository : RepositoryWithSoftDelete<Cliente>
{
    public ClienteRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    // Operaciones base + soft delete ya implementadas
    public IEnumerable<Cliente> GetClientesActivosPorCategoria(string categoria)
    {
        return GetActive(c => c.Categoria == categoria);
    }
}
```

---

## **💡 PATRONES DE IMPLEMENTACIÓN**

### **Patrón 1: Repositorio Simple**
```csharp
public interface IClienteRepository : IRepository<Cliente>
{
    IEnumerable<Cliente> GetByCategoria(string categoria);
}

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    public IEnumerable<Cliente> GetByCategoria(string categoria)
    {
        return Get(c => c.Categoria == categoria);
    }
}
```

### **Patrón 2: Repositorio con ID Tipado**
```csharp
public interface IProductoRepository : IRepositoryWithId<Producto, int>
{
    IEnumerable<Producto> GetEnStock();
}

public class ProductoRepository : RepositoryWithId<Producto, int>, IProductoRepository
{
    public ProductoRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    public IEnumerable<Producto> GetEnStock()
    {
        return Get(p => p.Stock > 0);
    }
}
```

### **Patrón 3: Repositorio con Auditoría**
```csharp
public interface IPedidoRepository : IRepositoryWithAudit<Pedido>
{
    IEnumerable<Pedido> GetPorEstado(string estado);
}

public class PedidoRepository : RepositoryWithAudit<Pedido>, IPedidoRepository
{
    public PedidoRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    public IEnumerable<Pedido> GetPorEstado(string estado)
    {
        return Get(p => p.Estado == estado);
    }
}
```

### **Patrón 4: Repositorio Completo**
```csharp
public interface IClienteRepository : 
    IRepositoryWithId<Cliente, int>,
    IRepositoryWithAudit<Cliente>,
    IRepositoryWithSoftDelete<Cliente>
{
    IEnumerable<Cliente> GetByCategoria(string categoria);
}

public class ClienteRepository : 
    RepositoryWithSoftDelete<Cliente>, 
    IClienteRepository
{
    public ClienteRepository(ManagementBusinessContext context) : base(context)
    {
    }
    
    public IEnumerable<Cliente> GetByCategoria(string categoria)
    {
        return GetActive(c => c.Categoria == categoria);
    }
}
```

---

## **🚀 CARACTERÍSTICAS TÉCNICAS**

### **1. Manejo de Errores**
- **Try-catch** en todas las operaciones
- **Validación de parámetros** antes de ejecutar operaciones
- **Mensajes de error descriptivos** para debugging
- **Preparado para logging** (TODO comments para implementación futura)

### **2. Expresiones Lambda Dinámicas**
- **Generación de predicados** en tiempo de ejecución
- **Combinación de condiciones** para consultas complejas
- **Reflection** para acceso a propiedades de entidades
- **Optimización** para Entity Framework

### **3. Soporte Asíncrono**
- **Task.CompletedTask** para operaciones síncronas simples
- **Async/await** para operaciones de base de datos
- **Consistencia** entre métodos síncronos y asíncronos

### **4. Extensibilidad**
- **Métodos virtuales** para sobrescritura
- **Herencia múltiple** para composición de funcionalidades
- **Interfaces bien definidas** para contratos claros

---

## **🔍 CONSIDERACIONES DE IMPLEMENTACIÓN**

### **1. Propiedades de Entidad**
**Para Auditoría:**
- `CreatedDate` (DateTime)
- `ModifiedDate` (DateTime?)
- `CreatedBy` (string)
- `ModifiedBy` (string?)

**Para Soft Delete:**
- `IsDeleted` (bool)
- `DeletedDate` (DateTime?)
- `DeletedBy` (string?)
- `RestoredDate` (DateTime?) - Opcional
- `RestoredBy` (string?) - Opcional

### **2. Contexto de Base de Datos**
- **Inyección obligatoria** en constructor
- **Validación null** con ArgumentNullException
- **Acceso protegido** para clases derivadas

### **3. Operaciones de Soft Delete**
- **Marcado automático** de propiedades
- **Estado EntityState.Modified** para detección de cambios
- **Propiedades opcionales** con verificación de existencia

### **4. Consultas por Fecha**
- **Conversión a Date** para comparaciones precisas
- **Rangos inclusivos** con manejo de días completos
- **UTC** para fechas de eliminación

---

## **📊 ESTADO DE IMPLEMENTACIÓN**

### **✅ COMPLETADO:**
- [x] **Repository<T>** - Implementación base completa
- [x] **RepositoryWithId<T, TId>** - Operaciones con ID tipado
- [x] **RepositoryWithAudit<T>** - Operaciones de auditoría
- [x] **RepositoryWithSoftDelete<T>** - Operaciones de soft delete
- [x] **Manejo de errores** en todas las operaciones
- [x] **Validación de parámetros** completa
- [x] **Soporte asíncrono** para todas las operaciones
- [x] **Documentación XML** en todos los métodos

### **🔄 PRÓXIMOS PASOS:**
- [ ] **Implementar** servicio de logging para errores
- [ ] **Crear** repositorios específicos para cada entidad
- [ ] **Implementar** Unit of Work pattern
- [ ] **Crear** servicios de negocio que usen estos repositorios
- [ ] **Implementar** pruebas unitarias para repositorios

---

## **🔗 ENLACES RELACIONADOS**

- **Interfaces de Repositorio:** `Documentation/Guides/Repository_Interfaces_Guide.md`
- **Implementación MVVM:** `Documentation/MVVM_Implementation.md`
- **Implementación de Base de Datos:** `Documentation/Database_Implementation.md`
- **Guía de Desarrollo:** `Documentation/Guides/Development_StepbyStep.md`

---

## **📝 NOTAS DE DESARROLLO**

### **Última Actualización:**
- **Fecha:** 27 de Agosto, 2025
- **Versión del Proyecto:** 1.0.2 (Repositorio Base Implementado)
- **Estado:** Repositorio base genérico completamente implementado y funcional

### **Dependencias Principales:**
- **.NET 8.0**
- **Entity Framework Core 9.0.8**
- **Microsoft.EntityFrameworkCore** (paquete NuGet)

### **Patrones de Diseño Utilizados:**
- **Repository Pattern** (implementación completa)
- **Template Method Pattern** (métodos virtuales)
- **Strategy Pattern** (diferentes tipos de repositorio)
- **Composition over Inheritance** (composición de funcionalidades)

---

*Esta guía debe actualizarse conforme se implementen repositorios específicos y se descubran nuevos patrones de uso.*
