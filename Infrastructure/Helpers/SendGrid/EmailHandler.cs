using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using API.helpers.SendGrid.Interfaces;
using Infrastructure.configurations;
using Microsoft.Extensions.Options;

namespace API.helpers.SendGrid
{
    public class EmailHandler : IEmailHandler
    {
        private readonly string _apiKey;

        public EmailHandler(IOptions<SendGridConfig> sendGridConfig)
        {
            _apiKey = sendGridConfig.Value.ApiKey;
        }
        
        public async Task SendResetPasswordEmail(string email)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("progeninfomail@gmail.com", "ProGen Support");
            var to = new EmailAddress(email, "ProGen user");
            var subject = "Reset your password";
            var plainTextContent = "Follow thee link down below to reset your password";
            var htmlContent = "<strong>LINK HERE!!</strong>";
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine(response.Body);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
        }
    }
}