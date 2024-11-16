using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Workers;

public class CantAddWorkerToCompanyException(string message = "Вы не можете добавлять работника в данную компанию") : ForbiddenRequestException(message);
