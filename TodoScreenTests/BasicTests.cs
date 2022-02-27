using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace TodoScreenTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // arrange
            IWebDriver driver = new ChromeDriver();

            // act
            driver.Navigate().GoToUrl("http://0.0.0.0:6002/");

            // assert
            Assert.Contains("TodoApp", driver.Title);

            driver.Close();
        }
    }
}
