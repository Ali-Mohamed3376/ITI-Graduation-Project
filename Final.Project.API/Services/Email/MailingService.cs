using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;






namespace Final.Project.API;
public class MailingService : IMailingService
{
    private readonly MailSetting mailSetting;

    public MailingService(IOptions<MailSetting> _mailSetting)
    {
        this.mailSetting = _mailSetting.Value;
    }




    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        string fromMail = "lab847270@gmail.com";
        string fromPassword = "quhaueejlzwyekky";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = subject;
        message.Body = $"<html><body>{htmlMessage}</body></html>";
        message.IsBodyHtml = true;
        message.To.Add(email);

        var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };

        smtpClient.Send(message);
    }




    //public async Task SendEmailAsync(string mailTo, string subject, string body)
    //{
    //    var email = new MimeMessage
    //    {
    //        // Configure Sender
    //        Sender = MailboxAddress.Parse(mailSetting.Email),
    //        Subject = subject,
    //    };

    //    //  Configure reciver
    //    email.To.Add(MailboxAddress.Parse(mailTo));

    //    email.From.Add(new MailboxAddress(mailSetting.DisplayName, mailSetting.Email));
    //    using var smtp = new MailKit.Net.Smtp.SmtpClient();
    //    smtp.Connect(mailSetting.Host, mailSetting.Port, SecureSocketOptions.StartTls);

    //    smtp.Authenticate(mailSetting.Email, mailSetting.Password);
    //    await smtp.SendAsync(email);
    //    smtp.Disconnect(true);
    //}

}
