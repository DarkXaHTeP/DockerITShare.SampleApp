using Microsoft.AspNetCore.Mvc;
using SampleApp.Web.DAL;

namespace SampleApp.Web.Controllers
{
    [Route("api/words")]
    public class WordController : Controller
    {
        private readonly IWordRepository _repository;

        public WordController(IWordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.List());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Word word)
        {
            _repository.Add(word.Value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch (WordNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
