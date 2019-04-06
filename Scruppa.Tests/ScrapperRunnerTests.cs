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

            var firstScrapperConfig = configs.First();
            Assert.Equal(1, configs.Count());
            Assert.Equal(firstScrapperConfig.Key, scrapper1);
            var allAlertConfigs = firstScrapperConfig.Value.Select(o => o.ScrapperAlertConfiguration);
            Assert.Equal(allAlertConfigs.Count(), 2);
            Assert.True(allAlertConfigs.Any(o => o == config1));
            Assert.True(allAlertConfigs.Any(o => o == config2));
        }

        [Fact]
        public async Task Can_Add_Run_All_Configurations()
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


            var results = await scrapperRunner.RunAllConfigurations();

            var testScrapperRunResults = results.GetResultsForScrapper<TestScrapper>()
                .Select(o => o.Value);

            Assert.Equal(4, testScrapperRunResults.Count(o => !o));
            Assert.Equal(1, testScrapperRunResults.Count(o => o));
        }

        [Fact]
        public async void True_Result_With_Action_To_Execute_Should_Be_Fired()
        {
            var runner = new ScrapperRunner();
            var executed = false;

            var result = new TestScrapperResults() { Test = true };
            var scrapper = new TestScrapper(result);
            var config = new TestAlertConfig(true);

            var runConfig = new ScrapperRunnerConfiguration(config, (x) => { executed = ((TestScrapperResults)x).Test; });
            runner.AddConfigurations(scrapper, runConfig);

            var results = await runner.RunAllConfigurations();

            var testScrapperRunResults = results.GetResultsForScrapper<TestScrapper>();

            var firstResult = testScrapperRunResults.First();
            Assert.Equal(1, testScrapperRunResults.Count());
            Assert.True(executed);
            Assert.True(firstResult.Value);
        }

         [Fact]
        public async void False_Result_With_Action_To_Execute_Should_Not_Be_Fired()
        {
            var runner = new ScrapperRunner();
            var executed = false;

            var result = new TestScrapperResults() { Test = false };
            var scrapper = new TestScrapper(result);
            var config = new TestAlertConfig(true);

            var runConfig = new ScrapperRunnerConfiguration(config, (x) => { executed = ((TestScrapperResults)x).Test; });
            runner.AddConfigurations(scrapper, runConfig);

            var results = await runner.RunAllConfigurations();

            var testScrapperRunResults = results.GetResultsForScrapper<TestScrapper>();

            var firstResult = testScrapperRunResults.First();
            Assert.Equal(1, testScrapperRunResults.Count());
            Assert.False(executed);
            Assert.False(firstResult.Value);
        }
    }
}