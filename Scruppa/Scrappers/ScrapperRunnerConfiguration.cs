using System;
using Scruppa.ScrappersActions;

namespace Scruppa.Scrappers
{
    public class ScrapperRunnerConfiguration
    {
        private readonly IAlertConfiguration _scrapperAlertConfiguration;
        private readonly IScrapperAction _actionToFire;

        public ScrapperRunnerConfiguration(IAlertConfiguration scrapperAlertConfiguration)
        {
            _scrapperAlertConfiguration = scrapperAlertConfiguration;
            
        }

        public ScrapperRunnerConfiguration(IAlertConfiguration scrapperAlertConfiguration, IScrapperAction actionToFire)
            : this(scrapperAlertConfiguration)
        {
            _actionToFire = actionToFire;
        }

        public void FireAction(ScrapperResults result)
        {
            if (_actionToFire != null) {
                _actionToFire.RunAction(result);
            }
        }

        public bool AlertFired(ScrapperResults result)
        {
            return _scrapperAlertConfiguration.Fired(result);
        }

        public string GetAlertDescription()
        {
            return _scrapperAlertConfiguration.GetDescription();
        }
    }
}