using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Weather
{
    public struct WeatherObservation
    {
        public DateTime TimeStamp;
        public float Barometric_Pressure;

        public static WeatherObservation Parse(string text)
        {
            var data = text.Split('\t');
            
            Debug.Assert(data.Length == 8);

            // changee format to en-US : for the dateTime and the float delimiter
            IFormatProvider culture = new CultureInfo("en-US", false);

            var timestamp = DateTime.Parse(data[(int)WeatherObservationMetrics.Date_Time].Replace("_", "-"),culture);
            var pressure = float.Parse(data[(int)WeatherObservationMetrics.Barometric_Pressure],culture);

            return new WeatherObservation()
            {
                TimeStamp = timestamp,
                Barometric_Pressure = pressure
            };
        }

        public static bool TryParse(string text, out WeatherObservation wo)
        {
            // changee format to en-US : for the dateTime and the float delimiter

            wo = new WeatherObservation()
            {
                TimeStamp = DateTime.MinValue,
                Barometric_Pressure = float.NaN

            }; 

            // changee format to en-US : for the dateTime and the float delimiter
            IFormatProvider culture = new CultureInfo("en-US");

            var data = text.Split('\t');

            if (data.Length != 8)
                return false;

            if (!DateTime.TryParse(data[(int)WeatherObservationMetrics.Date_Time].Replace("_", "-") , culture,DateTimeStyles.AssumeLocal , out DateTime timestamp))
                return false;

            if (!float.TryParse(data[(int)WeatherObservationMetrics.Barometric_Pressure], NumberStyles.Float ,culture, out float pressure))
                return false;

            wo = new WeatherObservation()
            {
                TimeStamp = timestamp,
                Barometric_Pressure = pressure
            };
            return true;


        }
    }
}
