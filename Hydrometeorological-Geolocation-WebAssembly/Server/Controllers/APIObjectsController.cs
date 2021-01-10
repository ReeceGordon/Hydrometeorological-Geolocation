using Hydrometeorological_Geolocation_Prototype_V2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hydrometeorological_Geolocation_Prototype_V2.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIObjectsController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<APIObjects> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new APIObjects
            {
                Date = DateTime.Now.AddDays(index),
                TempC = (rng.Next(-20, 55)).ToString(),
                Location = "Test Location",
                ObservedTime = "17:28 G",
                WeatherDescription = "It's pretty chilly mate"
            })
            .ToArray();
        }

    }
}