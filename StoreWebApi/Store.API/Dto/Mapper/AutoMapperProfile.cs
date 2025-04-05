using AutoMapper;
using Store.DataAccess.Postgress.Models;

namespace Store.API.Dto.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Source --> Target
            //Client
            CreateMap<ClientEntity, ClientReadDTO>();
            CreateMap<ClientCreateDTO, ClientEntity>();
            //Address
            CreateMap<AddressEntity, AddressReadDTO>();
            CreateMap<AddressCreateDTO,AddressEntity>();
            //Image
            CreateMap<ImagesEntity, ImageReadDTO>();
            CreateMap<ImageCreateDTO,ImagesEntity>();
            //Product
            CreateMap<ProductEntity, ProductReadDTO>();
            CreateMap<ProductCreateDTO,ProductEntity>();
            //Supplier
            CreateMap<SupplierEntiry, SupplierReadDTO>();
            CreateMap<SupplierCreateDTO, SupplierEntiry>();

        }
    }
}
