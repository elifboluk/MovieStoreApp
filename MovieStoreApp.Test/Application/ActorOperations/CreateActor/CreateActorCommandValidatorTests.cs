using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.ActorOperations.CreateActor;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.ActorOperations.CreateActor
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("", "surname")]
        [InlineData("name", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string surname)
        {

            //Arrange
            CreateActorCommand command = new(null, null);
            command.Model = new CreateActorModel
            {
                Name = name,
                Surname = surname
            };
            //Act
            CreateActorCommandValidator validator = new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateActorCommand command = new(null, null);
            command.Model = new CreateActorModel
            {
                Name = "Elif",
                Surname = "Bölük"
            };
            CreateActorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
