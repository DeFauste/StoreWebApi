using AutoMapper;
using Store.DataAccess.Postgress.Models;

namespace Store.API.Dto.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClientDTO, ClientEntity>();
            CreateMap<ClientEntity, ClientDTO>();
            CreateMap<ProductEntity, ProductDTO>();
            CreateMap<ProductDTO, ProductEntity>();
            CreateMap<SupplierDTO, SupplierEntiry>();
            CreateMap<SupplierEntiry, SupplierDTO>();
            CreateMap<AddressDTO, AddressEntity>();
            CreateMap<AddressEntity, AddressDTO>();
        }
    }
}
