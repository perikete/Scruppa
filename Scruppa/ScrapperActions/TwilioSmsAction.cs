using System;
using Scruppa.Scrappers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Scruppa.ScrappersActions
{
    public class TwilioSmsAction : IScrapperAction
    {
        private readonly Func<ScrapperResults, string> _transformResultsToHumanRedable;

        public TwilioSmsAction(Func<ScrapperResults, string> transformResultsToHumanRedable)
        {
            _transformResultsToHumanRedable = transformResultsToHumanRedable;
        }
        
        public void RunAction(ScrapperResults results)
        {

            const string accountSid = "AC766bf90201b1d2331d4cc6db0aa1fb54";
            const string authToken = "70e873ab08aeb7dab85bde5443975e9c";
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: _transformResultsToHumanRedable(results),
                from: new Twilio.Types.PhoneNumber("+18285284883"),
                to: new Twilio.Types.PhoneNumber("+640275421002")
            );
            Console.WriteLine("Sending SMS because alert fired");
        }
    }
}