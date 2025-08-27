using ManagementBusiness.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace ManagementBusiness.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _title = "Sistema de Gestión Empresarial";
        private string _statusMessage = "Listo";
        private bool _isBusy;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand RefreshCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand HelpCommand { get; }

        public MainViewModel()
        {
            RefreshCommand = new RelayCommand(ExecuteRefresh);
            SettingsCommand = new RelayCommand(ExecuteSettings);
            HelpCommand = new RelayCommand(ExecuteHelp);
        }

        private void ExecuteRefresh(object? parameter)
        {
            StatusMessage = "Actualizando...";
            IsBusy = true;
            
            // Simular operación asíncrona
            Task.Delay(1000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StatusMessage = "Actualizado correctamente";
                    IsBusy = false;
                });
            });
        }

        private void ExecuteSettings(object? parameter)
        {
            StatusMessage = "Abriendo configuración...";
            // Aquí se abriría la ventana de configuración
        }

        private void ExecuteHelp(object? parameter)
        {
            StatusMessage = "Abriendo ayuda...";
            // Aquí se abriría la ventana de ayuda
        }
    }
}
