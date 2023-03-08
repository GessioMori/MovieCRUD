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
        private DateOnly _dateOfRelease;
        private Director _director;
        private Genre _genre;

        // Onde colocar?
        public enum Genre
        {
            Action,
            Comedy,
            Drama,
            Fantasy,
            Horror,
            Mystery,
            Romance,
            Thriller
        }
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

        public DateOnly DateOfRelease
        {
            get { return _dateOfRelease; }
            set
            {
                if (_dateOfRelease != value)
                {
                    _dateOfRelease = value;
                    OnPropertyChanged(nameof(DateOfRelease));
                }
            }
        }

        public Director Director

        {
            get { return _director; }
            private set { _director = value; }
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
