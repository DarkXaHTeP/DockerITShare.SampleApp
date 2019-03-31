using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Web.DAL;

namespace SampleApp.Web.Controllers
{
    [Route("api/words")]
    public class WordController : Controller
    {
        private readonly WordContext _db;

        public WordController(WordContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Words.ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Word word)
        {
            Console.WriteLine("Value is: " + word.Value);
            _db.Words.Add(new Word() {Value = word.Value});
            _db.SaveChanges();
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var word = _db.Words.FirstOrDefault(w => w.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            _db.Words.Remove(word);
            _db.SaveChanges();
            return Ok();
        }
    }
}
