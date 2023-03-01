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
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            var directorId = director.Id;

            _context.Directors.Remove(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = director.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            var director = new MovieStorepApp.API.Entities.Director
            {
                Name = "updateTest",
                Surname = "updateTest"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);

            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyTest"
            };
            command.DirectorId = director.Id;
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            director = _context.Directors.FirstOrDefault(c => c.Id == command.DirectorId);
            director.Name.Should().Be(model.Name);
            director.Surname.Should().Be(model.Surname);

        }
    }
}
