using MovieCRUD.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Models.DbContext
{
    public abstract class IDbContext
    {
        private readonly string connectionString;
        public abstract List<Director> GetDirectors();
        public abstract Director AddDirector(Director newDirector);
        public abstract void DeleteDirector(int directorId);
        public abstract void UpdateDirector(Director directorToUpdate);
        public abstract List<Movie> GetMoviesByDirector(int directorId);
        public abstract Movie AddMovie(Movie newMovie);
        public abstract void DeleteMovie(int movieId);
        public abstract void UpdateMovie(Movie movieToUpdate);
    }
}
