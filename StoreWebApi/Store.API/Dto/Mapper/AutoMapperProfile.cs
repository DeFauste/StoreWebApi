using AutoMapper;
using Store.DataAccess.Postgress.Models;

namespace Store.API.Dto.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClientDto, ClientEntity>();
            CreateMap<ClientEntity, ClientDto>();
        }
    }
}
