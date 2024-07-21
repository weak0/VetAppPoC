using BusinessLayer;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Notification.NotificationType.SMS;

public class SmsSender : ISender
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _sender;
    
    public SmsSender(string senderNumber,  string accountSid, string authToken)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _sender = senderNumber;
    }
    
    public void Send(Message msg)
    {
        try
        {
            TwilioClient.Init(_accountSid, _authToken);
            var messageOptions = new CreateMessageOptions(new PhoneNumber(msg.Receiver))
            {
                From = new PhoneNumber(_sender),
                Body = msg.Content
            };

            var finalMessage = MessageResource.Create(messageOptions);

            if (!string.IsNullOrEmpty(finalMessage.ErrorMessage))
            {
                throw new Exception($"Something Went wrong: {finalMessage.ErrorMessage}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}