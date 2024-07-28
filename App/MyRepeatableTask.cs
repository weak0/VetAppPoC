using BusinessLayer;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using Notification;

namespace App;

public class MyRepeatableTask : IInvocable
{
    private readonly ILogger<MyRepeatableTask> _logger;
    private readonly DbMock _db;
    private readonly ISender _sender;
    
    public MyRepeatableTask(ILogger<MyRepeatableTask> logger, DbMock db, ISender sender)
    {
        _logger = logger;
        _db = db;
        _sender = sender;
        
    }
    public async Task Invoke()
    {
        if (_db.Messages.Count == 0)
        {
            _logger.LogInformation("No messages to send");
            return;
        }
        else
        {
            foreach (var message in _db.Messages)
            {
                _sender.Send(message);
                _logger.LogInformation($"Message sent to {message.Receiver}");
                _db.Messages.Remove(message);
            }
        }
    }
}