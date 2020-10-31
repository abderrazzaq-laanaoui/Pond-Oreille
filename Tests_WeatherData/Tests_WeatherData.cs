using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Weather;
using System.Linq;
using System.Collections.Generic;

namespace Tests_WeatherData
{
    [TestClass]
    public class Tests_WeatherData
    {
        private static string filename = @".\pond_data\Environmental_Data_Deep_Moor_2012.txt";

        #region Sample Data
        private static string sampleData =
@"date       time    	Air_Temp	Barometric_Press	Dew_Point	Relative_Humidity	Wind_Dir	Wind_Gust	Wind_Speed
2012_01_01 00:02:14	34.30	30.50	26.90	74.20	346.40	11.00	 3.60
2012_01_01 00:08:29	34.10	30.50	26.50	73.60	349.00	12.00	 8.00
2012_01_01 00:14:45	33.90	30.60	26.80	75.00	217.80	12.00	 9.20
2012_01_01 00:21:00	33.80	30.60	27.30	76.60	280.80	17.00	14.00";
        #endregion
        
        [TestMethod]
        public void Test_010_ParseLine()
        {
            var text = "2012_01_01 00:02:14	34.30	30.50	26.90	74.20	346.40	11.00	 3.60";

            var wo = WeatherObservation.Parse(text);

            Check.That(wo.TimeStamp).IsEqualTo(new DateTime(2012, 01, 01, 00, 02, 14));
            Check.That(wo.Barometric_Pressure).IsCloseTo(30.5, 0.01);
        }

        [TestMethod]
        public void Test_020_TryParseLine()
        {
            var text = "2012_01_01 00:02:14	34.30	30.50	26.90	74.20	346.40	11.00	 3.60";

            var tryParseOutcome = WeatherObservation.TryParse(text, out WeatherObservation wo);

            Check.That(tryParseOutcome).IsTrue();

            Check.That(wo.TimeStamp).IsEqualTo(new DateTime(2012, 01, 01, 00, 02, 14));
            Check.That(wo.Barometric_Pressure).IsCloseTo(30.5, 0.01);
        }

        [TestMethod]
        public void Test_030_ParseSampleText()
        {
            using (var text = new StringReader(sampleData))
            {
                text.ReadLine(); // ignore 1st line of text, it contains headers
                var data = WeatherData.ReadAll(text);

                Check.That(data.Count()).IsEqualTo(4);
            }
        }

        [TestMethod]
        public void Test_040_ParseSampleFile()
        {
            using (var text = new StreamReader(filename))
            {
                text.ReadLine(); // ignore 1st line of text, it contains headers.

                var data = WeatherData.ReadAll(text);

                Check.That(data.Count()).IsEqualTo(70675);
            }
        }

        [TestMethod]
        public void Test_050_ParseFilteredSampleFile()
        {
            var start = DateTime.Parse("2012-12-31 12:51:48");
            var end = DateTime.Parse("2012-12-31 22:43:13");

            using (var text = new StreamReader(filename))
            {
                text.ReadLine(); // ignore 1st line of text, it contains headers.

                var data = WeatherData.ReadRange(text,start,end);

                Check.That(data.Count()).IsEqualTo(6);
            }
        }

        [TestMethod]
        public void Test_060_FitLineToFilteredSampleFile()
        {
            var start = DateTime.Parse("2012-01-02 00:00:00");
            var end = DateTime.Parse("2012-01-02 17:00:00");

            using (var text = new StreamReader(filename))
            {
                text.ReadLine(); // ignore 1st line of text, it contains headers.

                // Extract
                // Transform
                // Load

                // MathNet.Numerics.Fit.Line(...);

                throw new NotImplementedException();
            }
        }
    }
}