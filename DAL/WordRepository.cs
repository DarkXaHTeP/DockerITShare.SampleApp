using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleApp.Web.DAL
{
    public class WordRepository : IWordRepository
    {
        private readonly WordContext _db;

        public WordRepository(WordContext db)
        {
            _db = db;
        }

        public IReadOnlyCollection<Word> List()
        {
            return new ReadOnlyCollection<Word>(_db.Words.ToList());
        }

        public void Add(string wordValue)
        {
            _db.Words.Add(new Word { Value = wordValue });
            _db.SaveChanges();
        }

        public void Delete(int wordId)
        {
            var word = _db.Words.FirstOrDefault(w => w.Id == wordId);
            if (word == null)
            {
                throw new WordNotFoundException(wordId);
            }

            _db.Words.Remove(word);
            _db.SaveChanges();
        }
    }
}