using Scruppa.Scrappers.PriceMe;
using Xunit;

namespace Scruppa.Tests.Scrappers.PriceMe
{
    public class PriceOfProductBelowAlertConfigurationTests
    {
        [Fact]
        public void When_Both_Conditions_Match_Alarm_Should_Be_Fired()
        {
            var result = new PriceMeScrapperResults { Price = 500, Title = "gopro"};

            var alert = new PriceOfProductBelowAlertConfiguration("GoPro", 500);

            Assert.True(alert.Fired(result));
        }
    }
}