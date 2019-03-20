using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Web.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new int[] { 1, 2, 3, 4 });
        }

        [HttpPost]
        public IActionResult Post([FromBody] int value)
        {
            return this.Ok(value);
        }
    }
}
