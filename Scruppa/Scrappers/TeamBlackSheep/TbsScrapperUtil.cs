using Microsoft.Extensions.Configuration;
using Scruppa.Scrappers.Logger;
using Scruppa.ScrappersActions.Twilio;

namespace Scruppa.Scrappers.TeamBlackSheep
{
    public static class TbsScrapperUtil
    {
        public static ScrapperRunner AddTbsScrapper(this ScrapperRunner runner, IConfiguration configuration, ILogger logger)
        {
            var tbsAlertConfig = configuration.GetSection("TBS:TbsAlertConfiguration").Get<Scrappers.TeamBlackSheep.PriceOfProductBelowAlertConfiguration>();
            var tbsRunnerConfig = new ScrapperRunnerConfiguration(tbsAlertConfig, new TwilioSmsAction((result) => TransformTbsResultsToSms(result), configuration, logger));
            var tbsScrapper = new TeamBlackSheepScrapper();

            runner.AddConfigurations(tbsScrapper, tbsRunnerConfig);

            return runner;
        }

        private static string TransformTbsResultsToSms(ScrapperResults results)
        {
            var tbsScrapperResult = (TeamBlackSheepScrapperResults)results;
            return $"TBS Scrapper({results.ScrapperUri}) result found for TBS Unify EVO with price: {tbsScrapperResult.Price}";
        }
    }
}