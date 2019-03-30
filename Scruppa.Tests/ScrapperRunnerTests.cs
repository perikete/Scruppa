using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Scruppa.Scrappers;
using Xunit;

namespace Scruppa.Tests
{
    public class ScrapperRunnerTests
    {
        [Fact]
        public void Can_Add_Scrapper_Configs()
        {
            var runner = new ScrapperRunner();

            var scrapper1 = Mock.Of<BaseScrapper>();
            var config1 = Mock.Of<IAlertConfiguration>();
            var config2 = Mock.Of<IAlertConfiguration>();


            runner.AddConfigurations(scrapper1, config1);
            runner.AddConfigurations(scrapper1, config2);

            var configs = runner.GetConfigurations();
            Assert.Equal(1, configs.Count);
            Assert.Equal(configs.First().Key, scrapper1);
            Assert.True(configs.First().Value.Contains(config1));
            Assert.True(configs.First().Value.Contains(config2));


        }

        [Fact]
        public void Can_Add_Run_All_Configurations()
        {
            var scrapper = new TestScrapper(new TestScrapperResults { Test = true });
            var result = new TestScrapperResults { Test = true };
            var config = new TestAlertConfig(true);
            var config1 = new TestAlertConfig(false);

            var scrapper1 = new TestScrapper(new TestScrapperResults { Test = true });
            var result1 = new TestScrapperResults { Test = true };
            var config2 = new TestAlertConfig(false);
            var config3 = new TestAlertConfig(false);
            var config4 = new TestAlertConfig(false);


            var scrapperRunner = new ScrapperRunner();

            scrapperRunner.AddConfigurations(scrapper, config);
            scrapperRunner.AddConfigurations(scrapper, config1);

            scrapperRunner.AddConfigurations(scrapper1, config2);
            scrapperRunner.AddConfigurations(scrapper1, config3);
            scrapperRunner.AddConfigurations(scrapper1, config4);


            var results = scrapperRunner.RunAllConfigurations();

        }
    }
}