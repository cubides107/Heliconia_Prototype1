﻿using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.UsersServices.RemoveUser
{
    public class RemoveUserCommand: IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public string Id { get; set; } 
    }
}
