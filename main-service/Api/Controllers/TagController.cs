using Api.Controllers.Abstractions;
using Application.Common.Interfaces.Services;
using Contracts.Tags.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/tags")]
public class TagController : BaseController
{
    private ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("{workerId:int}")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> GetTags(int workerId)
    {
        return Ok(await _tagService.GetWorkerTags(UserId, workerId));
    }
    
    [HttpPost("")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> AddTag(AddTagWorkerRequestDto requestDto)
    {
        return Ok(await _tagService.AddTag(UserId, requestDto));
    }
    
    [HttpDelete("")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> DeleteTag(DeleteTagWorkerRequestDto requestDto)
    {
        await _tagService.DeleteWorkerTag(UserId, requestDto);
        return NoContent();
    }
}