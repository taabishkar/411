using System;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace team_origin.Services
{
    public class VerificationCodeSenderService : IVerificationCodeSenderService
    {
        public  async Task<string> SendSmsAsync(string MobileNumber)
        {
            var s = "0123456789";
            var random = new Random();
            var verificationCode = new string(
                Enumerable.Range(1, 6).Select(i => s[random.Next(s.Length)]).ToArray());

            var accountSid = "AC9f094d3eebd333bcfe264277d1cdc702";

            var authToken = "842c7f46be5446fa8d8a202b25733678";

            TwilioClient.Init(accountSid, authToken);

            await MessageResource.CreateAsync(
             to: new PhoneNumber(MobileNumber),
             from: new PhoneNumber("+1 225-304-5610"),
             body: string.Format("Your Friends Tracker verification code is {0}.", verificationCode)
             );

            return verificationCode;
        }
    }
}
