using System;
using System.Collections.Generic;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        public class ScrapperRunnerResults
        {
            private readonly IDictionary<string, List<KeyValuePair<IAlertConfiguration, bool>>> _scrapperRunResults;

            public ScrapperRunnerResults()
            {
                _scrapperRunResults = new Dictionary<string, List<KeyValuePair<IAlertConfiguration, bool>>>();
            }

            public void AddResult(BaseScrapper scrapper, IAlertConfiguration configuration, bool alertValue)
            {

                var resultKvp = new KeyValuePair<IAlertConfiguration, bool>(configuration, alertValue);
                var key = scrapper.GetType().Name;
                
                if (_scrapperRunResults.ContainsKey(key))
                {
                    _scrapperRunResults[key].Add(resultKvp);
                }
                else
                {
                    _scrapperRunResults.Add(key, new List<KeyValuePair<IAlertConfiguration, bool>> { resultKvp });
                }
            }

            public IDictionary<string, List<KeyValuePair<IAlertConfiguration, bool>>> GetResults()
            {
                return _scrapperRunResults;
            }
            
            public IEnumerable<KeyValuePair<IAlertConfiguration, bool>> GetResultsForScrapper<TScrapper>()
            {
                return GetResults()[typeof(TScrapper).Name];
            }
        }
    }
}