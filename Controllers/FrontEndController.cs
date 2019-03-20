using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace SampleApp.Web.Controllers
{
    public class FrontEndController : Controller
    {
        private readonly IHostingEnvironment _env;

        public FrontEndController(IHostingEnvironment env)
        {
            _env = env;
        }
        
        [Route("{*url}")]
        public IActionResult Index()
        {
            IFileInfo file = _env.WebRootFileProvider.GetFileInfo("index.html");

            if (!file.Exists)
            {
                return NotFound("index.html file is not found");
            }
            
            using (var stream = file.CreateReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return Content(reader.ReadToEnd(), "text/html");
                }
            }
        }
    }
}
