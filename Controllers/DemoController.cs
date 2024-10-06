using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _myLogger;

        public DemoController(ILogger<DemoController> mylogger)
        {
            _myLogger = mylogger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.LogTrace("log message to trace ");
            _myLogger.LogDebug("log message to Debug ");
            _myLogger.LogInformation("log message to Information ");
            _myLogger.LogWarning("log message to Warning ");
            _myLogger.LogError("log message to Error ");
            _myLogger.LogCritical("log message to Critical ");
           
            return Ok();
        }
    }
}
