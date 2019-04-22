using Microsoft.Extensions.Configuration;
using Scruppa.Scrappers.Logger;
using Scruppa.ScrappersActions.Twilio;

namespace Scruppa.Scrappers.PriceMe
{
    public static class PriceMeScrapperUtil
    {
        public static ScrapperRunner AddPriceMeScrapper(this ScrapperRunner runner, IConfiguration configuration, ILogger logger)
        {
            var priceMeAlertConfig = configuration.GetSection("PriceMe:PriceMeAlertConfiguration").Get<Scrappers.PriceMe.PriceOfProductBelowAlertConfiguration>();
            var scrapperRunnerConfig = new ScrapperRunnerConfiguration(priceMeAlertConfig, new TwilioSmsAction((result) => TransformPriceMeResultsToSms(result), configuration, logger));
            var priceMeScrapper = new PriceMeScrapper(configuration);

            runner.AddConfigurations(priceMeScrapper, scrapperRunnerConfig);

            return runner;
        }

        private static string TransformPriceMeResultsToSms(ScrapperResults results)
        {
            var priceMeScrapperResult = (PriceMeScrapperResults)results;
            return $"Price Me Scrapper({results.ScrapperUri}) result found this match: {priceMeScrapperResult.Title} with price: {priceMeScrapperResult.Price}";
        }
    }
}