using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Npgsql;
using System.Configuration;
using MovieCRUD.Models.DbContext;
using MovieCRUD.Models;

namespace MovieCRUD.Tests
{
    [TestFixture]
    internal class PgContextGetDirectorsTests
    {
        private readonly string CreateTablesString = "CREATE TABLE directors (id SERIAL PRIMARY KEY, name VARCHAR(255) NOT NULL, year_of_birth INTEGER NOT NULL, nationality VARCHAR(255) NOT NULL);CREATE TABLE movies (id SERIAL PRIMARY KEY, title VARCHAR(255) NOT NULL, date_of_release DATE NOT NULL, genre VARCHAR(20) NOT NULL CHECK (genre IN ('Action', 'Comedy', 'Drama', 'Fantasy', 'Horror', 'Mystery', 'Romance', 'Thriller')), director_id INTEGER NOT NULL REFERENCES directors(id) ON DELETE CASCADE);\r\n";
        private readonly string DropTablesString = "DROP TABLE movies;DROP TABLE directors;";
        private readonly string ConnectionString = "Server=localhost;Port=5432;Database=moviecrudtest;User Id=postgres;Password=postgrespw;Integrated Security=true;";
        private IDbContext _pgContext;

        [SetUp]
        public void SetUp()
        {
            _pgContext = new PgContext(isTestMode: true);

            using (NpgsqlConnection connection = new(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new())
                {
                    command.Connection = connection;
                    command.CommandText = CreateTablesString;
                    command.ExecuteNonQuery();
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (NpgsqlConnection connection = new(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new())
                {
                    command.Connection = connection;
                    command.CommandText = DropTablesString;
                    command.ExecuteNonQuery();
                }
            }
        }

        [Test]
        public void GetDirectors_ReturnsAllDirectors_WhenDirectorsExist()
        {
            // Arrange
            List<Director> expectedDirectors = new()
            {
            new Director { Name = "Director 1", YearOfBirth = 1970, Nationality = "USA" },
            new Director { Name = "Director 2", YearOfBirth = 1980, Nationality = "UK" }
            };

            _pgContext.AddDirector(expectedDirectors[0]);
            _pgContext.AddDirector(expectedDirectors[1]);

            // Act
            List<Director> actualDirectors = _pgContext.GetDirectors();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(CompareDirectors(actualDirectors[0], expectedDirectors[0]), Is.EqualTo(true));
                Assert.That(CompareDirectors(actualDirectors[1], expectedDirectors[1]), Is.EqualTo(true));
            });
        }

        private bool CompareDirectors(Director actualDirector, Director expectedDirector)
        {
            return
              actualDirector.Name == expectedDirector.Name &&
              actualDirector.YearOfBirth == expectedDirector.YearOfBirth &&
              actualDirector.Nationality == expectedDirector.Nationality;
        }








    }
}
