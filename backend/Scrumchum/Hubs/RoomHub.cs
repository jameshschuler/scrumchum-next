using Microsoft.AspNetCore.SignalR;
using Scrumchum.Models;
using Scrumchum.Models.Request;
using Scrumchum.Models.Response;

namespace Scrumchum.Hubs;

public class RoomHub : Hub
{
    private readonly Random _random = new ();

    public RoomHub()
    {
        if (!TempDb.Rooms.ContainsKey("1234"))
        {
            TempDb.Rooms.Add("1234", new Room
            {
                RoomCode = "1234",
                RoomName = "Test Room"
            });
        }
    }
    
    public async Task<HubResponse<RoomResponse>> CreateRoom(CreateRoomRequest request)
    {
        var response = new HubResponse<RoomResponse>();
        var validator = new CreateRoomRequestValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            response.ValidationErrors = result.Errors.Select(e => new ErrorResponse(e.PropertyName, e.ErrorMessage));
            return response;
        }
         
        var roomCode = _random.Next(0, 9999).ToString("D4");
        var user = new User
        {
            ConnectionId = Context.ConnectionId,
            Username = request.Username!,
            Role = request.Role!.Value,
            UserId = _random.Next(0, 9999),
        };
        var room = new Room
        {
            RoomCode = roomCode,
            RoomName = request.RoomName,
            Users = new List<User> { user }
        };
        TempDb.Rooms.Add(room.RoomCode, room);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        
        var usersJoinedResponse =
            new UserJoinedResponse(room.RoomCode, room.Users.Select(u => new UserResponse(u.Username, u.Role)));
        await Clients.Group(room.RoomCode).SendAsync("UserJoined", usersJoinedResponse);
        
        return new HubResponse<RoomResponse>
        {
            Data = new RoomResponse(roomCode, "TODO", request.RoomName, new UserResponse(user.Username, user.Role, user.UserId)),
        };
    }

    public async Task<HubResponse<RoomResponse>> JoinRoom(JoinRoomRequest request)
    {
        var response = new HubResponse<RoomResponse>();
        var validator = new JoinRoomRequestValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            response.ValidationErrors = result.Errors.Select(e => new ErrorResponse(e.PropertyName, e.ErrorMessage));
            return response;
        }
        
        if (!TempDb.Rooms.ContainsKey(request.RoomCode!))
        {
            response.ErrorMessage = $"Room not found with provided code [{request.RoomCode}]";
            return response;
        }

        var room = TempDb.Rooms[request.RoomCode!];
        // TODO: If user rejoins they'll have a different connection id
        if (room.Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId) != null)
        {
            response.ErrorMessage = $"You've already joined this room [{request.RoomCode}]";
            return response;
        }

        var user = new User
        {
            ConnectionId = Context.ConnectionId,
            Username = request.Username!, 
            UserId = _random.Next(0, 9999),
        };
        
        room.Users.Add(user);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, room.RoomCode);

        var usersJoinedResponse =
            new UserJoinedResponse(room.RoomCode, room.Users.Select(u => new UserResponse(u.Username, u.Role)));
        await Clients.Group(room.RoomCode).SendAsync("UserJoined", usersJoinedResponse);
        
        response.Data = new RoomResponse(room.RoomCode, "TODO", room.RoomName, new UserResponse(user.Username, user.Role, user.UserId));
        return response;
    }

    public async Task<HubResponse<RoomResponse>> RejoinRoom(RejoinRoomRequest request)
    {
        var response = new HubResponse<RoomResponse>();
        var validator = new RejoinRoomRequestValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            response.ValidationErrors = result.Errors.Select(e => new ErrorResponse(e.PropertyName, e.ErrorMessage));
            return response;
        }
        
        if (!TempDb.Rooms.ContainsKey(request.RoomCode!))
        {
            response.ErrorMessage = $"Room not found with provided code [{request.RoomCode}]";
            return response;
        }

        var room = TempDb.Rooms[request.RoomCode!];
        var user = room.Users.FirstOrDefault(u => u.UserId == request.UserId!);
        if (user == null)
        {
            response.ErrorMessage = $"User not found in room [{request.RoomCode}]";
            return response;
        }

        user.ConnectionId = Context.ConnectionId;
        response.Data = new RoomResponse(room.RoomCode, "TODO", room.RoomName, new UserResponse(user.Username, user.Role, user.UserId));

        return response;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Connected", new { Message = "Hello!" });
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}