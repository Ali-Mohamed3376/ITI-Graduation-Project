namespace Final.Project.API;
public interface IMailingService
{
    Task<bool> SendEmailAsync(string email, string subject, string body);
}
