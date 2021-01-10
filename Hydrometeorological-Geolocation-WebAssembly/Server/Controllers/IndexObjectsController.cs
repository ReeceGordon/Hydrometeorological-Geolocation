using Hydrometeorological_Geolocation_Prototype_V2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Hydrometeorological_Geolocation_Prototype_V2.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexObjectsController : ControllerBase
    {

        private readonly ILogger<IndexObjectsController> logger;

        public IndexObjectsController(ILogger<IndexObjectsController> logger)
        {
            this.logger = logger;
        }

        //string conString = "Server=tcp:rg-weather-forecast.database.windows.net,1433;Initial Catalog=WeatherForecastDB;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        string conString = "Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        /// <summary>
        /// Posts all the respective data from object reference into respective sql tables. Also checks the count size of swell data as 
        /// I found that sometimes the API would return 7 data objects instead of 8
        /// </summary>
        /// <param name="IObjects">object reference with data value attributes present</param>
        [HttpPost]
        [Route("Post")]
        public void Post(IndexObjects IObjects)
        {
            using (var conn = new SqlConnection("Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[IOWeather] ([Date],[Location],[ObservedTime],[TempC],[WeatherDescription],[WindSpeedKMPH],[WindDirection],[Humidity],[RealFeel],[Lat],[Lon],[NearestCity]) VALUES('" + IObjects.Date +"','"+ IObjects.Location + "','" + IObjects.ObservedTime + "','" + IObjects.TempC + "','" + IObjects.WeatherDescription + "','" + IObjects.WindSpeedKMPH + "','" + IObjects.WindDirection + "','" + IObjects.Humidity + "','" + IObjects.RealFeel + "','" + IObjects.Lat + "','" + IObjects.Lon + "','" + IObjects.NearestCity + "')";
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    command.CommandText = "INSERT INTO [dbo].[IOHours] ([IOWeather_Id],[Hour1],[Hour2],[Hour3],[Hour4],[Hour5],[Hour6],[Hour7],[Hour8]) VALUES(" + "(SELECT Id FROM dbo.IOWeather WHERE Date='" + IObjects.Date + "' AND Location='" + IObjects.Location + "' AND ObservedTime='" + IObjects.ObservedTime + "'),'" + IObjects.Hours[0] + "','" + IObjects.Hours[1] + "','" + IObjects.Hours[2] + "','" + IObjects.Hours[3] + "','" + IObjects.Hours[4] + "','" + IObjects.Hours[5] + "','" + IObjects.Hours[6] + "','" + IObjects.Hours[7] + "')";
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();

                    if (IObjects.Count == 7)
                    {
                        command.CommandText = "INSERT INTO [dbo].[IOSwells] ([IOWeather_Id],[SigWaveHeight],[SigWaveHeight1],[SigWaveHeight2],[SigWaveHeight3],[SigWaveHeight4],[SigWaveHeight5],[SigWaveHeight6],[SigWaveHeight7],[SwellHeightM],[SwellHeightM1],[SwellHeightM2],[SwellHeightM3],[SwellHeightM4],[SwellHeightM5],[SwellHeightM6],[SwellHeightM7],[SwellPeriodsS],[SwellPeriodsS1],[SwellPeriodsS2],[SwellPeriodsS3],[SwellPeriodsS4],[SwellPeriodsS5],[SwellPeriodsS6],[SwellPeriodsS7]) VALUES(" + "(SELECT Id FROM dbo.IOWeather WHERE Date='" + IObjects.Date + "' AND Location='" + IObjects.Location + "' AND ObservedTime='" + IObjects.ObservedTime + "'),'" + IObjects.SwellDataSet[0].SigWaveHeight + "','" + IObjects.SwellDataSet[1].SigWaveHeight + "','" + IObjects.SwellDataSet[2].SigWaveHeight + "','" + IObjects.SwellDataSet[3].SigWaveHeight + "','" + IObjects.SwellDataSet[4].SigWaveHeight + "','" + IObjects.SwellDataSet[5].SigWaveHeight + "','" + IObjects.SwellDataSet[6].SigWaveHeight + "','" + "NA" + "','" + IObjects.SwellDataSet[0].SwellHeightM + "','" + IObjects.SwellDataSet[1].SwellHeightM + "','" + IObjects.SwellDataSet[2].SwellHeightM + "','" + IObjects.SwellDataSet[3].SwellHeightM + "','" + IObjects.SwellDataSet[4].SwellHeightM + "','" + IObjects.SwellDataSet[5].SwellHeightM + "','" + IObjects.SwellDataSet[6].SwellHeightM + "','" + "NA" + "','" + IObjects.SwellDataSet[0].SwellPeriodS + "','" + IObjects.SwellDataSet[1].SwellPeriodS + "','" + IObjects.SwellDataSet[2].SwellPeriodS + "','" + IObjects.SwellDataSet[3].SwellPeriodS + "','" + IObjects.SwellDataSet[4].SwellPeriodS + "','" + IObjects.SwellDataSet[5].SwellPeriodS + "','" + IObjects.SwellDataSet[6].SwellPeriodS + "','" + "NA" + "')";
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    if(IObjects.Count == 8)
                    {
                        command.CommandText = "INSERT INTO [dbo].[IOSwells] ([IOWeather_Id],[SigWaveHeight],[SigWaveHeight1],[SigWaveHeight2],[SigWaveHeight3],[SigWaveHeight4],[SigWaveHeight5],[SigWaveHeight6],[SigWaveHeight7],[SwellHeightM],[SwellHeightM1],[SwellHeightM2],[SwellHeightM3],[SwellHeightM4],[SwellHeightM5],[SwellHeightM6],[SwellHeightM7],[SwellPeriodsS],[SwellPeriodsS1],[SwellPeriodsS2],[SwellPeriodsS3],[SwellPeriodsS4],[SwellPeriodsS5],[SwellPeriodsS6],[SwellPeriodsS7]) VALUES(" + "(SELECT Id FROM dbo.IOWeather WHERE Date='" + IObjects.Date + "' AND Location='" + IObjects.Location + "' AND ObservedTime='" + IObjects.ObservedTime + "'),'" + IObjects.SwellDataSet[0].SigWaveHeight + "','" + IObjects.SwellDataSet[1].SigWaveHeight + "','" + IObjects.SwellDataSet[2].SigWaveHeight + "','" + IObjects.SwellDataSet[3].SigWaveHeight + "','" + IObjects.SwellDataSet[4].SigWaveHeight + "','" + IObjects.SwellDataSet[5].SigWaveHeight + "','" + IObjects.SwellDataSet[6].SigWaveHeight + "','" + IObjects.SwellDataSet[7].SigWaveHeight + "','" + IObjects.SwellDataSet[0].SwellHeightM + "','" + IObjects.SwellDataSet[1].SwellHeightM + "','" + IObjects.SwellDataSet[2].SwellHeightM + "','" + IObjects.SwellDataSet[3].SwellHeightM + "','" + IObjects.SwellDataSet[4].SwellHeightM + "','" + IObjects.SwellDataSet[5].SwellHeightM + "','" + IObjects.SwellDataSet[6].SwellHeightM + "','" + IObjects.SwellDataSet[7].SwellHeightM + "','" + IObjects.SwellDataSet[0].SwellPeriodS + "','" + IObjects.SwellDataSet[1].SwellPeriodS + "','" + IObjects.SwellDataSet[2].SwellPeriodS + "','" + IObjects.SwellDataSet[3].SwellPeriodS + "','" + IObjects.SwellDataSet[4].SwellPeriodS + "','" + IObjects.SwellDataSet[5].SwellPeriodS + "','" + IObjects.SwellDataSet[6].SwellPeriodS + "','" + IObjects.SwellDataSet[7].SwellPeriodS + "')";
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }

                    command.CommandText = "INSERT INTO [dbo].[IOTides] ([IOWeather_Id],[TideTime],[TideTime1],[TideTime2],[TideTime3],[TideHeight],[TideHeight1],[TideHeight2],[TideHeight3],[TideType],[TideType1],[TideType2],[TideType3]) VALUES(" + "(SELECT Id FROM dbo.IOWeather WHERE Date='" + IObjects.Date + "' AND Location='" + IObjects.Location + "' AND ObservedTime='" + IObjects.ObservedTime + "'),'" + IObjects.TideDataSet[0].TideTime + "','" + IObjects.TideDataSet[1].TideTime + "','" + IObjects.TideDataSet[2].TideTime + "','" + IObjects.TideDataSet[3].TideTime + "','" + IObjects.TideDataSet[0].TideHeight + "','" + IObjects.TideDataSet[1].TideHeight + "','" + IObjects.TideDataSet[2].TideHeight + "','" + IObjects.TideDataSet[3].TideHeight + "','" + IObjects.TideDataSet[0].TideType + "','" + IObjects.TideDataSet[1].TideType + "','" + IObjects.TideDataSet[2].TideType + "','" + IObjects.TideDataSet[3].TideType + "')";
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Creates a new list, grabs all relevant data from a SQL Join statement accessing data stored in multiple tables and then storing it in list.
        /// </summary>
        /// <returns>list of object referneces with relevant data with </returns>
        [HttpGet]
        [Route("Get")]
        public IEnumerable<IndexObjects> Get()
        {
            List<IndexObjects> tIObjects = new List<IndexObjects>();
            using (var conn = new SqlConnection("Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [dbo].[IOWeather] JOIN [dbo].[IOHours] ON [dbo].[IOHours].IOWeather_Id = [dbo].[IOWeather].Id JOIN [dbo].[IOSwells] ON [dbo].[IOSwells].IOWeather_Id = [dbo].[IOWeather].Id JOIN [dbo].[IOTides] ON [dbo].[IOTides].IOWeather_Id = [dbo].[IOWeather].Id";
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tIObjects.Add(

                                new IndexObjects
                                {
                                    Date = reader.GetDateTime(1),
                                    Location = reader.GetString(2),
                                    ObservedTime = reader.GetString(3),
                                    TempC = reader.GetString(4),
                                    WeatherDescription = reader.GetString(5),
                                    WindSpeedKMPH = reader.GetString(6),
                                    WindDirection = reader.GetString(7),
                                    Humidity = reader.GetString(8),
                                    RealFeel = reader.GetString(9),
                                    Lat = reader.GetString(10),
                                    Lon = reader.GetString(11),
                                    NearestCity = reader.GetString(12),
                                    Hours = new List<String>
                                    {
                                       reader.GetString(15),
                                       reader.GetString(16),
                                       reader.GetString(17),
                                       reader.GetString(18),
                                       reader.GetString(19),
                                       reader.GetString(20),
                                       reader.GetString(21),
                                       reader.GetString(22),
                                    },
                                    SwellDataSet = new List<SwellData> 
                                    { 
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(25),
                                            SwellHeightM = reader.GetString(33),
                                            SwellPeriodS = reader.GetString(41)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(26),
                                            SwellHeightM = reader.GetString(34),
                                            SwellPeriodS = reader.GetString(42)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(27),
                                            SwellHeightM = reader.GetString(35),
                                            SwellPeriodS = reader.GetString(43)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(28),
                                            SwellHeightM = reader.GetString(36),
                                            SwellPeriodS = reader.GetString(44)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(29),
                                            SwellHeightM = reader.GetString(37),
                                            SwellPeriodS = reader.GetString(45)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(30),
                                            SwellHeightM = reader.GetString(38),
                                            SwellPeriodS = reader.GetString(46)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(31),
                                            SwellHeightM = reader.GetString(39),
                                            SwellPeriodS = reader.GetString(47)
                                        },
                                        new SwellData
                                        {
                                            SigWaveHeight = reader.GetString(32),
                                            SwellHeightM = reader.GetString(40),
                                            SwellPeriodS = reader.GetString(48)
                                        }
                                    },
                                    TideDataSet = new List<TideData> 
                                    {
                                        new TideData
                                        {
                                            TideTime = reader.GetString(51),
                                            TideHeight = reader.GetString(55),
                                            TideType = reader.GetString(59)
                                        },
                                        new TideData
                                        {
                                            TideTime = reader.GetString(52),
                                            TideHeight = reader.GetString(56),
                                            TideType = reader.GetString(60)
                                        },
                                        new TideData
                                        {
                                            TideTime = reader.GetString(53),
                                            TideHeight = reader.GetString(57),
                                            TideType = reader.GetString(61)
                                        },
                                        new TideData
                                        {
                                            TideTime = reader.GetString(54),
                                            TideHeight = reader.GetString(58),
                                            TideType = reader.GetString(62)
                                        }
                                    }

                                }
                            );
                        }

                    }
                    conn.Close();
                    return tIObjects;
                }
            }

        }
    }
}