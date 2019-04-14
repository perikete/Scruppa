using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Scruppa.Scrappers;

namespace Scruppa.Tests
{
    public class TestScrapper : IScrapper
    {
        private readonly TestScrapperResults result;

        public string Name => "Test Scrapper";

        public string ScrapperUri => "Test URI";

        public TestScrapper(TestScrapperResults result)
        {
            this.result = result;
        }

        public Task<ScrapperResults> Scrap()
        {
            return Task.FromResult((ScrapperResults)result);
        }
    }

    public class TestScrapperResults : ScrapperResults
    {
        public bool Test { get; set; }
    }

    public class TestAlertConfig : IAlertConfiguration
    {
        private readonly bool _test;
      
        public TestAlertConfig(bool test)
        {
            _test = test;
        }

        public bool Fired(ScrapperResults result)
        {
            return ((TestScrapperResults)result).Test == _test;
        }

        public string GetDescription()
        {
            return "Test alert configuration";
        }
    }
}