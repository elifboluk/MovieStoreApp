using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.ActorOperations.GetActorDetail;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.ActorOperations.GetActorDetail
{
    public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        //Arrange
        public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var actor = new Actor()
            {
                Name = "ForNoExistActor",
                Surname = "ForNoExistActor"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            var actorId = actor.Id;
            _context.Actors.Remove(actor);
            _context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = actorId;

            //Act
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör mevcut değil");
        }
        [Fact]
        public void WhenTheActorIsNotAvailable_Actor_ShouldNotBeReturnErrors()
        {
            var actor = new Actor
            {
                Name = "ForHappyCode",
                Surname = "ForHapyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = actor.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}
