using Microsoft.AspNetCore.Mvc;

namespace WeGrow.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hi, that's WeGrow company");
        }
    }
}
