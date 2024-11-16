using Api.Controllers.Abstractions;
using Application.Common.Interfaces.Services;
using Contracts.Units.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/units")]
public class UnitController : BaseController
{
    private IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUnit(CreateUnitRequestDto requestDto)
    {
        return Ok(await _unitService.CreateUnit(UserId, requestDto));
    }

    [HttpPatch("")]
    public async Task<IActionResult> UpdateUnit(UpdateUnitRequestDto requestDto)
    {
        return Ok(await _unitService.UpdateUnit(UserId, requestDto));
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteUnit(int unitId)
    {
        await _unitService.DeleteUnit(UserId, unitId);
        return NoContent();
    }

    [HttpGet("{unitId:int}")]
    public async Task<IActionResult> GetUnit(int unitId)
    {
        return Ok(await _unitService.GetUnitById(UserId, unitId));
    }
    
    [HttpGet("{unitId:int}/workers")]
    public async Task<IActionResult> GetUnitWorkers(int unitId)
    {
        return Ok(await _unitService.GetUnitWorkers(UserId, unitId));
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetUnits(int companyId)
    {
        return Ok(await _unitService.GetCompanyUnits(UserId, companyId));
    }
}