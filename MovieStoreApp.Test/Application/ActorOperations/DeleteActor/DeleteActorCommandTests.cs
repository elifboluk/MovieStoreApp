using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.ActorOperations.DeleteActor;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var actor = new Actor()
            {
                Name = "ForNotExistActor",
                Surname = "ForNotExistActor"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            var actorId = actor.Id;
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actorId;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteActor_ShouldNotBeReturnError()
        {
            var actor = new Actor()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actor.Id;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
