using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Services;
using TodoApp.Helpers;

namespace TodoApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");            

            var apiUrl = builder.Configuration["API_URL_CONNECTION"];

            builder.Services.AddAntDesign();
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<TodoNotification>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

            await builder.Build().RunAsync();
        }
    }
}
