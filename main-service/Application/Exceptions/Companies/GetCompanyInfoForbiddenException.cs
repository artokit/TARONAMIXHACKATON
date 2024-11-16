using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Companies;

public class GetCompanyInfoForbiddenException(string message = "Вы не можете получить информацию о данной организации") : ForbiddenRequestException(message);