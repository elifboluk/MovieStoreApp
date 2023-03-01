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
    public class GetActorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = 0;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var actor = new Actor()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_context, _mapper);
            query.ActorId = actor.Id;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);

        }
    }
}
