

using Stocky.Core.Domain.Admin;
using Stocky.Domain.DomainModels.Admin;
using Stocky.Model.Admin;

namespace Stocky.Configurations
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<ProductListModel, ProductList>().ReverseMap();
            CreateMap<ProductImageModel, ProductImage>().ReverseMap();
            CreateMap<CustomerModel, Customer>().ReverseMap();
            CreateMap<OrderModel, Order>().ReverseMap();
            CreateMap<OrderItemModel, OrderItem>().ReverseMap();
        }
    }
}
