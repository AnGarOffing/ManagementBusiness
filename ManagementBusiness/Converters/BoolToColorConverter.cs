using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ManagementBusiness.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && parameter is string pageName)
            {
                // Obtener el ViewModel principal para verificar la página actual
                var mainWindow = System.Windows.Application.Current.MainWindow;
                if (mainWindow?.DataContext is ViewModels.MainViewModel mainViewModel)
                {
                    var currentPage = mainViewModel.CurrentPage;
                    if (currentPage == pageName)
                    {
                        return new SolidColorBrush(Colors.White); // Color para página seleccionada
                    }
                }
            }
            
            // Color por defecto para páginas no seleccionadas
            return new SolidColorBrush(Color.FromRgb(148, 163, 184)); // #94A3B8
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
