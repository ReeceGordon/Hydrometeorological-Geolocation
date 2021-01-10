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
    [ApiController]
    [Route("[controller]")]
    public class FavouriteObjectsController : ControllerBase
    {
        private readonly ILogger<FavouriteObjectsController> logger;

        public FavouriteObjectsController(ILogger<FavouriteObjectsController> logger)
        {
            this.logger = logger;
        }

        //string conString = "Server=tcp:rg-weather-forecast.database.windows.net,1433;Initial Catalog=WeatherForecastDB;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        string conString = "Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        /// <summary>
        /// Inserts favourite data from the object reference into the favourites table.
        /// </summary>
        /// <param name="IObjects"> Object reference to with Data to post</param>
        [HttpPost]
        [Route("Post")]
        public void Post(FavouriteObjects IObjects)
        {
            using (var conn = new SqlConnection("Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[Favourites] ([Location]) VALUES('" + IObjects.Location + "')";
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Creates new list then grabs all location data stored in Table and Adds them to the list before returning the list
        /// </summary>
        /// <returns>List of objects with all location data</returns>
        [HttpGet]
        [Route("Get")]
        public IEnumerable<FavouriteObjects> Get()
        {
            List<FavouriteObjects> tIObjects = new List<FavouriteObjects>();
            using (var conn = new SqlConnection("Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [dbo].[Favourites]";
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tIObjects.Add(

                                new FavouriteObjects
                                {
                                    Location = reader.GetString(1)
                                }
                            );
                        }

                    }
                    conn.Close();
                    return tIObjects;
                }
            }

        }

        /// <summary>
        /// Deleted given location value from database
        /// </summary>
        /// <param name="CLocation">Location value to be deleted</param>
        [HttpDelete]
        [Route("Delete/{CLocation}")]
        public void Delete([FromRoute] string CLocation)
        {
            using (var conn = new SqlConnection("Server=tcp:rg-weather-forecast2.database.windows.net,1433;Initial Catalog=WeatherForecastDBRG;Persist Security Info=False;User ID=dugz;Password=ReeceWeather123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [dbo].[Favourites] WHERE [Location] ='" + CLocation + "'";
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}