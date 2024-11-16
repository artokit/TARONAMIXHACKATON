namespace Domain.Entities.Cards;

public class TarotCard
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GoodPercent { get; set; }
    public List<string> Meaning { get; set; }
    public string ImageUrl { get; set; } = "https://assets.stickpng.com/thumbs/5ca4c09f1cf23004f2836845.png";
}