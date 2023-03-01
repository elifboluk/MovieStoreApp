using FluentValidation;

namespace MovieStorepApp.API.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(c => c.Model.Name).NotEmpty();
            RuleFor(c => c.Model.Surname).NotEmpty();
        }
    }
}
