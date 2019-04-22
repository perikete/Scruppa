using System;
using Microsoft.Extensions.Configuration;
using Scruppa.Scrappers;
using Scruppa.Scrappers.Logger;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Scruppa.ScrappersActions.Twilio
{
    public class TwilioSmsAction : IScrapperAction
    {
        private readonly Func<ScrapperResults, string> _transformResultsToHumanRedable;
        private readonly ILogger _logger;
        private readonly TwilioConfig _configuration;

        public TwilioSmsAction(Func<ScrapperResults, string> transformResultsToHumanRedable, IConfiguration configuration, ILogger logger)
        {
            _transformResultsToHumanRedable = transformResultsToHumanRedable;
            _logger = logger;
            _configuration = configuration.
                GetSection("TwilioSms").
                Get<TwilioConfig>();
        }

        public void RunAction(ScrapperResults results)
        {
            // no configuration, do nothing
            if (_configuration == null) return;

            TwilioClient.Init(_configuration.AccountSid, _configuration.AuthToken);

            var message = MessageResource.Create(
                body: _transformResultsToHumanRedable(results),
                from: new PhoneNumber(_configuration.FromNumber),
                to: new PhoneNumber(_configuration.ToNumber)
            );

            _logger.Log($"Twilio SMS Action: Sending SMS to {_configuration.ToNumber} because alert fired");
        }
    }
}