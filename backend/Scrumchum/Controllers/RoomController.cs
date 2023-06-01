using Microsoft.AspNetCore.Mvc;
using Scrumchum.Models;
using Scrumchum.Models.Request;
using Scrumchum.Models.Response;

namespace Scrumchum.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private Random _random = new Random();

    [HttpPost]
    public ActionResult<BaseResponse> CreateRoom(CreateRoomRequest request)
    {
        var roomCode = _random.Next(0, 9999).ToString("D4");
        TempDb.Rooms.Add(roomCode, new Room
        {
            RoomCode = roomCode,
            Users = new List<User>
            {
                new ()
                {
                    Name = request.Name,
                    Role = request.Role
                }
            }
        });

        return Ok(new HubResponse<CreateRoomResponse>
        {
            Data = new CreateRoomResponse(request.Name, roomCode),
            Success = true
        });
    }
    
    [HttpPost]
    [Route("join")]
    public ActionResult JoinRoom()
    {
        return Ok();
    }
}