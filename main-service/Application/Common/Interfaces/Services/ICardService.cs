using Application.Services;
using Contracts.Cards.Responses;
using Domain.Entities.Cards;

namespace Application.Common.Interfaces.Services;

public interface ICardService
{
    public List<TarotCard> GetAllCards();
    public Task<GenerateCompareWorkersResponseDto> GetCompareWorkers(int workerId1, int workerId2);
}