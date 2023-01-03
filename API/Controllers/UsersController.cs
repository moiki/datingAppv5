using System.Text.Json;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController: BaseApiController
{
    private readonly IUserRepository _user;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserRepository user, IMapper mapper, ILogger<UsersController> logger)
    {
        _user = user;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _user.GetMembersAsync();
        return Ok(users);

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetUser(int id)
    {
        var user = await _user.FindUserByIdAsync(id);
        if (user is null) return BadRequest(new {error = "User not found!"});
            // var jsonString = JsonSerializer.Serialize(user);
            // _logger.LogInformation("Person {FirstName}", jsonString);
        var result = _mapper.Map<MemberDto>(user);
        return Ok(result);

    }
}