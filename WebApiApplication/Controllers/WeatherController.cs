using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        const string apiKey = "YOUR_API_KEY_HERE";
        const string celsius = "celsius";
        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> City(string city)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    //Point the BaseAddress property Uri(Adress)
                    client.BaseAddress = new Uri("http://api.openweathermap.org");

                    //Pass the rest of the url when we make the call to client.GetAsync
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}& appid={apiKey} & units={celsius}");

                    //By calling 
                    response.EnsureSuccessStatusCode();

                    var strResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(strResult);
                    return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    });
                }
                catch(HttpRequestException ex)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {ex.Message}");
                }
            }
        }
    }
}
