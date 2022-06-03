using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.GetTypeUser
{
    public class GetTypeUserHandler : IRequestHandler<GetTypeUserQuery, GetTypeUserDTO>
    {

        private readonly IRepository repository;

        public GetTypeUserHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetTypeUserDTO> Handle(GetTypeUserQuery request, CancellationToken cancellationToken)
        {
            GetTypeUserDTO typeUserDTO = new();

            if (CheckTypeUser<Worker>(typeUserDTO, request.Mail))
                return await Task.FromResult(typeUserDTO);
            else if (CheckTypeUser<Manager>(typeUserDTO, request.Mail))
                return await Task.FromResult(typeUserDTO);
            else if (CheckTypeUser<HeliconiaUser>(typeUserDTO, request.Mail))
                return await Task.FromResult(typeUserDTO);

            throw new Exception("El usuario no se encuentra registrado");

        }

        //Verifica si el usuario existe un tabla T de la base de datos
        private bool CheckTypeUser<T>(GetTypeUserDTO typeUserDTO, string mail) where T : User
        {
            if (repository.Exists<T>(x => x.Mail == mail))
            {
                typeUserDTO.TypeUser = typeof(T).Name;
                return true;
            }
            else
                return false;
        }
    }
}
