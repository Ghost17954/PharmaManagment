using AutoMapper;
using PharmaManagment.Models;
using PharmaManagment.Models.Dtos;

namespace PharmaManagment.Profiles
{
    public class PharmaProfile:Profile
    {
        public PharmaProfile()
        {
            CreateMap<OrderDetails, OrderDetailDto>()
                .ReverseMap()
                .ForMember(p => p.CustomerId, x => x.Ignore())
                .ForMember(p => p.ProductId, x => x.Ignore())
                .ForMember(p=>p.Customer,x=>x.MapFrom(c=>c.Customer))
                .ForMember(p => p.Product, x => x.MapFrom(c => c.Product));
                
            CreateMap<Products, ProductDto>()
                .ReverseMap()
                .ForMember(x => x.OrderDetails, p => p.Ignore());

            CreateMap<Customer, CustomerDto>().ReverseMap()
                .ForMember(p=>p.UserPassword,x=>x.Ignore())
                .ForMember(p=>p.OrderDetails,x=>x.Ignore());

            CreateMap<OrderDetails,OrderDetailsCreateDto>().ReverseMap()
                .ForMember(x=>x.OrderId, p => p.Ignore())
                .ForMember(x=>x.TotalPrice,p=>p.Ignore())
                .ForMember(x=>x.Product,p=>p.Ignore())
                .ForMember(x=>x.Customer,p=>p.Ignore());

            CreateMap<OrderDetails, OrderDetailsUpdateDto>().ReverseMap()               
               .ForMember(x => x.TotalPrice, p => p.Ignore())
               .ForMember(x => x.Product, p => p.Ignore())
               .ForMember(x => x.Customer, p => p.Ignore());
        }
    }
}
