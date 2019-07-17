using System;
using System.Net.Http;
using System.Threading.Tasks;
using JustEat.StatsD;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace MikaelStrid.SmartHome.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IStatsDPublisher _statsPublisher;

        public ClimateController(IHttpClientFactory clientFactory, IStatsDPublisher statsPublisher)
        {
            _clientFactory = clientFactory;
            _statsPublisher = statsPublisher;
        }

        [HttpPost]
        [Route("temperature-humidity")]
        public async Task<ActionResult> PostTemperatureAndHumidity([FromBody] TemperatureAndHumidityApiModel model)
        {
            //var client = _clientFactory.CreateClient();
            //await SaveToLocalElasticsearch(model, client);
            //await SendToAzureApi(model, client);
            //SaveToGraphite(model, _statsPublisher);

            await Task.CompletedTask;
            Console.WriteLine(JsonConvert.SerializeObject(model));
            return Ok();
        }
        

        #region Unused save methods

        private static async Task SendToAzureApi(TemperatureAndHumidityApiModel model, HttpClient client)
        {
            await client.PostAsJsonAsync(
                "https://smarthomereceiversappservice.azurewebsites.net/api/TemperatureHumidity",
                new
                {
                    model.SensorId,
                    model.Temperature,
                    model.Humidity
                });
        }

        private static async Task SaveToLocalElasticsearch(TemperatureAndHumidityApiModel model, HttpClient client)
        {
            await client.PostAsJsonAsync(
                "http://docker.for.win.localhost:9200/climate/temperature_humidity",
                new
                {
                    Timestamp = DateTimeOffset.Now,
                    model.SensorId,
                    model.Temperature,
                    model.Humidity
                });
        }

        private static void SaveToGraphite(TemperatureAndHumidityApiModel model, IStatsDPublisher statsDPublisher)
        {
            statsDPublisher.Gauge(model.Temperature, "climate.thirdfloor.temperature");
        }

        #endregion
    }

    public class TemperatureAndHumidityApiModel
{
    public string SensorId { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
}
}
