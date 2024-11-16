using Api.Controllers.Abstractions;
using Application.Common.Interfaces.Services;
using Contracts.Companies.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/companies")]
public class CompanyController : BaseController
{
    private ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    
    [HttpGet("{companyId:int}")]
    public async Task<IActionResult> GetCompanyById(int companyId)
    {
        return Ok(await _companyService.GetCompanyById(companyId));
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetCompanies()
    {
        // Todo: Получение всех компаний человека.
        return Ok(_companyService);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("invite_recruiter/{companyId:int}")]
    public async Task<IActionResult> InviteRecruiterInCompany(int userId, int companyId)
    {
        await _companyService.InviteRecruiter(UserId, userId, companyId);
        return Ok();
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpPost("")]
    public async Task<IActionResult> CreateCompany(CreateCompanyRequestDto requestDto)
    {
        return Ok(await _companyService.CreateCompany(requestDto, UserId));
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpPatch("{companyId}")]
    public async Task<IActionResult> UpdateCompany(int companyId, UpdateCompanyRequestDto requestDto)
    {
        var res = await _companyService.UpdateCompany(companyId, requestDto, UserId);
        return Ok(res);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{companyId:int}")]
    public async Task<IActionResult> DeleteCompany(int companyId)
    {
        await _companyService.DeleteCompany(companyId, UserId);
        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("delete_recruiter/{companyId:int}")]
    public async Task<IActionResult> DeleteRecruiter(int recruiterId, int companyId)
    {
        await _companyService.DeleteRecruiter(recruiterId, companyId, UserId);
        return NoContent();
    }
}