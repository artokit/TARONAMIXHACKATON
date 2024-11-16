using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Contracts.Workers.Requests;

public class UpdateWorkerRequestDto
{
    [Required(ErrorMessage = "Укажите Id работника")]
    public int Id { get; set; }
    
    [EmailAddress(ErrorMessage = "Введите Email")]
    public string? Email { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public bool? IsLeader { get; set; }
    public bool? IsCandidate { get; set; }
    public int? UnitId { get; set; }
    public DateTime? Birthday { get; set; }
}