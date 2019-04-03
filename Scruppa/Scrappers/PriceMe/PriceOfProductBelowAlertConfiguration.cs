namespace Scruppa.Scrappers.PriceMe
{

    public class PriceOfProductBelowAlertConfiguration : IAlertConfiguration
    {
        private readonly PriceMeScrapperResults _results;

        public decimal PriceToMatch { get; private set; }
        public string TitleToMatch { get; private set; }

        public string Description => "Alert for when the Price of a product drops below a set price";

        public PriceOfProductBelowAlertConfiguration(string titleToMatch, decimal priceToMatch)
        {
            TitleToMatch = titleToMatch;
            PriceToMatch = priceToMatch;
        }

        public bool Fired(ScrapperResults result)
        {
            var priceMeResult = (PriceMeScrapperResults)result;
            return priceMeResult.Title.ToLowerInvariant().Contains(TitleToMatch.ToLowerInvariant()) 
                || PriceToMatch <= priceMeResult.Price; 
        }
    }
}