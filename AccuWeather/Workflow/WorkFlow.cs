using AccuWeather.Pages;
using AccuWeather.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuWeather.Workflow
{
    class WorkFlow
    {
        public WorkFlow(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebDriver driver { get; }

        public void CompareTemperature()
        {

            HomePage hp = new HomePage(driver);
        }
    }
}
