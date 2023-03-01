using AutoMapper;
using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.ActorOperations.GetActors;
using MovieStorepApp.API.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.ActorOperations.GetActors
{
    public class GetActorQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Movie_ShouldNotBeReturnErrors()
        {
            GetActorQuery query = new GetActorQuery(_context, _mapper);
            FluentActions.Invoking(() => query.Handle().GetAwaiter().GetResult());
        }
    }
}
