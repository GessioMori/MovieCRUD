using System;

namespace MovieCRUD.Models
{
    public interface IDirectorValidator
    {
        void Validate(Director director);
    }

    public class DirectorValidator : IDirectorValidator
    {
        public DirectorValidator()
        {
        }

        public void Validate(Director director)
        {
            if (string.IsNullOrWhiteSpace(director.Name) || director.Name.Length < 3)
                throw new ArgumentException("Name must be at least 3 characters long.");

            if (director.YearOfBirth <= 0 || director.YearOfBirth > DateTime.Now.Year)
                throw new ArgumentException("Year of birth must be a positive integer between 1 and the current year.");

            if (string.IsNullOrWhiteSpace(director.Nationality) || director.Nationality.Length < 3)
                throw new ArgumentException("Nationality must be at least 3 characters long.");
        }
    }
}
