using AutoMapper;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ClientService
    {
        private readonly IClientRepository _db;
        private readonly IMapper _mapepr;
        public ClientService(IClientRepository db, IMapper mapper)
        {
            _db = db;
            _mapepr = mapper;
        }


        public IEnumerable<ClientDTO> Get()
        {
            var listEntity = _db.FindAll();
            var listDto = _mapepr.Map<List<ClientEntity>, List<ClientDTO>>(listEntity);
            return listDto;
        }
        public void Create(ClientDTO clientDto)
        {
            var clientEntity = _mapepr.Map<ClientEntity>(clientDto);
            clientEntity.RegistrationDate = DateTime.UtcNow;
            _db.Add(clientEntity);
        }
    }
}
