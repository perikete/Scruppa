using System;
using System.Collections.Generic;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        public class ScrapperRunnerResults
        {
            private readonly IDictionary<string, List<KeyValuePair<Type, bool>>> _scrapperRunResults;

            public ScrapperRunnerResults()
            {
                _scrapperRunResults = new Dictionary<string, List<KeyValuePair<Type, bool>>>();
            }

            public void AddResult(BaseScrapper scrapper, IAlertConfiguration configuration, bool alertValue)
            {

                var resultKvp = new KeyValuePair<Type, bool>(configuration.GetType(), alertValue);
                var key = scrapper.GetType().Name;
                
                if (_scrapperRunResults.ContainsKey(key))
                {
                    _scrapperRunResults[key].Add(resultKvp);
                }
                else
                {
                    _scrapperRunResults.Add(key, new List<KeyValuePair<Type, bool>> { resultKvp });
                }
            }

            public IDictionary<string, List<KeyValuePair<Type, bool>>> GetResults()
            {
                return _scrapperRunResults;
            }
            
            public IEnumerable<KeyValuePair<Type, bool>> GetResultsForScrapper<TScrapper>()
            {
                return GetResults()[typeof(TScrapper).Name];
            }
        }
    }
}