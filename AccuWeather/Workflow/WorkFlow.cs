using AccuWeather.Controller;
using AccuWeather.Models;
using AccuWeather.Pages;
using AccuWeather.Utilities;
using Newtonsoft.Json;
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
            string currentLoc = hp.NavigateToHomePage().SearchDesiredCity("Bengaluru").GetCurrentLocFromUI();
            double currentTempUi = hp.GetCurrentTempFromUI();
            ControllerActions controllerActions = new ControllerActions();
            var t = controllerActions.GetWeatherBasedOnCity(currentLoc);
            WeatherResponseModel weatherResponse = JsonConvert.DeserializeObject<WeatherResponseModel>(controllerActions.GetWeatherBasedOnCity(currentLoc).Content);
            double tempFromApi = weatherResponse.Main.Temp;
        }
        public decimal CompareDifferenceInTemp(decimal tempFromUi, decimal tempFromApi)
        {
            return Math.Abs(tempFromUi - tempFromApi);
        }
    }
}
