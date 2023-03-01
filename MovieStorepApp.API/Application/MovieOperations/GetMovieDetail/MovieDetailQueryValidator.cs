using FluentValidation;

namespace MovieStorepApp.API.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQueryValidator : AbstractValidator<MovieDetailQuery>
    {
        public MovieDetailQueryValidator()
        {
            RuleFor(c => c.MovieId).GreaterThan(0);
        }
    }
}
