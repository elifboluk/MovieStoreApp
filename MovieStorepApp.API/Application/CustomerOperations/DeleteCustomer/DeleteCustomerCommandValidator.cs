using FluentValidation;

namespace MovieStorepApp.API.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId).GreaterThan(0);
        }
    }
}
