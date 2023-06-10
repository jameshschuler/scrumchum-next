using FluentValidation;
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
                RoomName = "Test Room",
            });
        }
    }
    
    // TODO: host can make other users host
    // TODO: host is responsible for starting the session and end votings / starting next vote
    // TODO: host can kick people from room
    // TODO: what happens if host disconnects? Just make next user host?
    public async Task<HubResponse<RoomResponse>> CreateRoom(CreateRoomRequest request)
    {
        var response = new HubResponse<RoomResponse>();
        var validationErrors = await ValidateRequest(request, new CreateRoomRequestValidator());
        if (validationErrors is not null && validationErrors.Any())
        {
            response.ValidationErrors = validationErrors;
            return response;
        }
         
        var roomCode = _random.Next(0, 9999).ToString("D4");
        var user = new User
        {
            ConnectionId = Context.ConnectionId,
            Username = request.Username!,
            Role = request.Role!.Value,
            UserId = Guid.NewGuid(),
            IsHost = true
        };
        var room = new Room
        {
            RoomCode = roomCode,
            RoomName = request.RoomName,
            Users = new List<User> { user },
            HostUserId = user.UserId
        };
        TempDb.Rooms.Add(room.RoomCode, room);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        
        var usersJoinedResponse =
            new UserJoinedResponse(room.RoomCode, room.Users.Select(u => u.ToUserResponse()));
        await Clients.Group(room.RoomCode).SendAsync("UserJoined", usersJoinedResponse);
        
        return new HubResponse<RoomResponse>
        {
            Data = new RoomResponse(room.HostUserId, roomCode, "TODO", request.RoomName, user.ToUserResponse()),
        };
    }

    public async Task<HubResponse<RoomResponse>> JoinRoom(JoinRoomRequest request)
    {
        var response = new HubResponse<RoomResponse>();
        var validationErrors = await ValidateRequest(request, new JoinRoomRequestValidator());
        if (validationErrors is not null && validationErrors.Any())
        {
            response.ValidationErrors = validationErrors;
            return response;
        }
        
        if (!TempDb.Rooms.ContainsKey(request.RoomCode!))
        {
            response.ErrorMessage = $"Room not found with provided code [{request.RoomCode}]";
            return response;
        }

        var room = TempDb.Rooms[request.RoomCode!];
        if (room.Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId) != null)
        {
            response.ErrorMessage = $"You've already joined this room [{request.RoomCode}]";
            return response;
        }

        var user = new User
        {
            ConnectionId = Context.ConnectionId,
            Username = request.Username!, 
            UserId = Guid.NewGuid(),
        };
        
        room.Users.Add(user);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, room.RoomCode);

        var usersJoinedResponse =
            new UserJoinedResponse(room.RoomCode, room.Users.Select(u => u.ToUserResponse()));
        await Clients.Group(room.RoomCode).SendAsync("UserJoined", usersJoinedResponse);
        
        response.Data = new RoomResponse(room.HostUserId, room.RoomCode, "TODO", room.RoomName, user.ToUserResponse());
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
        var user = room.Users.FirstOrDefault(u => u.UserId == Guid.Parse(request.UserId!));
        if (user == null)
        {
            response.ErrorMessage = $"User not found in room [{request.RoomCode}]";
            return response;
        }

        user.ConnectionId = Context.ConnectionId;
        response.Data = new RoomResponse(room.HostUserId, room.RoomCode, "TODO", room.RoomName, user.ToUserResponse());

        return response;
    }
    
    // TODO: close room (host only)
    
    public async Task<BaseResponse> LeaveRoom(LeaveRoomRequest request)
    {
        var response = new BaseResponse();

        var validationErrors = await ValidateRequest(request, new LeaveRoomRequestValidator());
        if (validationErrors is not null && validationErrors.Any())
        {
            response.ValidationErrors = validationErrors;
            return response;
        }
        
        if (!TempDb.Rooms.ContainsKey(request.RoomCode!))
        {
            response.ErrorMessage = $"Room not found with provided code [{request.RoomCode}]";
            return response;
        }
        
        var room = TempDb.Rooms[request.RoomCode!];
        var user = room.Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
        if (user == null)
        {
            response.ErrorMessage = $"User not found in room [{request.RoomCode}]";
            return response;
        }
        
        room.Users.Remove(user);
        return response;
    }

    private async Task<IList<ErrorResponse>?> ValidateRequest<TRequest>(TRequest request, AbstractValidator<TRequest> validator)
    {
        var result = await validator.ValidateAsync(request);
        return !result.IsValid ? result.Errors.Select(e => new ErrorResponse(e.PropertyName, e.ErrorMessage)).ToList() : null;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Connected", new { Message = "Hello!" });
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // TODO: update room list that user disconnected
        await base.OnDisconnectedAsync(exception);
    }
}