using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EnterTask.WebAPI.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "EnterTask.WebAPI";
        public const string AUDIENCE = "ApiSwagger";
        public const int LIFETIME = 60;

        public const string KEY = "23@#7f#hv200iv23em!2ink$lrbpo0!493mk??f%sd23";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
