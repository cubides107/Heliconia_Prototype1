using MediatR;

namespace Heliconia.Application.UsersServices.LoginWorker
{
    public class LoginWorkerCommand : IRequest<string>
    {
        public string Mail { get; set; }

        public string Password { get; set; }
    }
}
