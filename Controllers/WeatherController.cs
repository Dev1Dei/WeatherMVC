using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebMVC.Models;
namespace WebMVC.Controllers
{
    [Route("Weather")]
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherController(HttpClient httpClient,ILogger<WeatherController> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("GetWeatherInCity")]
        public async Task<IActionResult> GetWeatherInCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ViewBag.Error = "Please enter a valid city name.";
                return View("/Views/Home/Index.cshtml");
            }

            string currentUrl = _configuration["WeatherApi:CurrentUrl"];
            string apiKey = _configuration["WeatherApi:ApiKey"];
            string url = $"{currentUrl}?key={apiKey}&q={city}&aqi=yes";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error fetching weather data.";
                return View("/Views/Home/Index.cshtml");
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(content);

            var weatherData = JsonSerializer.Deserialize<WeatherApiResponseModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (weatherData?.Current == null || weatherData.Location == null)
            {
                ViewBag.Error = "Weather data is unavailable.";
                return View("/Views/Home/Index.cshtml", weatherData);
            }

            return View("/Views/Home/Index.cshtml", weatherData);
        }

    }
}
