using HealthMedScheduler.Application.ViewModel;

namespace HealthMedScheduler.Application.Interfaces.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailViewModel emailViewModel);
    }
}
