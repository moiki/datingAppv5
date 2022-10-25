using API.Data;
using API.Data.Repositories;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController: BaseApiController
{
    private readonly UserRepository _user;

    public UsersController(UserRepository user)
    {
        _user = user;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return Ok(await _user.ListUsersAsync());

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return Ok(await _user.FindUserByIdAsync(id));

    }
}