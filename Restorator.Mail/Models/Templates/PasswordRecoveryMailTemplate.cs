using Restorator.Mail.Models.Templates.Abstract;

namespace Restorator.Mail.Models.Templates
{
    public class PasswordRecoveryMailTemplate : MailTemplateBase
    {
        public string Username { get; }
        public string OTP { get; }
        public PasswordRecoveryMailTemplate(string email, string username, string otp) : base(nameof(PasswordRecoveryMailTemplate), "Сброс пароля", email)
        {
            Username = username;
            OTP = otp;
        }
        public override Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                {"@Username", Username},
                {"@OTP", OTP}
            };
        }
    }
}
