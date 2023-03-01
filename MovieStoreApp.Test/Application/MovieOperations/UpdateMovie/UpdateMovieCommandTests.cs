using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.MovieOperations.UpdateMovie;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.MovieOperations.UpdateMovie
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie",
                Surname = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new MovieStorepApp.API.Entities.Genre
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new MovieStorepApp.API.Entities.Movie
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            var movieId = movie.Id;

            _context.Remove(movie);
            _context.SaveChanges();

            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = movieId;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz film mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "ForHappyCodeUpdateMovie",
                Surname = "ForHappyCodeUpdateMovie"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var actor = new Actor
            {
                Name = "ForHappyCodeUpdateMovie",
                Surname = "ForHappyCodeUpdateMovie"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();
            var genre = new MovieStorepApp.API.Entities.Genre
            {
                Name = "ForHappyCodeUpdateMovie"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new MovieStorepApp.API.Entities.Movie
            {
                Name = "ForHappyCodeUpdateMovie",
                Year = 2001,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            var movieId = movie.Id;
            UpdateMovieModel model = new UpdateMovieModel
            {
                Name = "UpdateTest",
                Year = 2009,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 308,
                Actors = new[] { actor.Id }
            };

            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = movieId;
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
            movie = _context.Movies.FirstOrDefault(c => c.Id == command.MovieId);
            movie.Name.Should().Be(model.Name);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.GenreId.Should().Be(model.GenreId);
            movie.Year.Should().Be(model.Year);
            movie.Price.Should().Be(model.Price);
        }
    }
}
