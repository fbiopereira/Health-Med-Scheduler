using Health.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HealthMedScheduler.Api.Controllers
{
    [Route("Live")]
    public class LiveController : MainController<LiveController>
    {
        private readonly ILogger _logger;

        public LiveController(ILogger<LiveController> logger) : base(logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Indica para sistemas de monitoração que a API está UP.
        /// </summary>
        /// <returns>String com Data e Hora correntes.</returns>
        [HttpGet("/")]
        public IActionResult ImAlive()
        {
            _logger.LogInformation("Estou vivo.");
            return Ok(System.DateTime.Now);
        }
    }
}
