using MediatR;

namespace Heliconia.Application.UsersServices.LoginManager
{
    public class LoginManagerCommand : IRequest<string>
    {
        public string Mail { get; set; }

        public string Password { get; set; }  
    }
}
