using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TodoScreenTaskAutomation.Services
{
    public class ServiceHost : IHostedService
    {
        private CadastrarTodos _cadastrar;
        private ExcluirTodos _excluir;
        private IConfiguration _config;

        public ServiceHost(CadastrarTodos cadastrar, ExcluirTodos excluir, IConfiguration config)
        {
            (_cadastrar, _excluir, _config) = (cadastrar, excluir, config);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_config.GetSection("cadastrar").Get<bool>())
                await _cadastrar.Execute();

            if (_config.GetSection("excluir").Get<bool>())
                await _excluir.Execute();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}