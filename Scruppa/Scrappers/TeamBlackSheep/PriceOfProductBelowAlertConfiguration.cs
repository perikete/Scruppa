using Microsoft.Extensions.Configuration;

namespace Scruppa.Scrappers.TeamBlackSheep
{
    public class PriceOfProductBelowAlertConfiguration : IAlertConfiguration
    {
        public decimal PriceToMatch {get; set; }
        private decimal _priceValue;
        public bool Fired(ScrapperResults result)
        {
            var tbsScrapResult = (TeamBlackSheepScrapperResults)result;
            _priceValue = tbsScrapResult.Price;

            return tbsScrapResult.Price <= 60;
        }

        public string GetDescription()
        {
            return $"Alert for when the Price of the product TBS Unify Evo VTX drops below the price '{PriceToMatch}', actual value is: '{_priceValue}'";
        }
    }
}