using Microsoft.AspNetCore.SignalR;
using Scrumchum.Models;
using Scrumchum.Models.Request;

namespace Scrumchum.Hubs;

public class RoomHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinRoom(JoinRoomRequest request)
    {
        if (!TempDb.Rooms.ContainsKey(request.RoomCode))
        {
            return;
        }

        var room = TempDb.Rooms[request.RoomCode];
        var user = room.Users.FirstOrDefault(u => u.Name == request.Name);

        if (user == null)
        {
            return;
        }

        user.ConnectionId = Context.ConnectionId;
        
        await Groups.AddToGroupAsync(Context.ConnectionId, request.RoomCode);
        await Clients.Caller.SendAsync("JoinedRoom");
    }
    
    public override async Task OnConnectedAsync()
    {
        //await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
        await Clients.Caller.SendAsync("Connected", new { Message = "Hello!" });
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}