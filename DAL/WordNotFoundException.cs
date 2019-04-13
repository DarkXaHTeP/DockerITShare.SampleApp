using System;

namespace SampleApp.Web.DAL
{
    public class WordNotFoundException : Exception
    {
        public WordNotFoundException(int wordId) : base($"Word not found. Id = {wordId}") {}
    }
}
