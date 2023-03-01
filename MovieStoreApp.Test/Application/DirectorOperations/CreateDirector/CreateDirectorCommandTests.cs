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
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var director = new MovieStorepApp.API.Entities.Director()
            {
                Name = "WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            command.Model = new CreateDirectorModel() { Name = director.Name, Surname = director.Surname };

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz yönetmen mevcut");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
        {
            //Arrange
            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "WhenValidInputsAreGiven_Director_ShouldBeCreated",
                Surname = "WhenValidInputsAreGiven_Director_ShouldBeCreated"
            };
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            //Assert
            var director = _context.Directors.SingleOrDefault(c => c.Name == model.Name && c.Surname == model.Surname);
            director.Should().NotBeNull();
        }
    }
}
