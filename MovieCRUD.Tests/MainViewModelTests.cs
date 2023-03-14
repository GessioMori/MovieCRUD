using Moq;
using MovieCRUD.Models.DbContext;
using MovieCRUD.Models;
using MovieCRUD.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCRUD.Tests
{
    [TestFixture]
    internal class MainViewModelTests
    {

        [Test]
        public void AddDirectorCommand_WhenCalled_AddsDirectorToCollection()
        {
            // Arrange
            Mock<IDbContext> dbContextMock = new();
            MainViewModel viewModel = new(dbContextMock.Object);
            Director newDirector = new()
            {
                Name = "John Doe",
                YearOfBirth = 1980,
                Nationality = "USA"
            };

            // Act
            viewModel.AddDirector.Execute(newDirector);

            // Assert
            dbContextMock.Verify(x => x.AddDirector(newDirector), Times.Once);
            Assert.That(viewModel.Directors, Contains.Item(newDirector));
            Assert.That(viewModel.SelectedDirector, Is.EqualTo(newDirector));
        }
    }
}
