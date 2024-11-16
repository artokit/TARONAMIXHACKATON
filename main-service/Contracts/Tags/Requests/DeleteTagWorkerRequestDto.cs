namespace Contracts.Tags.Requests;

public class DeleteTagWorkerRequestDto
{
    public int WorkerId { get; set; }
    public int TagId { get; set; }
}