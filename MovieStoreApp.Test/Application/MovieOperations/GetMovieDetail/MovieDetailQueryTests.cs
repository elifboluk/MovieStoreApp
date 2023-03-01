using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.MovieOperations.GetMovieDetail;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "MovieDetailQueryTests",
                Surname = "MovieDetailQueryTests"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new MovieStorepApp.API.Entities.Genre
            {
                Name = "MovieDetailQueryTests"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new MovieStorepApp.API.Entities.Movie
            {
                Name = "MovieDetailQueryTests",
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

            MovieDetailQuery query = new MovieDetailQuery(_context, _mapper);
            query.MovieId = movie.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film mevcut değil");
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_Actor_ShouldNotBeReturnErrors()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "ForHappyCodeMovieDetailQueryTests",
                Surname = "ForHappyCodeMovieDetailQueryTests"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
            var genre = new MovieStorepApp.API.Entities.Genre
            {
                Name = "ForHappyCodeMovieDetailQueryTests"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var movie = new MovieStorepApp.API.Entities.Movie
            {
                Name = "ForHappyCodeMovieDetailQueryTests",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            var movieId = movie.Id;
            MovieDetailQuery query = new MovieDetailQuery(_context, _mapper);
            query.MovieId = movie.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
