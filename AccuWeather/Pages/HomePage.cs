﻿using AccuWeather.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuWeather.Pages
{
    class HomePage
    {
        public IWebDriver driver { get; }
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        #region elements
        public IWebElement SearchBox => driver.FindElement(By.XPath("//input[@name='query']"));
        public IWebElement CityHeader => driver.FindElement(By.XPath("//h1[@class='header-loc']"));
        public IWebElement CityTemp => driver.FindElement(By.XPath("//span[@class='header-temp']"));
        #endregion elements


        
        public HomePage NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("https://www.accuweather.com");
            driver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            return new HomePage(driver);
        }

        public HomePage SearchDesiredCity(string cityName)
        {
            SearchBox.Set(cityName);
            SearchBox.SendKeys(Keys.Enter);
            driver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            driver.FindElement(By.XPath(@$"//a[contains(.,'{cityName}')]")).Click();
            driver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            return new HomePage(driver);
        }

        public HomePage CompareTemperature() 
        {
            string UItemp = GetCurrentTempFromUI();
            string ApiTemp = GetCurrentTempFromAPI();
            return new HomePage(driver);
        }

        private string GetCurrentTempFromAPI()
        {
            throw new NotImplementedException();
        }

        private string GetCurrentTempFromUI()
        {
            Int32.TryParse(CityTemp.Text, out int temp);
            return temp.ToString();
        }
    }
}