using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuWeather.Controller
{
    public class ControllerActions
    {
        public string baseUrl = "api.openweathermap.org/data/2.5/weather";
        public string appID = "7fe67bf08c80ded756e598d6f8fedaea";
        #region Controller Action Methods

        public IRestResponse InvokeGetCall(string methodUrl)
        {
            var client = new RestClient(baseUrl + methodUrl);
            var request = new RestRequest(Method.GET);
            var response = client.ExecuteAsync(request).Result;
            Console.WriteLine("Invoke " + methodUrl + " response StatusCode: " + response.StatusCode);
            return response;
        }

        public IRestResponse GetWeatherBasedOnCity(string city, string units="metric")
        {
            string methodUrl = @$"?q={city}&appid={appID}&units={units}";
            return InvokeGetCall(methodUrl);
        }

        #endregion Controller Action Methods
    }
}
