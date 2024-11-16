using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Companies;

public class CompanyNotFoundException(string message = "Компании с даннам id не существует") : NotFoundException(message);