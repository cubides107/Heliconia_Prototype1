using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.Shared
{
    public abstract class UserCommandShared
    {
        public List<Claim> claims { get; set; }

        public DataUserEdit dataUser { get; set; }

        public class DataUserEdit
        {
            public string id { get; set; }

            public string Name { get; set; }

            public string LastName { get; set; }

            public string Phone { get; set; }

            public string IdentificationDocument { get; set; }
        }
    }
}
