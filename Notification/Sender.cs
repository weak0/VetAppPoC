using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Notification.NotificationType;
using Notification.NotificationType.SMS;

namespace Notification;

public class Sender : ISender
{
    private readonly IConfiguration _config;

    public Sender(IConfiguration config)
    {
        _config = config;
    }
    
    public void Send( Message msg)
    {
        switch (msg.NotificationType)
        {
            case NotificationTypeEnum.Email:
                new EmailSender(_config["Email:Sender"]!, _config["Email:Password"]!).Send(msg);
                break;
            case NotificationTypeEnum.Sms:
                new SmsSender(_config["Twilio:Sender"]!, _config["Twilio:Sid"]!, _config["Twilio:Token"]!).Send(msg);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}