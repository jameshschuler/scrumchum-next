using Microsoft.AspNetCore.SignalR;
using Scrumchum.Models.Response;

namespace Scrumchum.Hubs;

// TODO: move create and join to RoomHub?
public class PlanningHub : Hub
{
    private static Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
    private Random _random = new Random();
    
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    // TODO: support user role (dev, qa, moderator, viewer)
    // TODO: V2: w support ability to create room at scheduled time?
    public async Task<HubResponse<CreateRoomResponse>> CreateRoom(CreateRoomRequest request)
    {
        try
        {
            var roomCode = _random.Next(0, 9999).ToString("D4");
            
            while (_rooms.ContainsKey(roomCode))
            {
                roomCode = _random.Next(0, 9999).ToString("D4");
            }

            var room = new Room
            {
                RoomCode = roomCode,
            };

            room.Users.Add(new User
            {
                ConnectionId = Context.ConnectionId,
                Name = request.Username,
            });

            _rooms[roomCode] = room;

            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
            
            // TODO: generate join link

            var response = new HubResponse<CreateRoomResponse>
            {
                Data = new CreateRoomResponse(room.RoomCode),
                Message = "Created Room",
                Success = true
            };

            return response;
        }
        catch (Exception e)
        {
            return new HubResponse<CreateRoomResponse>
            {
                Success = false
            };
        }
    }
    
    // TODO: Join Room method
    
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

public class CreateRoomRequest
{
    public string Username { get; set; }
}

public class Room
{
    public string RoomCode { get; set; }

    public List<User> Users { get; set; } =  new List<User>();
}

public class User
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
}