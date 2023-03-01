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
    public class CreateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateOrderCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        public static readonly object[][] CorrectData =
        {
        new object[] {(0,1,200, new DateTime(2000, 4, 20))},
        new object[] {(1, 0, 200, new DateTime(2000, 5, 20)) },
        new object[] {(1, 1, 0, new DateTime(2000, 6, 20)) },
        new object[] {(1, 1, 200,"")}
        };
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(int customerId, int movieId, decimal price, DateTime purchasedDate)
        {
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderModel
            {
                CustomerId = customerId,
                MovieId = movieId,
                Price = price,
                PurchasedDate = purchasedDate
            };
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
