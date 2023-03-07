using MovieCRUD.Models;
using MovieCRUD.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.ViewModels.Commands
{
    class UpdateDirectorCommand : BaseCommand
    {
        private MainViewModel _viewModel;
        public UpdateDirectorCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel != null && _viewModel.SelectedDirector != null;
        }
        public override void Execute(object? parameter)

        {
            Director directorToUpdate = (Director)_viewModel.SelectedDirector.Clone();

            var updateDirectorWindow = new UpdateDirectorWindow
            {
                DataContext = directorToUpdate
            };

            updateDirectorWindow.ShowDialog();

            if (updateDirectorWindow.DialogResult.HasValue && updateDirectorWindow.DialogResult.Value)
            {
                _viewModel.SelectedDirector.Name = directorToUpdate.Name;
                _viewModel.SelectedDirector.YearOfBirth = directorToUpdate.YearOfBirth;
                _viewModel.SelectedDirector.Nationality = directorToUpdate.Nationality;
            }


        }
    }
}
