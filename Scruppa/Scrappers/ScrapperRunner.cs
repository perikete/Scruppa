using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        private readonly IDictionary<BaseScrapper, HashSet<IAlertConfiguration>> _scrapperConfigs;

        public ScrapperRunner()
        {
            _scrapperConfigs = new Dictionary<BaseScrapper, HashSet<IAlertConfiguration>>();
        }

        public void AddConfigurations(BaseScrapper scrapper, IAlertConfiguration config)
        {
            if (_scrapperConfigs.ContainsKey(scrapper))
            {
                _scrapperConfigs[scrapper].Add(config);
            }
            else
            {
                _scrapperConfigs.Add(scrapper, new HashSet<IAlertConfiguration> { config });
            }
        }

        public IDictionary<BaseScrapper, HashSet<IAlertConfiguration>> GetConfigurations()
        {
            return _scrapperConfigs;
        }

        public async Task<ScrapperRunnerResults> RunAllConfigurations()
        {
            var scrapperRunResults = new ScrapperRunnerResults();
            foreach (var scrapperConfig in _scrapperConfigs)
            {
                var scrapper = scrapperConfig.Key;
                var configs = scrapperConfig.Value;

                var result = await scrapper.Scrap();

                foreach (var config in configs)
                {
                    var runResult = config.Fired(result);
                    scrapperRunResults.AddResult(scrapper, config, runResult);
                }
            }

            return scrapperRunResults;
        }
    }
}