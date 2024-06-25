namespace Websocket;

public interface INotificationClient
{
    Task ReceiveMessage(string message);
    Task ReceiveBody(NotificationBody notificationBody);
}
