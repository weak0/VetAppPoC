using System.Net;
using System.Net.Mail;
using BusinessLayer;

namespace Notification;

public class EmailSender : ISender
{
    private readonly string _emailSender;
    private readonly string _password;
    private const string SmtpHost = "smtp.gmail.com";
    private const int SmtpPort = 587;

    public EmailSender(string emailSender, string password)
    {
        _emailSender = emailSender;
        _password = password;
    }
    
    public void Send(Message msg)
    {
        using var client = new SmtpClient(SmtpHost, SmtpPort);
        client.UseDefaultCredentials = false;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new NetworkCredential(_emailSender, _password);
        client.EnableSsl = true;
        try
        {
            var mail = new MailMessage(_emailSender, msg.Receiver)
            {
                Subject = "Maciek Test Email",
                Body = msg.Content
            };
            client.Send(mail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}