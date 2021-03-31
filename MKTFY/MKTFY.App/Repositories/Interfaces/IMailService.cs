using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
