using MovieCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Models
{
    public class Director: BaseNotifier, ICloneable
    {
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
