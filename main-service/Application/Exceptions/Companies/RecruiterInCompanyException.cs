using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Companies;

public class RecruiterInCompanyException(string message = "Данный человек уже в вашей компании") : BadRequestException(message);