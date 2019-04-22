using System;
using Microsoft.Extensions.Configuration;

namespace Scruppa.Scrappers.PriceMe
{

    public class PriceOfProductBelowAlertConfiguration : IAlertConfiguration
    {
        public decimal PriceToMatch { get; set; }
        public string TitleToMatch { get; set; }

        private decimal _priceValue;

        public string GetDescription()
        {
            return $"Alert for when the Price of the product '{TitleToMatch}' drops below the price '{PriceToMatch}', actual value is: '{_priceValue}'";
        }

        public bool Fired(ScrapperResults result)
        {
            var priceMeResult = (PriceMeScrapperResults)result;
            _priceValue = priceMeResult.Price;
            return priceMeResult.Title.ToLowerInvariant().Contains(TitleToMatch.ToLowerInvariant())
                && priceMeResult.Price <= PriceToMatch;
        }
    }
}