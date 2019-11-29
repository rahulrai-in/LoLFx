using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using Shouldly;
using Xunit;

namespace AzFunction.LoL.Tests
{
    public class LoLTests
    {
        [Fact]
        public void LoLReturnsNumberOfLoLsRequested()
        {
            // arrange
            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Query["count"].Returns(info => new StringValues("2"));

            // act
            var result = LoL.GetLoLz(httpRequest, NullLogger.Instance);

            // assert
            result.ShouldBeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.ShouldBe("LoL.LoL.");
        }

        [Fact]
        public void LoLReturnsBadRequestIfCountIsNotSpecified()
        {
            // arrange
            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Query["count"].Returns(StringValues.Empty);

            // act
            var result = LoL.GetLoLz(httpRequest, NullLogger.Instance);

            // assert
            result.ShouldBeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult)?.Value.ShouldBe("Please pass a count on the query string");
        }
    }
}
