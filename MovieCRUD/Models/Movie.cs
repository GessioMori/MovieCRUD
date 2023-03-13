using MovieCRUD.Models.Enums;
using MovieCRUD.ViewModels.ViewModelUtils;
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
        private int _id;
        private string _title;
        private DateTime _dateOfRelease;
        private int _directorId;
        private Genre _genre;
        
        public int Id
        {
            get
            { return _id; }
            set { _id = value; }
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

        public DateTime DateOfRelease
        {
            get { return _dateOfRelease; }
            set
            {
                _dateOfRelease = value;
                OnPropertyChanged(nameof(DateOfRelease));
            }
        }

        public int DirectorId

        {
            get { return _directorId; }
            set { _directorId = value; }
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

        public void CopyFromAnotherMovie(Movie otherMovie)
        {
            Title = otherMovie.Title;
            DateOfRelease = otherMovie.DateOfRelease;
            MovieGenre = otherMovie.MovieGenre;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                throw new ArgumentException("Title is required");
            }

            if (!Enum.IsDefined(typeof(Genre), MovieGenre))
            {
                throw new ArgumentException("Invalid genre");
            }

            if (DirectorId <= 0)
            {
                throw new ArgumentException("Director ID must be greater than zero");
            }

            if (DateOfRelease < new DateTime(1900, 1, 1) || DateOfRelease > DateTime.Now)
            {
                throw new ArgumentException("Release date must be between 1900 and the current date");
            }
        }
    }
}
