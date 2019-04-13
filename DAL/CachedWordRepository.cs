using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Caching.Distributed;

namespace SampleApp.Web.DAL
{
    public class CachedWordRepository : IWordRepository
    {
        private const string WordListKey = "SampleApp__words";

        private readonly IWordRepository _repository;
        private readonly IDistributedCache _cache;

        public CachedWordRepository(IWordRepository repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public IReadOnlyCollection<Word> List()
        {
            byte[] wordsBytes = _cache.Get(WordListKey);

            if (wordsBytes != null)
            {
                return Deserialize(wordsBytes);
            }

            var words = _repository.List();
            
            _cache.Set(WordListKey, Serialize(words));

            return words;

        }

        public void Add(string wordValue)
        {
            _repository.Add(wordValue);
            CleanCache();
        }

        public void Delete(int wordId)
        {
            _repository.Delete(wordId);
            CleanCache();
        }

        private void CleanCache()
        {
            _cache.Remove(WordListKey);
        }

        private byte[] Serialize(IReadOnlyCollection<Word> words)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, words);

            return mStream.ToArray();
        }

        private IReadOnlyCollection<Word> Deserialize(byte[] wordBytes)
        {
            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();

            mStream.Write(wordBytes, 0, wordBytes.Length);
            mStream.Position = 0;

            return binFormatter.Deserialize(mStream) as IReadOnlyCollection<Word>;
        }
    }
}
