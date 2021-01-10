using System;
using System.Collections.Generic;
using System.Text;

namespace Hydrometeorological_Geolocation_Prototype_V2.Shared
{
    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
