# Scruppa

Simple playground project to create web scrappers and create custom configurations for alerts based on the scrap results.
Supports a scrapper runner to allow multiple scrapper runs with different configurations for each one and getting a
report at the end of the process.

# Setup/Run instructions

Just clone the project and run it. You might need to setup your twilio account details in the config.json or secrets.json file to get SMS notifications working.

`"TwilioSms": {
        "AccountSid": "YOUR_SID",
        "AuthToken": "YOUR_TOKEN",
        "FromNumber": "YOUR_FROM_NUMBER",
        "ToNumber": "YOUR_TO_NUMBER"
    }`