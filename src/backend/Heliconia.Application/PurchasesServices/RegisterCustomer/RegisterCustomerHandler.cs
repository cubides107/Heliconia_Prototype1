using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.PurchasesEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.PurchasesServices.RegisterCustomer
{
    public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public RegisterCustomerHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {

            //Se verifica que el request no este nulo 
            Guard.Against.Null(request, nameof(request));

            //Verificar el acceso de los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Verificar si el comprador ya esta registrado con el numero de documento
            if (repository.Exists<Customer>(x => x.IdentificationDocument == request.Customer.IdentificationDocument))
                throw new Exception("El comprador ya se encuentra registrado");

            //Crear y gurdar el comprador en la bd
            await repository.Save<Customer>(Customer.Build(name: request.Customer.Name,
                lastName: request.Customer.LastName,
                identificationDocument:request.Customer.IdentificationDocument));
            await repository.Commit();

            return 0;
        }
    }
}
