using Api.Controllers.Abstractions;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/cards")]
public class CardsController : BaseController
{
    private ICardService _cardService;


    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetCards()
    {
        return Ok(_cardService.GetAllCards());
    }

    [HttpGet("compare")]
    public async Task<IActionResult> GetComparesWorkers(int workerId1, int workerId2)
    {
        return Ok(await _cardService.GetCompareWorkers(workerId1, workerId2));
    }
}