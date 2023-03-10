using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MovieCRUD.Models.Enums;

namespace MovieCRUD.Models.Repositories
{
    internal class PgContext : IDbContext
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        public PgContext()
        {
            
        }

        public override List<Director> GetDirectors()
        {

            List<Director> directors = new List<Director>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM directors";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Director director = new Director
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            YearOfBirth = (int)reader["year_of_birth"],
                            Nationality = (string)reader["nationality"]
                        };

                        directors.Add(director);
                    }

                    reader.Close();
                }
            };
            return directors;
        }
        public override Director AddDirector(string name, int yearOfBirth, string nationality)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO directors (name, year_of_birth, nationality) VALUES (@name, @yearOfBirth, @nationality) RETURNING id";
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("yearOfBirth", yearOfBirth);
                    cmd.Parameters.AddWithValue("nationality", nationality);

                    object? result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int newDirectorId = (int)result;

                        return new Director
                        {
                            Id = newDirectorId,
                            Name = name,
                            YearOfBirth = yearOfBirth,
                            Nationality = nationality
                        };
                    }
                    else
                    {
                        throw new Exception("Failed to add director.");
                    }
                }
            }
        }
        public override Movie AddMovie(int directorId, string title, DateTime releaseDate, Genre genre)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO movies (title, date_of_release, genre, director_id) VALUES (@title, @releaseDate, @genre, @directorId) RETURNING id";
                    cmd.Parameters.AddWithValue("title", title);
                    cmd.Parameters.AddWithValue("releaseDate", releaseDate);
                    cmd.Parameters.AddWithValue("genre", genre.ToString());
                    cmd.Parameters.AddWithValue("directorId", directorId);

                    object? result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int newMovieId = (int)result;

                        return new Movie
                        {
                            Id = newMovieId,
                            DateOfRelease = releaseDate,
                            DirectorId = directorId,
                            MovieGenre = genre,
                            Title = title
                        };
                    }
                    else
                    {
                        throw new Exception("Failed to add movie.");
                    }
                }
            }
        }
        public override void DeleteMovie(int movieId)
        {
            using (NpgsqlConnection connection = new(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM movies WHERE id = @movieId";
                    cmd.Parameters.AddWithValue("movieId", movieId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public override void UpdateMovie(Movie movieToUpdate)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE movies SET title = @title, date_of_release = @date_of_release, genre = @genre WHERE id = @id";
                    cmd.Parameters.AddWithValue("title", movieToUpdate.Title);
                    cmd.Parameters.AddWithValue("date_of_release", movieToUpdate.DateOfRelease);
                    cmd.Parameters.AddWithValue("genre", movieToUpdate.MovieGenre.ToString());
                    cmd.Parameters.AddWithValue("id", movieToUpdate.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public override void DeleteDirector(int directorId)
        {
            using (NpgsqlConnection connection = new(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM directors WHERE id = @directorId";
                    command.Parameters.AddWithValue("directorId", directorId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public override void UpdateDirector(Director directorToUpdate)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE directors SET name = @name, year_of_birth = @yearOfBirth, nationality = @nationality WHERE id = @id";
                    cmd.Parameters.AddWithValue("name", directorToUpdate.Name);
                    cmd.Parameters.AddWithValue("yearOfBirth", directorToUpdate.YearOfBirth);
                    cmd.Parameters.AddWithValue("nationality", directorToUpdate.Nationality);
                    cmd.Parameters.AddWithValue("id", directorToUpdate.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public override List<Movie> GetMoviesByDirector(int directorId)
        {
            var movies = new List<Movie>();

            using (NpgsqlConnection connection = new(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id, title, date_of_release, genre, director_id FROM movies WHERE director_id = @directorId";
                    command.Parameters.AddWithValue("directorId", directorId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var movie = new Movie
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                DateOfRelease = reader.GetDateTime(2),
                                MovieGenre = ParseGenre(reader.GetString(3)),
                                DirectorId = reader.GetInt32(4)
                            };
                            movies.Add(movie);
                        }
                    }
                }
            }

            return movies;
        }
        

    }
}
