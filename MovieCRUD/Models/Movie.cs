using MovieCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieCRUD.Models
{
    public class Movie : BaseNotifier, ICloneable
    {

        private string _title;
        private DateTime _dateOfRelease;
        private Director? _director;
        private Genre _genre;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public DateTime DateOfRelease
        {
            get { return _dateOfRelease; }
            set
            {
                _dateOfRelease = value;
                OnPropertyChanged(nameof(DateOfRelease));
            }
        }

        public Director? Director

        {
            get { return _director; }
            set { _director = value; }
        }


        public Genre MovieGenre
        {
            get { return _genre; }
            set
            {
                if (_genre != value)
                {
                    _genre = value;
                    OnPropertyChanged(nameof(MovieGenre));
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
