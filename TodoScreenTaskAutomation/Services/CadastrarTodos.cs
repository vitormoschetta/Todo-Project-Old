using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using TodoScreenTaskAutomation.Configure;

namespace TodoScreenTaskAutomation.Services
{
    public class CadastrarTodos
    {
        private IWebDriver _driver;
        private IConfiguration _config;

        public CadastrarTodos(TestFixture fixture, IConfiguration config)
        {
            (_driver, _config) = (fixture.Driver, config);
        }

        public async Task Execute()
        {
            var quantidade = _config.GetSection("quantidadeCadastrar").Get<int>();

            for (int i = 0; i < quantidade; i++)
            {
                _driver.Navigate().GoToUrl("http://0.0.0.0:6002/submodule/todo/add");
                Thread.Sleep(500);
                _driver.FindElement(By.Id("titulo")).SendKeys($"todo {i}");
                _driver.FindElement(By.Id("confirmar")).Click();
            }

            await Task.CompletedTask;
        }
    }
}