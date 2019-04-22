using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scruppa.Scrappers.Logger;

namespace Scruppa.Scrappers
{
    public partial class ScrapperRunner
    {
        private const string Indent = "     ";
        private readonly IDictionary<IScrapper, HashSet<ScrapperRunnerConfiguration>> _scrapperConfigs;
        private readonly ILogger _logger;

        public ScrapperRunner(ILogger logger)
        {
            _scrapperConfigs = new Dictionary<IScrapper, HashSet<ScrapperRunnerConfiguration>>();
            _logger = logger;
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
                
                _logger.Log($"Running scrapper {scrapper.Name}...");

                var result = await scrapper.Scrap();
                var scrapperRunnerConfigs = scrapperConfig.Value;

                foreach (var scrapperRunnerConfig in scrapperRunnerConfigs)
                {
                    _logger.Log($"{Indent} Running configuration {scrapperRunnerConfig.GetType().Name}");

                    var runResult = scrapperRunnerConfig.AlertFired(result);
                    
                    if (runResult)
                    {
                        _logger.Log($"{Indent} Matched condition, firing action...");
                        scrapperRunnerConfig.FireAction(result);
                    }

                    scrapperRunResults.AddResult(scrapper, scrapperRunnerConfig, runResult);
                }
            }

            return scrapperRunResults;
        }
    }
}