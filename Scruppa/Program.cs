using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Scruppa.Scrappers;
using Scruppa.Scrappers.Logger;
using Scruppa.Scrappers.PriceMe;
using Scruppa.Scrappers.TeamBlackSheep;
using Scruppa.ScrappersActions;
using Scruppa.ScrappersActions.Twilio;
using static Scruppa.Scrappers.ScrapperRunner;

namespace Scruppa
{
    class Program
    {
        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: true, reloadOnChange: true);

            if (string.IsNullOrWhiteSpace(environmentName) || environmentName == "dev")
            {
                builder.AddUserSecrets<Program>();
            }

            RunScrappers(builder.Build(), new DefaultLogger()).Wait();

            Console.ReadLine();
        }

        public static async Task<ScrapperRunnerResults> RunScrappers(IConfiguration configuration, ILogger logger)
        {
            var runner = new ScrapperRunner(logger)
                .AddPriceMeScrapper(configuration, logger)
                .AddTbsScrapper(configuration, logger);

            logger.Log("Running scrappers...");

            var results = await runner.RunAllConfigurations();

            logger.Log("Scrappers ran successfully, showing results...");

            foreach (var result in results.GetResults())
            {
                logger.Log($"Match result for Scrapper: {result.Key}:");

                foreach (var config in result.Value)
                {
                    logger.Log($"for the config({config.Key.GetType().Name}): {config.Key.GetAlertDescription()} with value: {config.Value}.");
                }
            }

            return results;
        }
    }
}
