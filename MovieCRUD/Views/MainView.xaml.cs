using MovieCRUD.Models.DbContext;
using MovieCRUD.ViewModels;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MovieCRUD.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            PgContext pgContext = new();
            DataContext = new MainViewModel(pgContext);
            InitializeComponent();
        }
    }
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
