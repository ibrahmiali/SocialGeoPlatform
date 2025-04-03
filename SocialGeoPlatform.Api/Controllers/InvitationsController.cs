using Microsoft.AspNetCore.Mvc;
using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Application.Mappers;
using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvitationsController : ControllerBase
{
    private readonly InvitationService _invitationService;

    public InvitationsController(InvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var invitations = await _invitationService.GetInvitationsByUserAsync(userId);
        var invitationDtos = invitations.Select(InvitationMapper.ToDto).ToList();
        return Ok(invitationDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var invitation = await _invitationService.GetInvitationByIdAsync(id);
        if (invitation == null) return NotFound();
        return Ok(InvitationMapper.ToDto(invitation));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InvitationDto invitationDto)
    {
        var invitation = new Invitation(invitationDto.EventId, invitationDto.SenderId, invitationDto.ReceiverId);
        var createdInvitation = await _invitationService.CreateInvitationAsync(invitation);
        return CreatedAtAction(nameof(GetById), new { id = createdInvitation.Id }, InvitationMapper.ToDto(createdInvitation));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] InvitationDto invitationDto)
    {
        var invitation = await _invitationService.GetInvitationByIdAsync(id);
        if (invitation == null) return NotFound();
        invitation.Status = invitationDto.Status;
        var updatedInvitation = await _invitationService.UpdateInvitationAsync(invitation);
        return Ok(InvitationMapper.ToDto(updatedInvitation));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var invitation = await _invitationService.GetInvitationByIdAsync(id);
        if (invitation == null) return NotFound();
        await _invitationService.DeleteInvitationAsync(id);
        return NoContent();
    }
}