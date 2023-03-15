﻿using System;
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
    internal class PgContextAddDirectorTests
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
        public void AddDirector_ValidDirector_ReturnsNewDirectorWithId()
        {
            // Arrange
            Director newDirector = new()
            {
                Name = "Test Director",
                YearOfBirth = 1980,
                Nationality = "Test Nationality"
            };

            // Act
            Director result = _pgContext.AddDirector(newDirector);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(0));
            Assert.That(result.Name, Is.EqualTo(newDirector.Name));
            Assert.That(result.YearOfBirth, Is.EqualTo(newDirector.YearOfBirth));
            Assert.That(result.Nationality, Is.EqualTo(newDirector.Nationality));
        }

        [Test]
        public void AddDirector_ThrowsException_WhenInvalidDirectorIsAdded()
        {
            // Arrange
            Director invalidDirector = new(){ Name = null, YearOfBirth = 1990, Nationality = "USA" };

            // Act + Assert
            Assert.Throws<Exception>(() => _pgContext.AddDirector(invalidDirector));
        }
    }
}
