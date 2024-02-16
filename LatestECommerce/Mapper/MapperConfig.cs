using AutoMapper;
using LatestECommerce.DbConfig;
using LatestECommerce.Models;

namespace LatestECommerce.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();

            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<CustomerModel, Customer>().ReverseMap();

            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<CategoryModel, Category>().ReverseMap();
        }
    }
}
