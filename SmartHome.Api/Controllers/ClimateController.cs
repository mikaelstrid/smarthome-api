using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace MikaelStrid.SmartHome.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public ClimateController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        [Route("temperature-humidity")]
        public async Task PostTemperatureAndHumidity([FromBody] TemperatureAndHumidityApiModel model)
        {
            var client = _clientFactory.CreateClient();
            var task1 = client.PostAsJsonAsync(
                "http://docker.for.win.localhost:9200/climate/temperature_humidity",
                new
                {
                    Timestamp = DateTimeOffset.Now,
                    model.SensorId,
                    model.Temperature,
                    model.Humidity
                });
            var task2 = client.PostAsJsonAsync(
                "https://smarthomereceiversappservice.azurewebsites.net/api/TemperatureHumidity",
                new
                {
                    model.SensorId,
                    model.Temperature,
                    model.Humidity
                });
            await Task.WhenAll(task1, task2);
        }
    }

    public class TemperatureAndHumidityApiModel
    {
        public string SensorId { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
