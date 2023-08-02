using idee5.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Mime;
using System.Text;

// https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
namespace idee5.AspNetCore.Tests {
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>> {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task CanDetectDecorators() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/command/donothingdecorator").ConfigureAwait(false);

            // Assert
            Assert.False(response.IsSuccessStatusCode);
        }
        [Theory]
        [InlineData("/api/query/Hello?Name=idee5")]
        [InlineData("/api/query/hello?Name=idee5")]
        [InlineData("/api/query/hello?name=idee5")]
        public async Task CanFindQueryController(string url) {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
        [Theory]
        [InlineData("/api/command/donothing")]
        [InlineData("/api/command/DoNothing")]
        public async Task CanFindCommandControllers(string url) {
            // Arrange
            HttpClient client = _factory.CreateClient();
            // Act
            HttpResponseMessage response = await client.PostAsync(url, new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json)).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task CanExecuteQuery() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/query/Hello?Name=idee5").ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal("Hello idee5", result);
        }

        [Fact]
        public async void CanGetAnonymousUser() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/query/User?name=xyc").ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal("anonymous", result);
        }

        [Fact]
        public async void CanGetRoutes() {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("/debug/routes").ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string routes = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.NotEqual(0, routes.Length);
            Assert.Equal(5, routes.CountLines());
        }
    }
}