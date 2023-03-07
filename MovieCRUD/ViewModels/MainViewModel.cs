using MovieCRUD.Models;
using MovieCRUD.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.ViewModels
{
    internal class MainViewModel : BaseNotifier

    {
        public ObservableCollection<Director> Directors { get; private set; }
       
        private Director? _selectedDirector;

        public DeleteDirectorCommand DeleteDirectorCommand { get; private set; } = new DeleteDirectorCommand();

        public AddDirectorCommand AddDirectorCommand { get; private set; }

        // Nome da propriedade?
        public UpdateDirectorCommand UpdateDirectorCommand { get; private set; }

        public Director? SelectedDirector
        {
            get { return _selectedDirector; }
            set
            {
                _selectedDirector = value;
                OnPropertyChanged(nameof(SelectedDirector));
                DeleteDirectorCommand.RaiseCanExecuteChanged();
                UpdateDirectorCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel()
        {
            AddDirectorCommand = new AddDirectorCommand(this);
            UpdateDirectorCommand = new UpdateDirectorCommand(this);
            Directors = new ObservableCollection<Director>();
            Directors.Add(new Director()
            {
                Name = "David Fincher",
                Nationality = "US",
                YearOfBirth = 1962
            });
            Directors.Add(new Director()
            {
                Name = "Christopher Nolan",
                Nationality = "UK",
                YearOfBirth = 1970
            });
            SelectedDirector = Directors.FirstOrDefault();
        }
    }
}
