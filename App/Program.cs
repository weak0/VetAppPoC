// See https://aka.ms/new-console-template for more information

using App;
using BusinessLayer;
using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification;
using Notification.NotificationType;

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

// var list = new List<Message> {msgE, msgS};
//
// foreach (var message in list)
// {
//     sender.Send(message);
// }

var builder = Host.CreateApplicationBuilder();

builder.Services.AddScheduler();
builder.Services.AddScoped<MyRepeatableTask>();
builder.Services.AddSingleton<DbMock>();
builder.Services.AddSingleton<ISender, Sender>(_ => new Sender(config));


var app = builder.Build();
app.Services.GetRequiredService<DbMock>().Messages = new List<Message>() {msgE, msgS};

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<MyRepeatableTask>().EverySeconds(5);
});

app.Run();