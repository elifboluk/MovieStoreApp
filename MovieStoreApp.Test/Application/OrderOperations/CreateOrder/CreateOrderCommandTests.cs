using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.OrderOperations.CreateOrder;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.OrderOperations.CreateOrder
{
    public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn()
        {

            var customer = new MovieStorepApp.API.Entities.Customer
            {
                Name = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            var order = new MovieStorepApp.API.Entities.Order
            {
                CustomerId = customer.Id,
                MovieId = 1
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = new CreateOrderModel { MovieId = 1, CustomerId = customer.Id };
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }

    }
}
