using System.Collections.Generic;

namespace MovieCRUD.Models.DbContext
{
    public interface IDbContext
    {
        List<Director> GetDirectors();
        Director AddDirector(Director newDirector);
        void DeleteDirector(int directorId);
        void UpdateDirector(Director directorToUpdate);
        List<Movie> GetMoviesByDirector(int directorId);
        Movie AddMovie(Movie newMovie);
        void DeleteMovie(int movieId);
        void UpdateMovie(Movie movieToUpdate);
    }
}
