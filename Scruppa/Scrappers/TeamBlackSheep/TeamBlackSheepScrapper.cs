using System.IO;
using Newtonsoft.Json.Linq;
using OpenScraping.Config;

namespace Scruppa.Scrappers.TeamBlackSheep
{
    public class TeamBlackSheepScrapper : BaseScrapper
    {
        public override string Name => "Team Blacksheep Scrapper";
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), 
            Path.Combine("Scrappers"), Path.Combine("TeamBlackSheep"), 
            Path.Combine("TBS.config.json"));


        public TeamBlackSheepScrapper()
        : base("https://www.team-blacksheep.com/products/prod:tbs_unify_evo", StructuredDataConfig.ParseJsonFile(_path))
        {
        }

        protected override ScrapperResults ToResultsCore(JContainer data)
        {
            return data.ToObject<TeamBlackSheepScrapperResults>();
        }
    }
}