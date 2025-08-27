using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ManagementBusiness.Views;

namespace ManagementBusiness.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        private string _currentPage;
        private string _currentPageTitle;
        private string _currentPageSubtitle;

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public string CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public string CurrentPageTitle
        {
            get => _currentPageTitle;
            set => SetProperty(ref _currentPageTitle, value);
        }

        public string CurrentPageSubtitle
        {
            get => _currentPageSubtitle;
            set => SetProperty(ref _currentPageSubtitle, value);
        }

        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel()
        {
            NavigateCommand = new RelayCommand(Navigate);
            LogoutCommand = new RelayCommand(Logout);

            // Navegar a la página de inicio por defecto
            NavigateToHome();
        }

        private void Navigate(object? parameter)
        {
            if (parameter is not string pageName) return;
            
            CurrentPage = pageName;
            
            switch (pageName)
            {
                case "Home":
                    CurrentView = new HomeView();
                    CurrentPageTitle = "Panel de Control";
                    CurrentPageSubtitle = "Bienvenido al sistema de gestión empresarial";
                    break;
                case "Customers":
                    CurrentView = new CustomersView();
                    CurrentPageTitle = "Gestión de Clientes";
                    CurrentPageSubtitle = "Administra la información de tus clientes";
                    break;
                case "Products":
                    CurrentView = new ProductsView();
                    CurrentPageTitle = "Gestión de Productos";
                    CurrentPageSubtitle = "Controla tu inventario y catálogo de productos";
                    break;
                case "Orders":
                    CurrentView = new OrdersView();
                    CurrentPageTitle = "Gestión de Pedidos";
                    CurrentPageSubtitle = "Seguimiento y administración de pedidos";
                    break;
                case "Transactions":
                    CurrentView = new TransactionsView();
                    CurrentPageTitle = "Transacciones";
                    CurrentPageSubtitle = "Historial y análisis de transacciones";
                    break;
                case "Shipments":
                    CurrentView = new ShipmentsView();
                    CurrentPageTitle = "Gestión de Envíos";
                    CurrentPageSubtitle = "Control de logística y entregas";
                    break;
                case "Settings":
                    CurrentView = new SettingsView();
                    CurrentPageTitle = "Configuración";
                    CurrentPageSubtitle = "Personaliza tu experiencia";
                    break;
                default:
                    CurrentView = new HomeView();
                    CurrentPageTitle = "Panel de Control";
                    CurrentPageSubtitle = "Bienvenido al sistema de gestión empresarial";
                    break;
            }
        }

        private void NavigateToHome()
        {
            CurrentPage = "Home";
            CurrentView = new HomeView();
            CurrentPageTitle = "Panel de Control";
            CurrentPageSubtitle = "Bienvenido al sistema de gestión empresarial";
        }

        private void Logout(object? parameter)
        {
            var result = MessageBox.Show(
                "¿Estás seguro de que quieres cerrar sesión?",
                "Confirmar Cierre de Sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Aquí puedes implementar la lógica de cierre de sesión
                MessageBox.Show("Sesión cerrada exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
