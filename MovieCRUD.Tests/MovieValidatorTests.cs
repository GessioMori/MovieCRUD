using MovieCRUD.Models;
using MovieCRUD.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Tests
{
    [TestFixture]
    internal class MovieValidatorTests
    {
        private IMovieValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MovieValidator();
        }

        [Test]
        public void Validate_ValidMovie_NoExceptionThrown()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "The Godfather",
                MovieGenre = Genre.Drama,
                DirectorId = 1,
                DateOfRelease = new DateTime(1972, 3, 24)
            };

            // Act
            TestDelegate action = () => _validator.Validate(movie);

            // Assert
            Assert.DoesNotThrow(action);
        }

        [Test]
        public void Validate_NullTitle_ThrowsArgumentException()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = null,
                MovieGenre = Genre.Drama,
                DirectorId = 1,
                DateOfRelease = new DateTime(1972, 3, 24)
            };

            // Act
            TestDelegate action = () => _validator.Validate(movie);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_InvalidGenre_ThrowsArgumentException()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "The Godfather",
                MovieGenre = (Genre)10, // invalid genre
                DirectorId = 1,
                DateOfRelease = new DateTime(1972, 3, 24)
            };

            // Act
            TestDelegate action = () => _validator.Validate(movie);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_InvalidDirectorId_ThrowsArgumentException()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "The Godfather",
                MovieGenre = Genre.Drama,
                DirectorId = -1, // invalid director ID
                DateOfRelease = new DateTime(1972, 3, 24)
            };

            // Act
            TestDelegate action = () => _validator.Validate(movie);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_InvalidReleaseDate_ThrowsArgumentException()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "The Godfather",
                MovieGenre = Genre.Drama,
                DirectorId = 1,
                DateOfRelease = new DateTime(1800, 1, 1) // invalid release date
            };

            // Act
            TestDelegate action = () => _validator.Validate(movie);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }
    }
}
