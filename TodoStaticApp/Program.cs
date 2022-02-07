using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Services;

namespace TodoApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddAntDesign();
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(@"http://localhost:5000/api/")});

            await builder.Build().RunAsync();
        }
    }
}
