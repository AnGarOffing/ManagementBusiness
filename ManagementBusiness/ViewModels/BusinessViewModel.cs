using ManagementBusiness.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ManagementBusiness.ViewModels
{
    public class BusinessViewModel : BaseViewModel
    {
        private static readonly BusinessViewModel _instance = new();
        public static BusinessViewModel Instance => _instance;

        private ObservableCollection<Business> _businesses;
        private Business? _selectedBusiness;
        private string _searchText = string.Empty;

        public ObservableCollection<Business> Businesses
        {
            get => _businesses;
            set => SetProperty(ref _businesses, value);
        }

        public Business? SelectedBusiness
        {
            get => _selectedBusiness;
            set => SetProperty(ref _selectedBusiness, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterBusinesses();
                }
            }
        }

        public ICommand AddBusinessCommand { get; }
        public ICommand EditBusinessCommand { get; }
        public ICommand DeleteBusinessCommand { get; }
        public ICommand ClearSearchCommand { get; }

        private BusinessViewModel()
        {
            _businesses = new ObservableCollection<Business>();
            AddBusinessCommand = new RelayCommand(ExecuteAddBusiness);
            EditBusinessCommand = new RelayCommand(ExecuteEditBusiness, CanExecuteEditBusiness);
            DeleteBusinessCommand = new RelayCommand(ExecuteDeleteBusiness, CanExecuteDeleteBusiness);
            ClearSearchCommand = new RelayCommand(ExecuteClearSearch);

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            Businesses.Add(new Business("TechCorp", "Empresa de tecnología", 1500000));
            Businesses.Add(new Business("GreenEnergy", "Energías renovables", 2500000));
            Businesses.Add(new Business("FoodService", "Servicios de alimentación", 800000));
        }

        private void ExecuteAddBusiness(object? parameter)
        {
            var newBusiness = new Business("Nuevo Negocio", "Descripción del negocio", 0);
            Businesses.Add(newBusiness);
            SelectedBusiness = newBusiness;
        }

        private void ExecuteEditBusiness(object? parameter)
        {
            if (SelectedBusiness != null)
            {
                // Aquí se abriría la ventana de edición
                // Por ahora solo cambiamos el estado
                SelectedBusiness.IsActive = !SelectedBusiness.IsActive;
            }
        }

        private bool CanExecuteEditBusiness(object? parameter)
        {
            return SelectedBusiness != null;
        }

        private void ExecuteDeleteBusiness(object? parameter)
        {
            if (SelectedBusiness != null)
            {
                Businesses.Remove(SelectedBusiness);
                SelectedBusiness = null;
            }
        }

        private bool CanExecuteDeleteBusiness(object? parameter)
        {
            return SelectedBusiness != null;
        }

        private void ExecuteClearSearch(object? parameter)
        {
            SearchText = string.Empty;
        }

        private void FilterBusinesses()
        {
            // Implementar filtrado si es necesario
            // Por ahora solo actualizamos la UI
            OnPropertyChanged(nameof(Businesses));
        }
    }
}
