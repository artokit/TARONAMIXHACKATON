using Api.Controllers.Abstractions;
using Application.Common.Interfaces.Services;
using Contracts.Workers.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Route("api/workers")]
[ApiController]
public class WorkerController : BaseController
{
    private IWorkerService _workerService;

    public WorkerController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpPost("")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> AddWorker(AddWorkerRequestDto addWorkerRequestDto)
    {
        return Ok(await _workerService.AddWorker(addWorkerRequestDto, UserId));
    }
    
    [HttpGet("")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> GetCompanyWorkers(int companyId)
    {
        return Ok(await _workerService.GetCompanyWorkers(companyId, UserId));
    }
    
    [HttpGet("worker/{workerId:int}")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> GetWorker(int workerId)
    {
        return Ok(await _workerService.GetWorkerById(workerId, UserId));
    }
    
    [HttpPatch("")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> UpdateWorker(UpdateWorkerRequestDto updateWorkerRequestDto)
    {
        return Ok(await _workerService.UpdateWorker(updateWorkerRequestDto, UserId));
    }
    
    [HttpDelete("")]
    [Authorize(Roles="Recruiter,Administrator")]
    public async Task<IActionResult> DeleteWorker(int workerId)
    {
        await _workerService.DeleteWorkerById(workerId, UserId);
        return NoContent();
    }
}