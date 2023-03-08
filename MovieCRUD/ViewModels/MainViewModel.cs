using MovieCRUD.Models;
using MovieCRUD.ViewModels.Commands;
using MovieCRUD.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieCRUD.ViewModels
{
    internal class MainViewModel : BaseNotifier

    {
        public ObservableCollection<Director> Directors { get; private set; }

        private Director? _selectedDirector;

        private Movie? _selectedMovie;
        public ICommand DeleteDirectorCommand { get; private set; }
        public ICommand AddDirectorCommand { get; private set; }
        public ICommand UpdateDirectorCommand { get; private set; }
        public ICommand AddMovieCommand { get; private set; }
        public ICommand UpdateMovieCommand { get; private set; }
        public ICommand DeleteMovieCommand { get; private set; }


        public Director? SelectedDirector
        {
            get { return _selectedDirector; }
            set
            {
                _selectedDirector = value;
                OnPropertyChanged(nameof(SelectedDirector));
            }
        }

        public Movie? SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }

        public MainViewModel()
        {
            Directors = new ObservableCollection<Director> { };
            Directors.Add(new Director()
            {
                Name = "David Fincher",
                Nationality = "US",
                YearOfBirth = 1962,
                Movies = new ObservableCollection<Movie> {
                    new Movie() {
                        Title = "Seven",
                        DateOfRelease = new DateTime(1995, 12, 1),
                        MovieGenre = Genre.Drama}
                }
            });
            Directors.Add(new Director()
            {
                Name = "Christopher Nolan",
                Nationality = "UK",
                YearOfBirth = 1970,
                Movies = new ObservableCollection<Movie> { }
            });

            StartCommands();
        }

        public void StartCommands()
        {
            AddDirectorCommand = new RelayCommand(
                (object _) =>
                {
                    Director newDirector = new()
                    {
                        Movies = new ObservableCollection<Movie> { }
                    };

                    AddDirectorWindow addDirectorWindow = new AddDirectorWindow
                    {
                        DataContext = newDirector
                    };

                    addDirectorWindow.ShowDialog();

                    if (addDirectorWindow.DialogResult.HasValue && addDirectorWindow.DialogResult.Value)
                    {
                        Directors.Add(newDirector);
                        SelectedDirector = newDirector;
                    }
                });

            UpdateDirectorCommand = new RelayCommand(
                (object _) =>
                {
                    Director directorToUpdate = (Director)SelectedDirector.Clone();

                    UpdateDirectorWindow updateDirectorWindow = new()
                    {
                        DataContext = directorToUpdate
                    };

                    updateDirectorWindow.ShowDialog();

                    if (updateDirectorWindow.DialogResult.HasValue && updateDirectorWindow.DialogResult.Value)
                    {
                        CopyProperties.CopyObj(directorToUpdate, SelectedDirector);
                    }

                },
                (object _) =>
                {
                    return SelectedDirector != null;
                });
            DeleteDirectorCommand = new RelayCommand(
                (object _) =>
                {
                    Directors.Remove(SelectedDirector);
                    SelectedDirector = Directors.FirstOrDefault();
                },
                (object _) =>
                {
                    return SelectedDirector != null;
                });
            AddMovieCommand = new RelayCommand(
                (object _) => {
                    Movie newMovie = new() { 
                    Director = _selectedDirector,
                    DateOfRelease = DateTime.Today
                    };
                    AddMovieWindow addMovieWindow = new()
                    {
                        DataContext = newMovie
                    };

                    addMovieWindow.ShowDialog();

                    if (addMovieWindow.DialogResult.HasValue && addMovieWindow.DialogResult.Value)
                    {
                        SelectedDirector.Movies.Add(newMovie);
                        SelectedMovie = newMovie;
                    }
                },
                (object _) =>
                {
                    return SelectedDirector != null;
                });
            UpdateMovieCommand = new RelayCommand(
                (object _) =>
                {
                    Movie movieToUpdate = (Movie)SelectedMovie.Clone();

                    UpdateMovieWindow updateMovieWindow = new()
                    {
                        DataContext = movieToUpdate
                    };

                    updateMovieWindow.ShowDialog();

                    if (updateMovieWindow.DialogResult.HasValue && updateMovieWindow.DialogResult.Value)
                    {
                        CopyProperties.CopyObj(movieToUpdate, SelectedMovie);
                    }

                },
                (object _) =>
                {
                    return SelectedMovie != null;
                });
            DeleteMovieCommand = new RelayCommand(
                (object _) =>
                {
                    SelectedDirector.Movies.Remove(SelectedMovie);
                    SelectedMovie = SelectedDirector.Movies.FirstOrDefault();
                },
                (object _) =>
                {
                    return SelectedMovie != null;
                });

        }
    }
}
