using MovieCRUD.ViewModels.ViewModelUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Models
{
    public class Director : BaseNotifier, ICloneable
    {
        private int _id;
        public int Id
        {
            get
            { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private int _yearOfBirth;
        public int YearOfBirth
        {
            get { return _yearOfBirth; }
            set
            {
                if (_yearOfBirth != value)
                {
                    _yearOfBirth = value;
                    OnPropertyChanged(nameof(YearOfBirth));
                }
            }
        }
        private string _nationality;
        public string Nationality
        {
            get { return _nationality; }
            set
            {
                if (_nationality != value)
                {
                    _nationality = value;
                    OnPropertyChanged(nameof(Nationality));
                }
            }
        }

        private ObservableCollection<Movie> _movies;

        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void CopyFromAnotherDirector(Director otherDirector)
        {
            Name = otherDirector.Name;
            YearOfBirth = otherDirector.YearOfBirth;
            Nationality = otherDirector.Nationality;
        }
    }
}
