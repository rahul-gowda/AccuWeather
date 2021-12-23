using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuWeather.Helper
{
    [Serializable]
    public class TemperatureDifferenceException : Exception
    {
        public TemperatureDifferenceException() { }

        public TemperatureDifferenceException(string tempDiff)
            : base(String.Format("Temperature difference is morethan the threshold difference: Actual difference is {0}", tempDiff))
        {

        }
    }
}
