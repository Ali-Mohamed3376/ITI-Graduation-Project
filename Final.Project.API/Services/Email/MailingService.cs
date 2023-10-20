using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;
using Final.Project.BL;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Final.Project.API;

public class MailingService : IMailingService
{
    private readonly MailSetting mailSetting;

    public MailingService(IOptions<MailSetting> _mailSetting)
    {
        this.mailSetting = _mailSetting.Value;
    }
    public async Task<bool> SendEmailAsync(string email, string subject, string body)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(mailSetting.Email);
        message.Subject = subject;
        message.Body = $"<html><body>{body}</body></html>";
        message.IsBodyHtml = true;
        message.To.Add(email);

        var smtpClient = new System.Net.Mail.SmtpClient(mailSetting.Host)
        {
            Port = mailSetting.Port,
            Credentials = new NetworkCredential(mailSetting.Email, mailSetting.Password),
            EnableSsl = mailSetting.EnableSsl,
        };

        try
        {
            await smtpClient.SendMailAsync(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }


}
