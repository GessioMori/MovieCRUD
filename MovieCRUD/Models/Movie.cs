using MovieCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Models
{
    public class Movie : BaseNotifier, ICloneable
    {

        private string _title;
        private DateOnly _dateOfRelease;
        private Director _director;
        private Genre _genre;
        private string? _imdbUrl;
       
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
        public string Title { get; set; }

        public DateOnly DateOfRelease { get; set; }

        public Director Director { get; set; }

        public Genre MovieGenre { get; set; }

        public string? ImdbUrl { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
