using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoScreenTaskAutomation.Configure;
using TodoScreenTaskAutomation.Services;

namespace TodoScreenTaskAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<TestFixture>();
                    services.AddSingleton<CadastrarTodos>();
                    services.AddSingleton<ExcluirTodos>();
                    services.AddHostedService<ServiceHost>();
                });

            host.RunConsoleAsync();
        }
    }
}
