using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.ViewModels.Commands
{
    internal class DeleteDirectorCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter)
        {
            var viewModel = parameter as MainViewModel;
            return viewModel != null && viewModel.SelectedDirector != null;
        }

        public override void Execute(object? parameter)
        {
            var viewModel = (MainViewModel)parameter;
            viewModel.Directors.Remove(viewModel.SelectedDirector);
            viewModel.SelectedDirector = viewModel.Directors.FirstOrDefault();
        }
    }
}
