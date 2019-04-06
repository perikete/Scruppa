using System;
using System.Collections.Generic;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        public class ScrapperRunnerResults
        {
            private readonly IDictionary<string, List<KeyValuePair<ScrapperRunnerConfiguration, bool>>> _scrapperRunResults;

            public ScrapperRunnerResults()
            {
                _scrapperRunResults = new Dictionary<string, List<KeyValuePair<ScrapperRunnerConfiguration, bool>>>();
            }

            public void AddResult(BaseScrapper scrapper, ScrapperRunnerConfiguration configuration, bool alertValue)
            {

                var resultKvp = new KeyValuePair<ScrapperRunnerConfiguration, bool>(configuration, alertValue);
                var key = scrapper.GetType().Name;
                
                if (_scrapperRunResults.ContainsKey(key))
                {
                    _scrapperRunResults[key].Add(resultKvp);
                }
                else
                {
                    _scrapperRunResults.Add(key, new List<KeyValuePair<ScrapperRunnerConfiguration, bool>> { resultKvp });
                }
            }

            public IDictionary<string, List<KeyValuePair<ScrapperRunnerConfiguration, bool>>> GetResults()
            {
                return _scrapperRunResults;
            }
            
            public IEnumerable<KeyValuePair<ScrapperRunnerConfiguration, bool>> GetResultsForScrapper<TScrapper>()
            {
                return GetResults()[typeof(TScrapper).Name];
            }
        }
    }
}