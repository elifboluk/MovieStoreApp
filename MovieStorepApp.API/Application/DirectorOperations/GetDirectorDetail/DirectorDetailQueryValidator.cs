using FluentValidation;

namespace MovieStorepApp.API.Application.DirectorOperations.GetDirectorDetail
{
    public class DirectorDetailQueryValidator : AbstractValidator<DirectorDetailQuery>
    {
        public DirectorDetailQueryValidator()
        {
            RuleFor(c => c.DirectorId).GreaterThan(0);
        }
    }
}
