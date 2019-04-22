using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;

namespace Scruppa.Scrappers
{
    public abstract class BaseScrapper : IScrapper
    {
        private readonly ConfigSection _config;
        public abstract string Name { get; }
        public string ScrapperUri { get ;}

        public BaseScrapper(string scrapUri, ConfigSection config)
        {
            ScrapperUri = scrapUri;
            _config = config;
        }
        public async Task<ScrapperResults> Scrap()
        {
            var data = await GetSiteData(ScrapperUri);
            var extractor = GetDataExtractor(_config);

            var results = GetResults(extractor, data);

            return ToResults(results);
        }

        protected async Task<string> GetSiteData(string scrapUri)
        {
            using (var client = new HttpClient())
            {
                var siteText = await client.GetStringAsync(scrapUri);
                return siteText;
            }
        }

        protected StructuredDataExtractor GetDataExtractor(ConfigSection config)
        {
            return new StructuredDataExtractor(config);
        }

        protected JContainer GetResults(StructuredDataExtractor extractor, string data)
        {
           return extractor.Extract(data);
        } 

        protected ScrapperResults ToResults(JContainer data)
        {
            var result = ToResultsCore(data);
            result.ScrapperUri = ScrapperUri;

            return result;

        }
        protected abstract ScrapperResults ToResultsCore(JContainer data);
       
    }
}