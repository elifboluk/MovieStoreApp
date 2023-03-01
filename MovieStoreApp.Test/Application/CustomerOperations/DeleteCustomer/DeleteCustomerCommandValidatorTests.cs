using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.CustomerOperations.DeleteCustomer;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            var customer = new MovieStorepApp.API.Entities.Customer
            {
                Name = "WhenActorIdLessThanZero_Validator_ShouldBeReturnError",
                Surname = "WhenActorIdLessThanZero_Validator_ShouldBeReturnError"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = 0;

            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {

            var customer = new MovieStorepApp.API.Entities.Customer
            {
                Name = "ForHappyCodeCustomerDeleteValidator",
                Surname = "ForHappyCodeCustomerDeleteValidator"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;

            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
