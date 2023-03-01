using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.MovieOperations.GetMovieDetail;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenMovieIdLessThanZero_Validator_ShouldBeReturnError()
        {
            MovieDetailQuery query = new MovieDetailQuery(_context, _mapper);
            query.MovieId = 0;

            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
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
            query.MovieId = movieId;

            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}
