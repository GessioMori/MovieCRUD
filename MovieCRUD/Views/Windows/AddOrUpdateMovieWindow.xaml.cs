using System.Windows;

namespace MovieCRUD.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddOrUpdateMovieWindow.xaml
    /// </summary>
    public partial class AddOrUpdateMovieWindow : Window
    {
        public AddOrUpdateMovieWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
