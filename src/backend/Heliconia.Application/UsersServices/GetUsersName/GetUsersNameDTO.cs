using System.Collections.Generic;

namespace Heliconia.Application.UsersServices.GetUsersName
{
    public class GetUsersNameDTO
    {        
        public List<UsersDTO> ListUsers { get; set; }

        public class UsersDTO
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Lasname { get; set; }

            public string IdentificationDocument { get; set; }

            public string Mail { get; set; }

            public string CellPhoneNumber { get; set; }
        }
    }
}
