using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.CustomerOperations.DeleteCustomer;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        public void WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var customer = new MovieStorepApp.API.Entities.Customer
            {
                Name = "WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;

            _context.Remove(customer);
            _context.SaveChanges();

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
        }
        public void WhenValidInputsAreGiven_DeleteCustomer_ShouldNotBeReturnError()
        {
            var customer = new MovieStorepApp.API.Entities.Customer
            {
                Name = "ForHappyCodeCustomer",
                Surname = "ForHappyCodeCustomer"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
