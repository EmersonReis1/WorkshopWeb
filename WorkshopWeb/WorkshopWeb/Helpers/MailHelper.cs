using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace WorkshopWeb.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BodyMailConfirmation(string link, string clientName, string url)
        {
            

            return $"<div style='width: 500px; height: 800px; margin: auto; font - family: Georgia, serif;'><div style='background - color: black; height: 60px; margin - bottom: 10px;'><img src='{url}images/logo-white.png' style='width: 250px; margin - left: 10px;'></div><div ><h1>Hi {clientName},</h1><p style='font - size: x - large; '>Welcome to ER.Auto. Please click on the button below to confirm your email address and activate your account.</p></div><a  href =\'{link}' style='color: rgb(255, 255, 255); font-size: 24px;line-height: 24px; padding: 6px;border-radius: 12px; font-weight: normal;text-decoration: none; font-style: normal;font-variant: normal;text-transform: none;background-color: black;display: inline-block; padding: 12px;margin-left: 98px;'>Click here to Confirm Now</a><div style=' width: 500px; height: 50px;background-color: black;margin-top: 100px;'><p style='color:white; padding: 10px 0 0 15px;'>Copyright © ER.Auto Inc.</p></div></div>";


        }

        public void SendMail(string to, string subject, string body)
        {
            var nameFrom = _configuration["Mail:NameFrom"];
            var from = _configuration["Mail:From"];
            var smtp = _configuration["Mail:Smtp"];
            var port = _configuration["Mail:Port"];
            var password = _configuration["Mail:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(nameFrom, from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodybuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {

                client.CheckCertificateRevocation = false;
                client.Connect(smtp, int.Parse(port), false);
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);

            }
        }
    }
}
