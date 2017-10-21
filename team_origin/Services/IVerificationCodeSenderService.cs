using System;
using System.Threading.Tasks;

namespace team_origin.Services
{
    public interface IVerificationCodeSenderService
    {
        Task<string> SendSmsAsync(String MobileNumber);
    }
}
