// See https://aka.ms/new-console-template for more information

using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.NotificationType;

Console.WriteLine("hello world");

var user = new User
{
    FirstName = "John",
    LastName = "Doe",
    Email = "maciej.gorzela89@gmail.com",
    PhoneNumber = "+48664935546",
    SmsAgreement = true,
    EmailAgreement = true
};

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var sender = new Sender(config);

var msgE = new Message
{
    Receiver = user.Email,
    Content = "Hello from Maciek",
    NotificationType = NotificationTypeEnum.Email
};

var msgS = new Message
{
    Receiver = user.PhoneNumber,
    Content = "Hello from Maciek",
    NotificationType = NotificationTypeEnum.Sms
};

var list = new List<Message> {msgE, msgS};

foreach (var message in list)
{
    sender.Send(message);
}