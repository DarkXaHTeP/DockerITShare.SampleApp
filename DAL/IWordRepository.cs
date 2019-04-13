using System.Collections.Generic;

namespace SampleApp.Web.DAL
{
    public interface IWordRepository
    {
        IReadOnlyCollection<Word> List();
        void Add(string wordValue);
        void Delete(int wordId);
    }
}