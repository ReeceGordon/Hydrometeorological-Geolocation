# Hydrometeorological-Geolocation
 
Weather forecast and tidal data for given location.

Timeline of functionality once a given location is entered into the search bar is as follow:
-First grabs Weather data from WorldWeatherOnline
-Sends input location to GeoCoding api to convert City to Lat Long
-Such Lat Long is sent to MarineData API to grab marine data from nearest marine point
-Returned Lat and Lon from Marine Data Point is sent to reverse GeoCoding api to find closest City to input location
-Infomation is presented with Bootstrap HTML

Marine Data included: Tidal Swell, Swell Height, Swell period, Significant Wave Height, Tide Time et al

Uses .Net Blazor WebAssembly and MVC. Pulls data from World Weather Online API for Weather forecast data, Admiralty API for Marine Forecasting tidal data and MapQuest API for latitude and longitude data.

Also utilised user login implmentation through .Net Identity Framework, SQL data storage, Unit testing prior to deployment to Azure App Services.
