using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient httpClient;

        public TodoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            var serverUrl = Startup.ServerUrl.EndsWith('/') ? Startup.ServerUrl : $"{Startup.ServerUrl}/";
            this.httpClient.BaseAddress = new Uri(Startup.ServerUrl);
        }

        public async Task<(HttpStatusCode, IEnumerable<Todo>)> GetAll()
        {
            string queryString = "TodoItem";

            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(queryString))
            {
                string content = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                }

                var response = JsonConvert.DeserializeObject<IEnumerable<Todo>>(content);

                return (httpResponse.StatusCode, response);
            }
        }

    }
}