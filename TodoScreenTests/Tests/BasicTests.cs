using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace TodoScreenTests.Tests
{
    public class BasicTests : IDisposable
    {
        protected IWebDriver driver;

        public BasicTests()
        {            
            driver = new ChromeDriver();
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

        public void Dispose()
        {
            // fecha o browser ao concluir o teste
            driver.Quit();
        }
    }
}
