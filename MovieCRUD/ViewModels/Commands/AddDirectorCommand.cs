using MovieCRUD.Models;
using MovieCRUD.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.ViewModels.Commands
{
    class AddDirectorCommand : BaseCommand

    {
        private MainViewModel _viewModel;
        public AddDirectorCommand(MainViewModel viewModel) {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)

        {
            Director newDirector = new();

            var addDirectorWindow = new AddDirectorWindow
            {
                DataContext = newDirector
            };

            addDirectorWindow.ShowDialog();

            if (addDirectorWindow.DialogResult.HasValue && addDirectorWindow.DialogResult.Value)
            {
                _viewModel.Directors.Add(newDirector);
                _viewModel.SelectedDirector = newDirector;
            }


        }
    }
}
