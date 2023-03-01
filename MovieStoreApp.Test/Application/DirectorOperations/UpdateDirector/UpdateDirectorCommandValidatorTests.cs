using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.DirectorOperations.UpdateDirectors;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.DirectorOperations.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateDirectorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(1, "", "surname")]
        [InlineData(1, "name", "")]
        [InlineData(0, "name", "surname")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string surname)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(null);
            UpdateDirectorModel model = new UpdateDirectorModel
            {
                Name = name,
                Surname = surname
            };
            command.DirectorId = id;
            command.Model = model;

            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorModel model = new UpdateDirectorModel();
            command.Model = new UpdateDirectorModel
            {
                Name = "ForHappCodeTest",
                Surname = "ForHappCodeTest"
            };

            command.DirectorId = director.Id;
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
