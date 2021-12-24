using AccuWeather.Workflow;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace AccuWeather
{
    [TestFixture]
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


        [TestCase("Bengaluru", TestName = "Compare temperature for Bengaluru")]
        [TestCase("Mysore", TestName = "Compare temperature for Mysore")]
        [TestCase("London", TestName = "Compare temperature for London")]
        public void Test1(string city)
        {
            WorkFlow workflow = new WorkFlow(driver);
            workflow.CompareTemperature(city);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}