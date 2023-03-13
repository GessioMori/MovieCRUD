using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MovieCRUD.Models.Enums;
using System.Windows;

namespace MovieCRUD.Models.DbContext
{
    internal class PgContext : IDbContext
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public PgContext()
        {
        }
        public override List<Director> GetDirectors()
        {
            using (NpgsqlConnection connection = new(connectionString))
            {
                string query = "SELECT * FROM directors";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        List<Director> directors = new();

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

                        return directors;
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("It was not possible to get the directors. Error: " + ex.Message);
                    }

                }
            };

        }
        public override Director AddDirector(Director newDirector)
        {
            using (NpgsqlConnection connection = new(connectionString))
            {
                using (NpgsqlCommand cmd = new())
                {
                    try
                    {
                        connection.Open();
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO directors (name, year_of_birth, nationality) VALUES (@name, @yearOfBirth, @nationality) RETURNING id";
                        cmd.Parameters.AddWithValue("name", newDirector.Name);
                        cmd.Parameters.AddWithValue("yearOfBirth", newDirector.YearOfBirth);
                        cmd.Parameters.AddWithValue("nationality", newDirector.Nationality);

                        object? result = cmd.ExecuteScalar();

                        int newDirectorId = (int)result;

                        return new Director
                        {
                            Id = newDirectorId,
                            Name = newDirector.Name,
                            YearOfBirth = newDirector.YearOfBirth,
                            Nationality = newDirector.Nationality
                        };
                    }
                    catch (InvalidOperationException)
                    {
                        throw new Exception("Invalid data.");
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        public override Movie AddMovie(Movie newMovie)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                using (NpgsqlCommand cmd = new())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO movies (title, date_of_release, genre, director_id) VALUES (@title, @releaseDate, @genre, @directorId) RETURNING id";
                        cmd.Parameters.AddWithValue("title", newMovie.Title);
                        cmd.Parameters.AddWithValue("releaseDate", newMovie.DateOfRelease);
                        cmd.Parameters.AddWithValue("genre", newMovie.MovieGenre.ToString());
                        cmd.Parameters.AddWithValue("directorId", newMovie.DirectorId);

                        object? result = cmd.ExecuteScalar();

                        int newMovieId = (int)result;

                        return new Movie
                        {
                            Id = newMovieId,
                            DateOfRelease = newMovie.DateOfRelease,
                            DirectorId = newMovie.DirectorId,
                            MovieGenre = newMovie.MovieGenre,
                            Title = newMovie.Title
                        };
                    }
                    catch (InvalidOperationException)
                    {
                        throw new Exception("Invalid data.");
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        public override void DeleteMovie(int movieId)
        {
            using (NpgsqlConnection connection = new(connectionString))
            {
                using (NpgsqlCommand cmd = new())
                {
                    try
                    {
                        connection.Open();
                        cmd.Connection = connection;
                        cmd.CommandText = "DELETE FROM movies WHERE id = @movieId";
                        cmd.Parameters.AddWithValue("movieId", movieId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        public override void UpdateMovie(Movie movieToUpdate)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {

                using (NpgsqlCommand cmd = new())
                {
                    try
                    {
                        conn.Open();

                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE movies SET title = @title, date_of_release = @date_of_release, genre = @genre WHERE id = @id";
                        cmd.Parameters.AddWithValue("title", movieToUpdate.Title);
                        cmd.Parameters.AddWithValue("date_of_release", movieToUpdate.DateOfRelease);
                        cmd.Parameters.AddWithValue("genre", movieToUpdate.MovieGenre.ToString());
                        cmd.Parameters.AddWithValue("id", movieToUpdate.Id);

                        cmd.ExecuteNonQuery();
                    }
                    catch (InvalidOperationException)
                    {
                        throw new Exception("Invalid data.");
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
        }
        public override void DeleteDirector(int directorId)
        {
            using (NpgsqlConnection connection = new(connectionString))
            {
                using (NpgsqlCommand command = new())
                {
                    try
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "DELETE FROM directors WHERE id = @directorId";
                        command.Parameters.AddWithValue("directorId", directorId);
                        command.ExecuteNonQuery();
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
        }
        public override void UpdateDirector(Director directorToUpdate)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                using (NpgsqlCommand cmd = new())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE directors SET name = @name, year_of_birth = @yearOfBirth, nationality = @nationality WHERE id = @id";
                        cmd.Parameters.AddWithValue("name", directorToUpdate.Name);
                        cmd.Parameters.AddWithValue("yearOfBirth", directorToUpdate.YearOfBirth);
                        cmd.Parameters.AddWithValue("nationality", directorToUpdate.Nationality);
                        cmd.Parameters.AddWithValue("id", directorToUpdate.Id);

                        cmd.ExecuteNonQuery();
                    }
                    catch (InvalidOperationException)
                    {
                        throw new Exception("Invalid data.");
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        public override List<Movie> GetMoviesByDirector(int directorId)
        {
            var movies = new List<Movie>();

            using (NpgsqlConnection connection = new(connectionString))
            {
                using (NpgsqlCommand command = new())
                {
                    try
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "SELECT id, title, date_of_release, genre, director_id FROM movies WHERE director_id = @directorId";
                        command.Parameters.AddWithValue("directorId", directorId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _ = Enum.TryParse<Genre>(reader.GetString(3), out Genre movieGenre);
                                Movie movie = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    DateOfRelease = reader.GetDateTime(2),
                                    MovieGenre = movieGenre,
                                    DirectorId = reader.GetInt32(4)
                                };
                                movies.Add(movie);
                            }
                        }
                        return movies;
                    }
                    catch (NpgsqlException)
                    {
                        throw new Exception("An error occurred while accessing the database.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }

        }
    }
}
