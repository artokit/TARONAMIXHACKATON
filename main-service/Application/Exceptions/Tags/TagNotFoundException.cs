using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Tags;

public class TagNotFoundException(string message = "Данного тэга нету либо он отсутвует у работника") : NotFoundException(message);