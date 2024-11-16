using System.Text.Json.Serialization;

namespace Domain.Common.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Roles
{
    Administrator,
    Recruiter
}