using Notification.NotificationType;

namespace BusinessLayer;

public class Message
{
    public string Content { get; init; }
    public string Receiver { get; init; }
    public NotificationTypeEnum NotificationType { get; init; }
}