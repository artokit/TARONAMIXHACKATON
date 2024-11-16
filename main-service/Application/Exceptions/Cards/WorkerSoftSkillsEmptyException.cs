using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Cards;

public class WorkerSkillsEmptyException(string message) : BadRequestException(message);