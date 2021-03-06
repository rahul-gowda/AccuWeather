using AccuWeather.Controller;
using AccuWeather.Utilities;
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
            //driver.SkipAlertIfAny();

            try
            {
                driver.SwitchTo().ParentFrame();
                driver.SwitchTo().Frame(driver.FindElement(By.Id("google_ads_iframe_/6581/web/in/interstitial/admin/search_0")));
                driver.FindElement(By.Id("dismiss-button")).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("No ads frame to close");
            }
            return new HomePage(driver);
        }

        public HomePage CompareTemperature() 
        {
            double UItemp = GetCurrentTempFromUI();
            string currentLoc = CityHeader.Text.Replace(" ", "");
            string ApiTemp = GetCurrentTempFromAPI(currentLoc);
            return new HomePage(driver);
        }

        private string GetCurrentTempFromAPI(string currentLoc)
        {
            ControllerActions controllerActions = new ControllerActions();
            var t = controllerActions.GetWeatherBasedOnCity(currentLoc);
            return t.Content;
        }

        public string GetCurrentLocFromUI()
        {           
            return CityHeader.Text.Replace(" ","").Trim();
        }
        public double GetCurrentTempFromUI()
        {
            var t = CityTemp.Text;
            Double.TryParse(CityTemp.Text.Split("°")[0], out double temp);
            return temp;
        }
    }
}
