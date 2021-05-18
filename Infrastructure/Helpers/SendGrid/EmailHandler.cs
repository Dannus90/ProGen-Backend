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
        private readonly string _frontendBaseUrl;

        public EmailHandler(IOptions<SendGridConfig> sendGridConfig, IOptions<ProGenUrlConfig> progenUrlConfig)
        {
            _apiKey = sendGridConfig.Value.ApiKey;
            _frontendBaseUrl = progenUrlConfig.Value.FrontendBaseUrl;
        }
        
        public async Task SendResetPasswordEmail(string email, string token)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("progeninfomail@gmail.com", "ProGen Support");
            var to = new EmailAddress(email, "ProGen user");
            var subject = "Reset your password";
            var plainTextContent = "Follow thee link down below to reset your password";
            var htmlContent = "<strong> " + _frontendBaseUrl + token + "</strong>";
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            await client.SendEmailAsync(msg);
        }
    }
}