using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OpenScraping.Config;

namespace Scruppa.Scrappers.PriceMe
{
    public class PriceMeScrapper : BaseScrapper
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Scrappers"), Path.Combine("PriceMe"), Path.Combine("PriceMe.config.json"));
        public override string Name => "Price Me NZ Scrapper";

        public PriceMeScrapper(IConfiguration configuration)
            : base(configuration["PriceMe:PriceMeUri"], StructuredDataConfig.ParseJsonFile(_path))
        {
        }

        protected override ScrapperResults ToResultsCore(JContainer data)
        {
            return data.ToObject<PriceMeScrapperResults>();
        }
    }
}