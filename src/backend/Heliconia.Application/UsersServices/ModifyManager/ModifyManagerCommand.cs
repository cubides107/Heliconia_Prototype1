using Heliconia.Application.Shared;
using MediatR;

namespace Heliconia.Application.UsersServices.ModifyManager
{
    public class ModifyManagerCommand: UserCommandShared, IRequest<int>
    {
    }
}
