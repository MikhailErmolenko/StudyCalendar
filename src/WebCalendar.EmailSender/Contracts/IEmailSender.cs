using System.Threading.Tasks;
using WebCalendar.EmailSender.Models;

namespace WebCalendar.EmailSender.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}