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

    public UsersController(IUserRepository user, IMapper mapper)
    {
        _user = user;
        _mapper = mapper;
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
        var result = _mapper.Map<MemberDto>(user);
        return Ok(result);

    }
}