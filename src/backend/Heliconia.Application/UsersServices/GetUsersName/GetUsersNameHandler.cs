using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.GetUsersName
{
    public class GetUsersNameHandler : IRequestHandler<GetUsersNameQuery, GetUsersNameDTO>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        private readonly IMapObject mapObject;

        public GetUsersNameHandler(IRepository repository, IUtility utility, ISecurity security, IMapObject mapObjet)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
            this.mapObject = mapObjet;
        }

        public async Task<GetUsersNameDTO> Handle(GetUsersNameQuery request, CancellationToken cancellationToken)
        {
            //Comprobar que el filtro de nombre a aplicar tenga como minimo 3 caracteres
            if (request.FilterName.Length < 3)
                throw new Exception("Se debe ingresar un nombre con minimo de 3 letras");

           //Inicializar el DTO y la lista a retornar
            GetUsersNameDTO getUsersNameDTO = new();
            getUsersNameDTO.ListUsers = new();

            Guard.Against.Null(request, nameof(request));

            if (Access.IsUserType<HeliconiaUser>(request.Claims, security))
            {
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);
                await AddListUser<HeliconiaUser>(request, getUsersNameDTO);
                await AddListUser<Manager>(request, getUsersNameDTO);
                await AddListUser<Worker>(request, getUsersNameDTO);
            }
            else if (Access.IsUserType<Manager>(request.Claims, security))
            {
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);
                await AddListUser<Manager>(request, getUsersNameDTO);
                await AddListUser<Worker>(request, getUsersNameDTO);
            }
            else if (Access.IsUserType<Worker>(request.Claims, security))
            {
                await Access.VerifyAccess<Worker>(request.Claims, repository, security, utility);
                await AddListUser<Worker>(request, getUsersNameDTO);
            }
            return getUsersNameDTO;
        }

        /// <summary>
        /// Obtiene los usuario de una tabla de Tipo T, mapea los usuarios y los agrega a la lista para retornar
        /// </summary>
        /// <typeparam name="T">Tipo de Usuarios a obtener</typeparam>
        /// <param name="request">peticion con datos necesarios tales como: el numero de paginas, el tamaño de cada pagina y el filtro para el nombre</param>
        /// <param name="dto"></param>
        private async Task AddListUser<T>(GetUsersNameQuery request, GetUsersNameDTO dto) where T : User
        {
            List<T> listUsers = new();
            listUsers = await repository.GetAll<T>(x => x.Name, request.Page, request.PageSize, x => x.Name.Contains(request.FilterName));
            dto.ListUsers.AddRange(mapObject.Map<List<T>, List<GetUsersNameDTO.UsersDTO>>(listUsers));
        }
    }
}
