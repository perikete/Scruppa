using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OpenScraping;
using OpenScraping.Config;

namespace Scruppa.Scrappers.PriceMe
{
    public class PriceMeScrapper : BaseScrapper
    {
        private ConfigSection _config;

        public PriceMeScrapper() : base()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Scrappers"), Path.Combine("PriceMe"), Path.Combine("PriceMe.config.json"));
            _config = StructuredDataConfig.ParseJsonFile(path);
        }

        public override string Name => "Price Me NZ Scrapper";

        public override async Task<ScrapperResults> Scrap()
        {
            var openScraping = new StructuredDataExtractor(_config);
            var html = await GetSiteData();

            var scrapingResults = openScraping.Extract(html);

            return scrapingResults.ToObject<PriceMeScrapperResults>();
        }

        private async Task<string> GetSiteData()
        {
            using (var client = new HttpClient())
            {
                var siteText = await client.GetStringAsync("https://www.bbc.com/news/world-asia-47590685");
                return siteText;
            }
        }
    }
}