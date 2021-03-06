using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TodoApp.Models;
using TodoStaticApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient httpClient;

        public TodoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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

                var response = JsonConvert.DeserializeObject<List<Todo>>(content);

                return (httpResponse.StatusCode, response);
            }
        }

        public async Task<(HttpStatusCode, GenericResponse)> Add(Todo todo)
        {
            string queryString = "TodoItem";

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");

            using (HttpResponseMessage httpResponse = await httpClient.PostAsync(queryString, requestContent))
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                }

                var response = JsonConvert.DeserializeObject<GenericResponse>(responseContent);

                return (httpResponse.StatusCode, response);
            }
        }

        public async Task<(HttpStatusCode, GenericResponse)> Delete(int id)
        {
            string queryString = $"TodoItem/{id}";            

            using (HttpResponseMessage httpResponse = await httpClient.DeleteAsync(queryString))
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                }

                var response = JsonConvert.DeserializeObject<GenericResponse>(responseContent);

                return (httpResponse.StatusCode, response);
            }
        }
    }
}