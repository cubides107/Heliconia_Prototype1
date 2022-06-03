using AutoMapper;
using Heliconia.Application.CategoriesServices.GetAllProducts;
using Heliconia.Application.CategoriesServices.GetChildCategories;
using Heliconia.Application.CategoriesServices.GetParentCategories;
using Heliconia.Application.CompaniesServices.GetCompany;
using Heliconia.Application.PurchasesServices.GeneratePurchaseReceipt;
using Heliconia.Application.PurchasesServices.GetCustomer;
using Heliconia.Application.StoresServices.GetAllStores;
using Heliconia.Application.StoresServices.GetStore;
using Heliconia.Application.UsersServices.GetUsersName;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.PurchasesEntities;
using Heliconia.Domain.UsersEntities;
using System.Collections.Generic;

namespace Heliconia.Infrastructure.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            this.CreateMap<Company, GetCompanyDTO>();
            this.CreateMap<HeliconiaUser, GetUsersNameDTO.UsersDTO>();
            this.CreateMap<Manager, GetUsersNameDTO.UsersDTO>();
            this.CreateMap<Worker, GetUsersNameDTO.UsersDTO>();
            this.CreateMap<Store, GetStoreDTO>();
            this.CreateMap<Store, GetAllStoresDTO>();
            this.CreateMap<Customer, GetCustomerDTO>();
            this.CreateMap<Category, GetParentCategoriesDTO>();
            this.CreateMap<Category, GetChildCategoriesDTO.CategoryDTO>();
            this.CreateMap<Customer, GeneratePurchaseReceiptDTO.CustomerDTO>();
            this.CreateMap<Purchase, GeneratePurchaseReceiptDTO.PurchaseDTO>();
            this.CreateMap<Product, GeneratePurchaseReceiptDTO.ProductDTO>();
            this.CreateMap<Product, GetAllProductsDTO>();
        }
    }
}
