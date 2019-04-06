using System.Threading.Tasks;
using Scruppa.Scrappers;

namespace Scruppa.Tests
{
    public class TestScrapper : BaseScrapper
    {
        private readonly TestScrapperResults result;

        public override string Name => "Test Scrapper";

        public TestScrapper(TestScrapperResults result)
        {
            this.result = result;
        }

        public override Task<ScrapperResults> Scrap()
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