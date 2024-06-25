using Microsoft.AspNetCore.SignalR;

namespace Websocket;

public sealed class NotificationHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
    }
}
