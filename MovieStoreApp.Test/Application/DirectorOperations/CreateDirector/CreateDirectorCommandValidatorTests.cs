using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.DirectorOperations.CreateDirector;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData("", "surname")]
        [InlineData("name", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string surname)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = name,
                Surname = surname
            };
            command.Model = model;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "test",
                Surname = "test"
            };
            command.Model = model;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
