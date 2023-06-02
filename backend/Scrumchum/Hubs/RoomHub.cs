using Microsoft.AspNetCore.SignalR;
using Scrumchum.Models;
using Scrumchum.Models.Request;
using Scrumchum.Models.Response;

namespace Scrumchum.Hubs;

public class RoomHub : Hub
{
    private Random _random = new Random();
    
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    // TODO: validate request
    public async Task<HubResponse<RoomResponse>> CreateRoom(CreateRoomRequest request)
    {
        var roomCode = _random.Next(0, 9999).ToString("D4");
        var room = new Room
        {
            RoomCode = roomCode,
            RoomName = request.RoomName,
            Users = new List<User>
            {
                new()
                {
                    ConnectionId = Context.ConnectionId,
                    Username = request.Username,
                    Role = request.Role,
                }
            }
        };
        TempDb.Rooms.Add(room.RoomCode, room);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        
        var usersJoinedResponse =
            new UserJoinedResponse(room.RoomCode, room.Users.Select(u => new UserResponse(u.Username, u.Role)));
        await Clients.Group(room.RoomCode).SendAsync("UserJoined", usersJoinedResponse);
        
        return new HubResponse<RoomResponse>
        {
            Data = new RoomResponse(roomCode, "TODO", request.RoomName, new UserResponse(request.Username, request.Role)),
        };
    }

    // TODO: validate request
    public async Task<HubResponse<RoomResponse>> JoinRoom(JoinRoomRequest request)
    {
        var response = new HubResponse<RoomResponse>();
        if (!TempDb.Rooms.ContainsKey(request.RoomCode))
        {
            response.ErrorMessage = $"Room not found with provided code [{request.RoomCode}]";
            return response;
        }

        var room = TempDb.Rooms[request.RoomCode];
        if (room.Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId) != null)
        {
            response.ErrorMessage = $"You've already joined this room [{request.RoomCode}]";
            return response;
        }

        var user = new User
        {
            ConnectionId = Context.ConnectionId,
            Username = request.Username,
        };
        
        room.Users.Add(user);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, room.RoomCode);

        var usersJoinedResponse =
            new UserJoinedResponse(room.RoomCode, room.Users.Select(u => new UserResponse(u.Username, u.Role)));
        await Clients.Group(room.RoomCode).SendAsync("UserJoined", usersJoinedResponse);
        
        response.Data = new RoomResponse(room.RoomCode, "TODO", room.RoomName, new UserResponse(request.Username, request.Role));
        return response;
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