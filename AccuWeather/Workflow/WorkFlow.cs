using AccuWeather.Controller;
using AccuWeather.Helper;
using AccuWeather.Models;
using AccuWeather.Pages;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;

namespace AccuWeather.Workflow
{
    public class WorkFlow
    {
        public WorkFlow(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebDriver driver { get; }

        public void CompareTemperature(string desiredCity)
        {
            HomePage hp = new HomePage(driver);
            string currentLoc = hp.NavigateToHomePage().SearchDesiredCity(desiredCity).GetCurrentLocFromUI();
            double tempFromUi = hp.GetCurrentTempFromUI();
            ControllerActions controllerActions = new ControllerActions();
            var t = controllerActions.GetWeatherBasedOnCity(currentLoc);
            WeatherResponseModel weatherResponse = JsonConvert.DeserializeObject<WeatherResponseModel>(controllerActions.GetWeatherBasedOnCity(currentLoc).Content);
            double tempFromApi = weatherResponse.Main.Temp;
            CompareDifferenceInTemp(tempFromUi, tempFromApi);
        }
        public bool CompareDifferenceInTemp(double tempFromUi, double tempFromApi, int variance=1)
        {
            //try
            //{
            var t = Math.Abs(tempFromUi - tempFromApi);
            if (t > variance)
            {
                throw new TemperatureDifferenceException(Math.Abs(tempFromUi - tempFromApi).ToString());
            }
            //}
            //catch (TemperatureDifferenceException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }
    }
}
