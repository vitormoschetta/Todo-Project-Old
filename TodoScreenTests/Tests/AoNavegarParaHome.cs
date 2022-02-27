using OpenQA.Selenium;
using TodoScreenTests.Configure;
using Xunit;

namespace TodoScreenTests.Tests
{
    // essa annotation possibilita abrir o browser apenas uma vez para realizar todos os testes desta classe
    [Collection("Chrome Driver")]
    public class AoNavegarParaHome
    {
        private IWebDriver driver;

        public AoNavegarParaHome(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void TestTitle()
        {
            // arrange
            // act
            driver.Navigate().GoToUrl("http://0.0.0.0:6002/");

            // assert
            Assert.Contains("TodoApp", driver.Title);
        }

        [Fact]
        public void TestHome()
        {
            // arrange
            // act
            driver.Navigate().GoToUrl("http://0.0.0.0:6002/");            

            // assert
            Assert.Equal(true, driver.PageSource.Contains("Hello"));
        }
    }
}