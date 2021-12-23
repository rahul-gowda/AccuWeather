using AccuWeather.Workflow;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace AccuWeather
{
    public class Tests
    {
        public IWebDriver driver { get; private set; }

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("disable-extensions");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", "false");
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            WorkFlow workflow = new WorkFlow(driver);
            workflow.CompareTemperature("Bengaluru");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}