﻿using System;
using Microsoft.Extensions.Configuration;

namespace Scruppa.Scrappers.PriceMe
{

    public class PriceOfProductBelowAlertConfiguration : IAlertConfiguration
    {
        public decimal PriceToMatch { get; private set; }
        public string TitleToMatch { get; private set; }

        private decimal _priceValue;

        public string GetDescription()
        {
            return $"Alert for when the Price of the product '{TitleToMatch}' drops below the price '{PriceToMatch}', actual value is: '{_priceValue}'";
        }

        public PriceOfProductBelowAlertConfiguration(string titleToMatch, decimal priceToMatch)
        {
            TitleToMatch = titleToMatch;
            PriceToMatch = priceToMatch;
        }

        public PriceOfProductBelowAlertConfiguration(IConfiguration config)
        {
            TitleToMatch = config["PriceMe:PriceMeAlertConfiguration:TitleToMatch"];
            PriceToMatch = Convert.ToDecimal(config["PriceMe:PriceMeAlertConfiguration:PriceToMatch"]);
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