using MovieCRUD.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Models
{
    public interface IMovieValidator
    {
        void Validate(Movie movie);
    }

    public class MovieValidator : IMovieValidator
    {
        public MovieValidator()
        {
        }

        public void Validate(Movie movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Title))
            {
                throw new ArgumentException("Title is required");
            }

            if (!Enum.IsDefined(typeof(Genre), movie.MovieGenre))
            {
                throw new ArgumentException("Invalid genre");
            }

            if (movie.DirectorId <= 0)
            {
                throw new ArgumentException("Director ID must be greater than zero");
            }

            if (movie.DateOfRelease < new DateTime(1900, 1, 1) || movie.DateOfRelease > DateTime.Now)
            {
                throw new ArgumentException("Release date must be between 1900 and the current date");
            }
        }
    }
}
