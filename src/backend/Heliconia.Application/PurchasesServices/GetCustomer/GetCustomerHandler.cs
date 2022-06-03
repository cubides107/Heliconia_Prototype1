using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.PurchasesEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.PurchasesServices.GetCustomer
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, GetCustomerDTO>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        private readonly IMapObject mapObject;

        public GetCustomerHandler(IRepository repository, ISecurity security, IUtility utility, IMapObject mapObject)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
            this.mapObject = mapObject;
        }

        public async Task<GetCustomerDTO> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            Customer customer;

            //Se verifica que el request no este nulo 
            Guard.Against.Null(request, nameof(request));

            //Verificar el acceso de los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Verificar que el usuario a consultar exista en la bd, si existe recuperarlo
            if (repository.Exists<Customer>(x => x.IdentificationDocument == request.CustomerDocument) is false)
                throw new Exception("El comprador no existe");

            customer = await repository.Get<Customer>(x => x.IdentificationDocument == request.CustomerDocument);

            //Mapear la entidad y retornar DTO
            return mapObject.Map<Customer, GetCustomerDTO>(customer);

        }
    }
}
