using FluentValidation;

namespace MovieStorepApp.API.Application.ActorOperations.CreateActor
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(c => c.Model.Name).NotEmpty();
            RuleFor(c => c.Model.Surname).NotEmpty();
        }
    }
}
