using System;
using System.Collections.Generic;
using System.Text;

namespace Hydrometeorological_Geolocation_Prototype_V2.Shared
{
    

    public class APIObjects
    {
        public DateTime Date { get; set; }
        public String Location { get; set; }
        public String ObservedTime { get; set; }
        public String TempC { get; set; }
        public String WeatherDescription { get; set; }
    }

    public class TideForecasts
    {
        public String Date { get; set; }
        public String EventType { get; set; }
        public String Height { get; set; }
    }

    

    public class CoastCity
    {
        public String Name { get; set; }
        public String Id { get; set; }
    }

    public class TideData
    {
        public String TideTime { get; set; }
        public String TideHeight { get; set; }
        public String TideType { get; set; }
    }

    public class SwellData
    {
        public String SigWaveHeight { get; set; }
        public String SwellHeightM { get; set; }
        public String SwellPeriodS { get; set; }
    }

}
