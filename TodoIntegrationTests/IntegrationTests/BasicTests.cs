using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TodoApi;
using TodoApi.Models;
using TodoIntegrationTests.Factories;
using Xunit;


namespace TodoIntegrationTests.ApiTests
{
    public class BasicTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BasicTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;         
        }


        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Post_Endpoints_Return_Success(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var todoItem = new TodoItem() { Id = 100, Title = "Todo item", Done = false };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Post_Endpoints_Return_BadRequest(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var todoItem = new TodoItem() { Title = "", Done = false };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Get_Endpoints_Return_Success(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var todoItems = JsonConvert.DeserializeObject<IList<TodoItem>>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, todoItems.Count);
        }

        [Theory]
        [InlineData("/api/todoItem/100")]
        public async Task Get_Endpoints_With_Parameter_Return_Success(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/100")]
        public async Task Put_Endpoints_Return_Success(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var todoItem = new TodoItem() { Id = 100, Title = "Todo item new", Done = true };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/9999")]
        public async Task Put_Endpoints_Return_BadRequest(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var todoItem = new TodoItem() { Id = 1, Title = "Todo item new", Done = true };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/9999")]
        public async Task Put_Endpoints_Return_NotFound(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var todoItem = new TodoItem() { Id = 9999, Title = "Todo item new", Done = true };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/100")]
        public async Task Delete_Endpoints_Return_Success(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/9999")]
        public async Task Delete_Endpoints_Return_NotFound(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}