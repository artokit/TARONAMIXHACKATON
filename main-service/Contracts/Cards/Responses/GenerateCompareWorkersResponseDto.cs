using Domain.Entities.Cards;

namespace Contracts.Cards.Responses;

public class GenerateCompareWorkersResponseDto
{
    public List<TarotCard> Cards { get; set; }
    public int ResultCompareHard { get; set; }
    public int MagicComparePercent { get; set; }
    public string MagicCompareText { get; set; }
}
