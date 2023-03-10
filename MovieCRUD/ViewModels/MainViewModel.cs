using MovieCRUD.Models;
using MovieCRUD.Models.Repositories;
using MovieCRUD.ViewModels.ViewModelUtils;
using MovieCRUD.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieCRUD.ViewModels
{
    internal class MainViewModel : BaseNotifier

    {
        private IDbContext DbContext { get; set; }
        public ObservableCollection<Director> Directors { get; }

        private Director? _selectedDirector;

        private Movie? _selectedMovie;
        public ICommand DeleteDirector { get; private set; }
        public ICommand AddDirector { get; private set; }
        public ICommand UpdateDirector { get; private set; }
        public ICommand AddMovie { get; private set; }
        public ICommand UpdateMovie { get; private set; }
        public ICommand DeleteMovie { get; private set; }


        public Director? SelectedDirector
        {
            get { return _selectedDirector; }
            set
            {
                _selectedDirector = value;
                if (value != null)
                {
                    List<Movie> movies = DbContext.GetMoviesByDirector(value.Id);
                    _selectedDirector.Movies = new ObservableCollection<Movie>(movies);
                }
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

        public MainViewModel(IDbContext dbContext)
        {
            DbContext = dbContext;

            try
            {
                Directors = new ObservableCollection<Director>(DbContext.GetDirectors());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            StartCommands();
        }

        private void StartCommands()
        {
            AddDirector = new RelayCommand(
                (object _) =>
                {
                    Director newDirector = new()
                    {
                        Movies = new ObservableCollection<Movie> { }
                    };

                    AddOrUpdateDirectorWindow addDirectorWindow = new()
                    {
                        DataContext = newDirector
                    };

                    addDirectorWindow.ShowDialog();

                    if (addDirectorWindow.DialogResult.HasValue && addDirectorWindow.DialogResult.Value)
                    {
                        try
                        {
                            Director dbAddedDirector = DbContext.AddDirector(newDirector.Name, newDirector.YearOfBirth, newDirector.Nationality);
                            Directors.Add(dbAddedDirector);
                            SelectedDirector = dbAddedDirector;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                });

            UpdateDirector = new RelayCommand(
                (object _) =>
                {
                    Director directorToUpdate = (Director)SelectedDirector.Clone();

                    AddOrUpdateDirectorWindow updateDirectorWindow = new()
                    {
                        DataContext = directorToUpdate
                    };

                    updateDirectorWindow.ShowDialog();

                    if (updateDirectorWindow.DialogResult.HasValue && updateDirectorWindow.DialogResult.Value)
                    {
                        try
                        {
                            SelectedDirector.CopyFromAnotherDirector(directorToUpdate);
                            DbContext.UpdateDirector(SelectedDirector);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                },
                (object _) =>
                {
                    return SelectedDirector != null;
                });
            DeleteDirector = new RelayCommand(
                (object _) =>
                {
                    try
                    {
                        DbContext.DeleteDirector(SelectedDirector.Id);
                        Directors.Remove(SelectedDirector);
                        SelectedDirector = Directors.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                },
                (object _) =>
                {
                    return SelectedDirector != null;
                });
            AddMovie = new RelayCommand(
                (object _) =>
                {
                    Movie newMovie = new()
                    {
                        DirectorId = _selectedDirector.Id,
                        DateOfRelease = DateTime.Today
                    };
                    AddOrUpdateMovieWindow addMovieWindow = new()
                    {
                        DataContext = newMovie
                    };

                    addMovieWindow.ShowDialog();

                    if (addMovieWindow.DialogResult.HasValue && addMovieWindow.DialogResult.Value)
                    {
                        try
                        {
                            Movie dbAddedMovie = DbContext.AddMovie(SelectedDirector.Id, newMovie.Title, newMovie.DateOfRelease, newMovie.MovieGenre);
                            SelectedDirector.Movies.Add(dbAddedMovie);
                            SelectedMovie = dbAddedMovie;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                },
                (object _) =>
                {
                    return SelectedDirector != null;
                });
            UpdateMovie = new RelayCommand(
                (object _) =>
                {
                    Movie movieToUpdate = (Movie)SelectedMovie.Clone();

                    AddOrUpdateMovieWindow updateMovieWindow = new()
                    {
                        DataContext = movieToUpdate
                    };

                    updateMovieWindow.ShowDialog();

                    if (updateMovieWindow.DialogResult.HasValue && updateMovieWindow.DialogResult.Value)
                    {
                        try
                        {
                            SelectedMovie.CopyFromAnotherMovie(movieToUpdate);
                            DbContext.UpdateMovie(SelectedMovie);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                },
                (object _) =>
                {
                    return SelectedMovie != null;
                });
            DeleteMovie = new RelayCommand(
                (object _) =>
                {
                    try
                    {
                        DbContext.DeleteMovie(SelectedMovie.Id);
                        SelectedDirector.Movies.Remove(SelectedMovie);
                        SelectedMovie = SelectedDirector.Movies.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                },
                (object _) =>
                {
                    return SelectedMovie != null;
                });
        }
    }
}
