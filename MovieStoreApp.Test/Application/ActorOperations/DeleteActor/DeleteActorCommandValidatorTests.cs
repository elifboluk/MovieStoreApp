using FluentAssertions;
using MovieStoreApp.Test.TestSetup;
using MovieStorepApp.API.Application.ActorOperations.DeleteActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 0;

            //Axt
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 1;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
