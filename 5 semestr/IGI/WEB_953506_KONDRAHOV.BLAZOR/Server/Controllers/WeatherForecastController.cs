using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_953506_KONDRAHOV.BLAZOR.Shared;
using WEB_953506_KONDRASHOV.Data;
using WEB_953506_KONDRASHOV.Entities;

namespace WEB_953506_KONDRAHOV.BLAZOR.Server.Controllers
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
        private ApplicationDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Engine>>> GetEngines(int? group)
        {
            var engines = _context.Engines.Where(d => !group.HasValue || d.ClassId == group.Value);

            return await engines.ToListAsync();
        }
    }
}
