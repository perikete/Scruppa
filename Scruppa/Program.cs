using System;
using System.Threading.Tasks;
using Scruppa.Scrappers;
using Scruppa.Scrappers.PriceMe;
using Scruppa.ScrappersActions;
using static Scruppa.Scrappers.ScrapperRunner;

namespace Scruppa
{
    class Program
    {
        static void Main(string[] args)
        {
            

            RunScrappers().Wait();
            
            Console.ReadLine();
        }

        public static async Task<ScrapperRunnerResults> RunScrappers()
        {
            var runner = new ScrapperRunner();
            var priceMeAlertConfig = new PriceOfProductBelowAlertConfiguration("hero 7", 550);
            var scrapperRunnerConfig = new ScrapperRunnerConfiguration(priceMeAlertConfig, SendEmail.SendMail);
            
            var priceMeScrapper = new PriceMeScrapper();

            runner.AddConfigurations(priceMeScrapper, scrapperRunnerConfig);

            Console.WriteLine("Running scrappers...");
            
            var results = await runner.RunAllConfigurations();

            Console.WriteLine("Scrappers ran successfully, showing results...");

            foreach(var result in results.GetResults())
            {
                Console.WriteLine($"Match result for Scrapper: {result.Key}:");
                
                foreach(var config in result.Value)
                {
                    Console.WriteLine($"for the config({config.Key.GetType().Name}): {config.Key.ScrapperAlertConfiguration.GetDescription()} with value: {config.Value}.");
                }
            }

            return results;
        }
    }
}
