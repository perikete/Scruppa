namespace Scruppa.Scrappers
{
    public interface IAlertConfiguration
    {
        bool Fired(ScrapperResults result);
        string GetDescription();
    }
}