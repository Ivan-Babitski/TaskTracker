using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaskTracker.UI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        //[HttpGet("{taskId}")]
        //public async Task<IEnumerable<object>> GetTaskById(int taskId)
        //{
        //    var tasks = await _taskApiService.GetTaskById(taskId).ListAsync();
        //    return tasks;
        //}


        //[HttpGet("{taskId}")]
        //public ActionResult<TaskApi> GetTaskById(int taskId)
        //{
        //    if (taskId > 0)
        //    {
        //        return new ObjectResult(_taskDtoService.GetTaskById(taskId));
        //    }
        //    return BadRequest(taskId);
        //}
    }
}