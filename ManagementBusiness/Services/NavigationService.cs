using System;
using System.Collections.Generic;
using System.Windows;

namespace ManagementBusiness.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<Type> _navigationStack = new();
        private readonly Dictionary<Type, object> _viewModels = new();

        public void NavigateTo<T>() where T : class
        {
            var type = typeof(T);
            _navigationStack.Push(type);
            
            // Aquí se implementaría la lógica de navegación real
            // Por ahora solo registramos la navegación
        }

        public void NavigateBack()
        {
            if (_navigationStack.Count > 1)
            {
                _navigationStack.Pop();
                var previousType = _navigationStack.Peek();
                
                // Aquí se implementaría la navegación hacia atrás
            }
        }

        public void ShowDialog<T>() where T : class
        {
            var type = typeof(T);
            
            // Aquí se implementaría la lógica para mostrar diálogos
            // Por ahora solo registramos la acción
        }

        public void CloseDialog()
        {
            // Aquí se implementaría la lógica para cerrar diálogos
        }

        public T GetViewModel<T>() where T : class
        {
            var type = typeof(T);
            if (!_viewModels.ContainsKey(type))
            {
                // Crear una nueva instancia del ViewModel
                var instance = Activator.CreateInstance(type);
                if (instance != null)
                {
                    _viewModels[type] = instance;
                }
            }
            
            return _viewModels[type] as T ?? throw new InvalidOperationException($"Could not create ViewModel of type {type.Name}");
        }
    }
}
