using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Companies.API.Test.Integration
{
    public class BasicTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BasicTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        //[InlineData("/")]
        [InlineData("/api/Company/1")]
        public async Task GetHttpRequest(string url)
        {
            // arrange
            var ciient = _factory.CreateClient();

            // Act
            var response = await ciient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            //[InlineData("/")]
            //Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());

            Assert.Equal("text/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

    }
}
