using System.Threading.Tasks;

namespace Scruppa.Scrappers
{
    public interface IScrapper
    {
        Task<ScrapperResults> Scrap();
        string Name { get; }

        string ScrapperUri { get; }
    }
}