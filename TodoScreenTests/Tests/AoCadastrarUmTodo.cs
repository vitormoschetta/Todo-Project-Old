using System.Threading;
using OpenQA.Selenium;
using TodoScreenTests.Configure;
using Xunit;

namespace TodoScreenTests.Tests
{
    [Collection("Chrome Driver")]
    public class AoCadastrarUmTodo
    {
        private IWebDriver driver;

        public AoCadastrarUmTodo(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void DadoNaoPreencherTituloSolicitarPreenchimento()
        {

            // arrange
            driver.Navigate().GoToUrl("http://0.0.0.0:6002/submodule/todo/add");

            // act            
            var botao = driver.FindElement(By.Id("confirmar"));
            botao.Click();

            Thread.Sleep(300);

            // assert
            Assert.Contains("Informe o título", driver.PageSource);
        }
    }
}
