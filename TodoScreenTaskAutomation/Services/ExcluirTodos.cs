using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using TodoScreenTaskAutomation.Configure;

namespace TodoScreenTaskAutomation.Services
{
    public class ExcluirTodos
    {
        private IWebDriver _driver;
        private IConfiguration _config;

        public ExcluirTodos(TestFixture fixture, IConfiguration config)
        {
            (_driver, _config) = (fixture.Driver, config);
        }

        public async Task Execute()
        {
            _driver.Navigate().GoToUrl("http://0.0.0.0:6002/submodule/todo");

            Thread.Sleep(300);

            var quantidadeExcluir = _config.GetSection("quantidadeExcluir").Get<int>();
            var quantidadeExcluida = 1;

            while (true && quantidadeExcluida <= quantidadeExcluir)
            {
                var items = _driver.FindElements(By.Id("btnExcluir"));

                Thread.Sleep(300);

                if (!items.Any()) break;

                var item = _driver.FindElement(By.Id("btnExcluir"));

                Thread.Sleep(300);

                item.Click();

                quantidadeExcluida += 1;

                Thread.Sleep(300);
            }

            await Task.CompletedTask;
        }
    }
}