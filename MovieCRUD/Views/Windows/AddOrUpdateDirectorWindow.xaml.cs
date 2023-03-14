using System.Windows;

namespace MovieCRUD.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddOrUpdateDirectorWindow.xaml
    /// </summary>
    public partial class AddOrUpdateDirectorWindow : Window
    {
        public AddOrUpdateDirectorWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
