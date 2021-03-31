using Microsoft.Extensions.Configuration;
using MKTFY.App.Repositories.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MKTFY.App.Helpers
{
    public class SendGridMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tony.onyeka@launchpadbyvog.com", "MKTFY TEST");
            var htmlContent = $"Please follow the instruction from <a href=https:www.vogdevelopers.com/registeration/email=`{toEmail}`> for the request";
            var plainTextContent = "Please clink on the link to reset your password";
            var to = new EmailAddress(toEmail); //"tony.onyeka@launchpadbyvog.com"
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg); // test the response at breakpoint
        }
    }
}
