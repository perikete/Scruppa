using System;

namespace Scruppa.Scrappers.PriceMe
{
    public class PriceMeScrapperResults : ScrapperResults
    {   
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}