using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TodoScreenTaskAutomation.Configure
{
    // Classe helper para abstrair a instancia e fechamento do browser
    public class TestFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public TestFixture()
        {
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}