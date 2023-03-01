using FluentValidation;

namespace MovieStorepApp.API.Application.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(c => c.DirectorId).GreaterThan(0);
        }
    }
}
