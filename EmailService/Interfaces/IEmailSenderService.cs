using FotballersAPI.Domain.Enums;

namespace EmailService.Interfaces
{
    public interface IEmailSenderService
    {
        Task<bool> SendAsync(string to, string subject, EmailTemplate emailTemplate, object model);
    }
}
