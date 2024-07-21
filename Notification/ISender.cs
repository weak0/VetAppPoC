using BusinessLayer;

namespace Notification;

public interface ISender
{
    void Send(Message msg);
}