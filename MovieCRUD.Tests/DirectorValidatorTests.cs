using MovieCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieCRUD.Tests
{
    [TestFixture]
    public class DirectorValidatorTests
    {
        private IDirectorValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new DirectorValidator();
        }

        [Test]
        public void Validate_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            Director director = new Director
            {
                Name = null,
                YearOfBirth = 1980,
                Nationality = "American"
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_NameIsLessThanThreeCharacters_ThrowsArgumentException()
        {
            // Arrange
            Director director = new Director
            {
                Name = "Jo",
                YearOfBirth = 1980,
                Nationality = "American"
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_YearOfBirthIsZero_ThrowsArgumentException()
        {
            // Arrange
            Director director = new Director
            {
                Name = "John Smith",
                YearOfBirth = 0,
                Nationality = "American"
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_YearOfBirthIsGreaterThanCurrentYear_ThrowsArgumentException()
        {
            // Arrange
            Director director = new Director
            {
                Name = "John Smith",
                YearOfBirth = DateTime.Now.Year + 1,
                Nationality = "American"
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_NationalityIsNull_ThrowsArgumentException()
        {
            // Arrange
            Director director = new Director
            {
                Name = "John Smith",
                YearOfBirth = 1980,
                Nationality = null
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_NationalityIsLessThanThreeCharacters_ThrowsArgumentException()
        {
            // Arrange
            Director director = new Director
            {
                Name = "John Smith",
                YearOfBirth = 1980,
                Nationality = "US"
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Validate_DirectorIsValid_DoesNotThrowException()
        {
            // Arrange
            Director director = new Director
            {
                Name = "John Smith",
                YearOfBirth = 1980,
                Nationality = "American"
            };

            // Act
            TestDelegate action = () => _validator.Validate(director);

            // Assert
            Assert.DoesNotThrow(action);
        }
    }

}
