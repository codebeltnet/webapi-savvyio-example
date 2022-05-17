using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuemon.Extensions;
using Cuemon.Text;
using Savvyio.Domain;
using Savvyio.Domain.EventSourcing;
using Savvyio.Extensions;
using Savvyio.Extensions.EFCore;
using Savvyio.Extensions.EFCore.Domain.EventSourcing;
using Savvyio_Example.App.Domain;
using Savvyio_Example.App.Queries;

namespace Savvyio_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        private readonly IEfCoreDataStore _ds;

        public WeatherForecastController(IMediator mediator, ILogger<WeatherForecastController> logger, IEfCoreDataStore ds)
        {
            _logger = logger;
            _mediator = mediator;
            _ds = ds;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _mediator.QueryAsync(new GetWeatherForecast());
        }

        [HttpGet("events")]
        public Task<IActionResult> GetEvents()
        {
            var s = _ds.Set<EfCoreTracedAggregateEntity<LocationForecast, Guid>>();
            return Task.FromResult<IActionResult>(Ok(s));
        }
    }
}
