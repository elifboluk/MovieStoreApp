using FluentValidation;

namespace MovieStorepApp.API.Application.DirectorOperations.UpdateDirectors
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(c => c.DirectorId).GreaterThan(0);
            RuleFor(c => c.Model.Name).NotEmpty();
            RuleFor(c => c.Model.Surname).NotEmpty();
        }
    }
}
