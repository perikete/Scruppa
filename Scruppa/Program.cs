using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Scruppa.Scrappers;
using Scruppa.Scrappers.PriceMe;
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

            RunScrappers(builder.Build()).Wait();

            Console.ReadLine();
        }

        public static string TransformPriceMeResultsToSms(ScrapperResults results)
        {
            var priceMeScrapperResult = (PriceMeScrapperResults)results;
            return $"Price Me Scrapper({results.ScrapperUri}) result found this match: {priceMeScrapperResult.Title} with price: {priceMeScrapperResult.Price}";
        }

        public static async Task<ScrapperRunnerResults> RunScrappers(IConfiguration configuration)
        {
            var runner = new ScrapperRunner();
            var priceMeAlertConfig = new PriceOfProductBelowAlertConfiguration(configuration);
            var scrapperRunnerConfig = new ScrapperRunnerConfiguration(priceMeAlertConfig, new TwilioSmsAction((result) => TransformPriceMeResultsToSms(result), configuration));

            var priceMeScrapper = new PriceMeScrapper(configuration);

            runner.AddConfigurations(priceMeScrapper, scrapperRunnerConfig);

            Console.WriteLine("Running scrappers...");

            var results = await runner.RunAllConfigurations();

            Console.WriteLine("Scrappers ran successfully, showing results...");

            foreach (var result in results.GetResults())
            {
                Console.WriteLine($"Match result for Scrapper: {result.Key}:");

                foreach (var config in result.Value)
                {
                    Console.WriteLine($"for the config({config.Key.GetType().Name}): {config.Key.GetAlertDescription()} with value: {config.Value}.");
                }
            }

            return results;
        }
    }
}
