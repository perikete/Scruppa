using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        private readonly IDictionary<IScrapper, HashSet<ScrapperRunnerConfiguration>> _scrapperConfigs;

        public ScrapperRunner()
        {
            _scrapperConfigs = new Dictionary<IScrapper, HashSet<ScrapperRunnerConfiguration>>();
        }

        public void AddConfigurations(IScrapper scrapper, IAlertConfiguration config)
        {
            AddConfigurations(scrapper, new ScrapperRunnerConfiguration(config));

        }

        public void AddConfigurations(IScrapper scrapper, ScrapperRunnerConfiguration runnerConfig)
        {
            if (_scrapperConfigs.ContainsKey(scrapper))
            {
                _scrapperConfigs[scrapper].Add(runnerConfig);
            }
            else
            {
                _scrapperConfigs.Add(scrapper,
                    new HashSet<ScrapperRunnerConfiguration> { runnerConfig });
            }
        }

        public IDictionary<IScrapper, HashSet<ScrapperRunnerConfiguration>> GetConfigurations()
        {
            return _scrapperConfigs;
        }

        public async Task<ScrapperRunnerResults> RunAllConfigurations()
        {
            var scrapperRunResults = new ScrapperRunnerResults();
            foreach (var scrapperConfig in _scrapperConfigs)
            {
                var scrapper = scrapperConfig.Key;

                var result = await scrapper.Scrap();
                var scrapperRunnerConfigs = scrapperConfig.Value;

                foreach (var scrapperRunnerConfig in scrapperRunnerConfigs)
                {
                    var runResult = scrapperRunnerConfig.AlertFired(result);
                    
                    if (runResult)
                    {
                        scrapperRunnerConfig.FireAction(result);
                    }

                    scrapperRunResults.AddResult(scrapper, scrapperRunnerConfig, runResult);
                }
            }

            return scrapperRunResults;
        }
    }
}