using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Common.Settings;

public class AuthOptions
{
    public string ISSUER { get; init; } = null;
    public string AUDIENCE { get; init; } = null;
    public string KEY { get; init; } = null;
    public const int ACCESS_LIFETINE_IN_MINUTE = 7 * 24 * 60;
    public const string SECTION_NAME = "AuthOptions";

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}