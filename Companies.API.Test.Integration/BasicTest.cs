using System;
using System.Net.Http.Headers;
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
        public async Task Get_One_Works(string url)
        {
            // Arrange
            var ciient = _factory.CreateClient();

            // CAUTION: Authentication token must be reneved every day!!!
            var _jwToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InNlZWRBZG1pbkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzZWVkQWRtaW5AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJuYmYiOjE1OTQxMjg4MzMsImV4cCI6MTU5NDIxNTIzMywiaXNzIjoibG9jYWxob3N0LmNvbSIsImF1ZCI6ImxvY2FsaG9zdC5jb20ifQ.NYQSXjC2at6R0AIr4uC8O6CMxCry7zZbeHB1o5a3grg";
            ciient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "bearer", parameter: _jwToken);

            // Act
            var response = await ciient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        //[InlineData("/")]
        [InlineData("/api/Company/1")]
        public async Task Unathorized_Get_One_Fails(string url)
        {
            // Arrange
            var ciient = _factory.CreateClient();

            // Act
            var response = await ciient.GetAsync(url);

            // Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Theory]
        [InlineData("/")]
        //[InlineData("/api/Company/1")]
        public async Task API_UI_Works(string url)
        {
            // Arrange
            var ciient = _factory.CreateClient();

            // Act
            var response = await ciient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            //[InlineData("/")]
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());

        }

    }
}
