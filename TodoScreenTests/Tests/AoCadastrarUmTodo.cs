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

            // assert
            Assert.Contains("Informe o título", driver.PageSource);
        }      

        [Fact]
        public void DadoInformacoesValidasDeveRedirecionarParaListaDeTodos()
        {
            driver.Navigate().GoToUrl("http://0.0.0.0:6002/submodule/todo/add");

            var inputTitle = driver.FindElement(By.Id("titulo"));
            var botao = driver.FindElement(By.Id("confirmar"));

            inputTitle.SendKeys("Novo TodoItem");

            botao.Click();            

            // assert
            Assert.Contains("Novo TodoItem", driver.PageSource);
        }

        [Fact]
        public void DadoInformacoesValidasDeveRedirecionarParaListaDeTodos2()
        {
            driver.Navigate().GoToUrl("http://0.0.0.0:6002/submodule/todo/add");

            // assert
            Assert.Equal("http://0.0.0.0:6002/submodule/todo/add", driver.Url);
        }
    }
}
