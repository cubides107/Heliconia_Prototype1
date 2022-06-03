using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain
{
    public interface ISecurity
    {
        public const string MAIL = "mail";

        public const string USERNAME = "Username";

        public const string USERID = "UserId";

        public const string JTI = "jti";

        public const string ROLE = "Userrole";

        public string CreateToken(string id, string name, string mail, string role);

        public string EncryptPassword(string password);

        public string GetClaim(string token, string claimType);

        public string GetClaim(List<Claim> claims, string claimType);
    }
}
