using System;
using System.Net.Http;
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
        public void PostTemperatureAndHumidity([FromBody] TemperatureAndHumidityApiModel model)
        {
            var client = _clientFactory.CreateClient();
            client.PostAsJsonAsync(
                "http://docker.for.win.localhost:9200/climate/temperature_humidity",
                new
                {
                    Timestamp = DateTimeOffset.Now,
                    model.SensorId,
                    model.Temperature,
                    model.Humidity
                });
        }
    }

    public class TemperatureAndHumidityApiModel
    {
        public string SensorId { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
