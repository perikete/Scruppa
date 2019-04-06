using System;

namespace Scruppa.Scrappers
{
    public class ScrapperRunnerConfiguration
    {
        public IAlertConfiguration ScrapperAlertConfiguration { get; private set; }
        private Action<ScrapperResults> _actionToFire;

        public ScrapperRunnerConfiguration(IAlertConfiguration scrapperAlertConfiguration)
        {
            ScrapperAlertConfiguration = scrapperAlertConfiguration;
            
        }

        public ScrapperRunnerConfiguration(IAlertConfiguration scrapperAlertConfiguration, Action<ScrapperResults> actionToFire)
            : this(scrapperAlertConfiguration)
        {
            _actionToFire = actionToFire;
        }

        public void FireAction(ScrapperResults result)
        {
            if (_actionToFire != null) {
                _actionToFire(result);
            }
        }
    }
}