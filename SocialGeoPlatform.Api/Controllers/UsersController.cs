using Microsoft.AspNetCore.Mvc;
using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Application.Mappers;
using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        var userDtos = users.Select(UserMapper.ToDto).ToList();
        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(UserMapper.ToDto(user));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        var user = new User(userDto.Username, userDto.Email);
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, UserMapper.ToDto(createdUser));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserDto userDto)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        user.Username = userDto.Username;
        user.Email = userDto.Email;
        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(UserMapper.ToDto(updatedUser));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}