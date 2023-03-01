using FluentValidation;

namespace MovieStorepApp.API.Application.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(c => c.MovieId).GreaterThan(0);
        }
    }
}
