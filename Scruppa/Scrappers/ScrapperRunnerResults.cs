using System.Collections.Generic;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        public class ScrapperRunnerResults
        {
            private readonly IDictionary<string, IList<KeyValuePair<string, string>>> _scrapperRunResults;

            public ScrapperRunnerResults()
            {
                _scrapperRunResults = new Dictionary<string, IList<KeyValuePair<string, string>>>();
            }

            public void AddResult(BaseScrapper scrapper, IAlertConfiguration configuration, bool alertValue)
            {

                var newKvp = new KeyValuePair<string, string>($"the result of the configuration {nameof(configuration)} is: ", alertValue.ToString());

                if (_scrapperRunResults.ContainsKey(nameof(scrapper)))
                {
                    _scrapperRunResults[nameof(scrapper)].Add(newKvp);
                }
                else
                {
                    _scrapperRunResults.Add(nameof(scrapper), new List<KeyValuePair<string, string>> { newKvp });
                }
            }
        }
    }
}