using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.DirectorOperations.DeleteDirector;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var director = new MovieStorepApp.API.Entities.Director()
            {
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            var directorId = director.Id;

            _context.Directors.Remove(director);
            _context.SaveChanges();
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = directorId;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yönetmen mevcut değil");

        }
        [Fact]
        public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn2()
        {
            //Arrange
            var director = new MovieStorepApp.API.Entities.Director()
            {
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
            };
            _context.Directors.Add(director);
            var genre = new MovieStorepApp.API.Entities.Genre
            {
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var movie = new MovieStorepApp.API.Entities.Movie
            {
                GenreId = genre.Id,
                DirectorId = director.Id,
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Price = 100,
                Year = 1999
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();
            var directorId = director.Id;

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = directorId;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yönetmenin filmi mevcut");

        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteActor_ShouldNotBeReturnError()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = director.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
