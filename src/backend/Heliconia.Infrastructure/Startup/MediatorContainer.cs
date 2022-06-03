using Heliconia.Application.AccountingServices.ManageLedger;
using Heliconia.Application.CategoriesServices.AddChildCategory;
using Heliconia.Application.CategoriesServices.CreateParentCategory;
using Heliconia.Application.CategoriesServices.ModifyCategory;
using Heliconia.Application.CategoriesServices.ModifyProduct;
using Heliconia.Application.CategoriesServices.RegisterProduct;
using Heliconia.Application.CategoriesServices.RemoveCategory;
using Heliconia.Application.CategoriesServices.RemoveProduct;
using Heliconia.Application.CompaniesServices.CreateCompany;
using Heliconia.Application.CompaniesServices.GetCompany;
using Heliconia.Application.CompaniesServices.ModifyBasicAttributes;
using Heliconia.Application.PurchasesServices.RegisterCustomer;
using Heliconia.Application.PurchasesServices.RegisterPurchase;
using Heliconia.Application.StoresServices.CreateStore;
using Heliconia.Application.StoresServices.GetAllStores;
using Heliconia.Application.StoresServices.GetStore;
using Heliconia.Application.StoresServices.ModifyStore;
using Heliconia.Application.StoresServices.RemoveStore;
using Heliconia.Application.UsersServices.GetUsersName;
using Heliconia.Application.UsersServices.LoginHeliconiaUser;
using Heliconia.Application.UsersServices.LoginManager;
using Heliconia.Application.UsersServices.LoginWorker;
using Heliconia.Application.UsersServices.ModifyHeliconiaUser;
using Heliconia.Application.UsersServices.ModifyManager;
using Heliconia.Application.UsersServices.ModifyWorker;
using Heliconia.Application.UsersServices.RegisterHeliconiaUser;
using Heliconia.Application.UsersServices.RegisterManager;
using Heliconia.Application.UsersServices.RegisterWorker;
using Heliconia.Application.UsersServices.RemoveUser;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Heliconia.Infrastructure.Startup
{
    class MediatorContainer
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginHeliconiaUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterHeliconiaUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCompanyCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateStoreCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterManagerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterWorkerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(LoginWorkerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(LoginManagerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetCompanyQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyManagerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyHeliconiaUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyWorkerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(AddChildCategoryCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUsersNameQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetStoreQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllStoresQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RemoveStoreCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RemoveUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyStoreCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateParentCategoryCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterCustomerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyCategoryCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyProductCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterProductCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RemoveCategoryCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RemoveProductCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterPurchaseCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ModifyBasicAttributesCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ManageLedgerCommand).GetTypeInfo().Assembly);

        }
    }
}
