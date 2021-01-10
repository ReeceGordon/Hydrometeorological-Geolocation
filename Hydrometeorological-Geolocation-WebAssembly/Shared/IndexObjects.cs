using System;
using System.Collections.Generic;
using System.Text;

namespace Hydrometeorological_Geolocation_Prototype_V2.Shared
{
    public class IndexObjects
    {
        public int Count { get; set; }

        //Weather data
        public DateTime Date { get; set; }
        public String Location { get; set; }
        public String ObservedTime { get; set; }
        public String TempC { get; set; }
        public String WeatherDescription { get; set; }
        public String WindSpeedKMPH { get; set; }
        public String WindDirection { get; set; }
        public String Humidity { get; set; }
        public String RealFeel { get; set; }

        //3 Hour Intervals Temperatures
        public List<String> Hours { get; set; }

        //Lat & Long reverse geocode data
        public String Lat { get; set; }
        public String Lon { get; set; }

        public String NearestCity { get; set; }

        //Marine data
        public List<TideData> TideDataSet { get; set; }
        public List<SwellData> SwellDataSet { get; set; }

    }
}
