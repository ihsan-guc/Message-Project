using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Text;

namespace Message.Api.Core
{
    public interface IChangePasswordCode
    {
        string PasswordCodeCreate(string email);
        bool SendEmail(string emailtext, string codeePassword);
    }
    public class ChangePasswordCode : IChangePasswordCode
    {
        public ChangePasswordCode()
        {
        }
        public string PasswordCodeCreate(string email)
        {
            Random rastgele = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                int karakter = rastgele.Next(0, 20);
                sb.Append(karakter);
            }
            return sb.ToString();
        }

        [Obsolete]
        public bool SendEmail(string emailtext, string codeePassword)
        {
            try
            {
                var email = new MimeMessage();
                email.Subject = "Şifre Değiştirme";
                email.From.Add(new MailboxAddress("eysangucc"));
                email.To.Add(new MailboxAddress(Encoding.UTF8, "Şifre Yenileme", emailtext));

                email.Body = new TextPart("html")
                {
                    Text = "Şifrenin : " + codeePassword,
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("eysangucc@gmail.com", "42282127bey");
                    client.Send(email);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
