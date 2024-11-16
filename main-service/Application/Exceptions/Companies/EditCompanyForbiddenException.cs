using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Companies;

public class ActionWithCompanyForbiddenException(string message = "Вы не можете взаимодействовать с данной компанией") : ForbiddenRequestException(message);
