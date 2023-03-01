using FluentValidation;

namespace MovieStorepApp.API.Application.CustomerOperations.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.Model.Name).NotEmpty();
            RuleFor(c => c.Model.Surname).NotEmpty();
        }
    }
}
