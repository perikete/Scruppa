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

            var alert = new PriceOfProductBelowAlertConfiguration { TitleToMatch = "GoPro", PriceToMatch = 500 };

            Assert.True(alert.Fired(result));
        }

        [Fact]
        public void When_Price_Is_Above_Condition_Alarm_Should_Be_False()
        {
            var result = new PriceMeScrapperResults { Price = 510, Title = "gopro"};

            var alert = new PriceOfProductBelowAlertConfiguration { TitleToMatch = "GoPro", PriceToMatch = 500 };

            Assert.False(alert.Fired(result));
        }

        [Fact]
        public void When_Price_Is_Below_Condition_Alarm_Should_Be_True()
        {
            var result = new PriceMeScrapperResults { Price = 450, Title = "gopro"};

            var alert = new PriceOfProductBelowAlertConfiguration { TitleToMatch = "GoPro", PriceToMatch = 500 };

            Assert.True(alert.Fired(result));
        }

        [Fact]
        public void When_Title_Dont_Matches_But_Price_Is_Below_Condition_Alarm_Should_Be_False()
        {
            var result = new PriceMeScrapperResults { Price = 450, Title = "gepro"};

            var alert = new PriceOfProductBelowAlertConfiguration { TitleToMatch = "GoPro", PriceToMatch = 500 };            

            Assert.False(alert.Fired(result));
        }

         [Fact]
        public void When_Title_Dont_Matches_But_Price_Is_Above_Condition_Alarm_Should_Be_False()
        {
            var result = new PriceMeScrapperResults { Price = 510, Title = "gepro"};
            
            var alert = new PriceOfProductBelowAlertConfiguration { TitleToMatch = "GoPro", PriceToMatch = 500 };

            Assert.False(alert.Fired(result));
        }
    }
}