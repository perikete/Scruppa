using Scruppa.Scrappers;

namespace Scruppa.ScrappersActions
{
    public interface IScrapperAction
    {
        void RunAction(ScrapperResults results);
    }
}