using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scruppa.Scrappers
{
    public abstract class BaseScrapper
    {
        public abstract string Name { get; }
        public abstract Task<ScrapperResults> Scrap();
    }
}