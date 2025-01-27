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
        
        public bool CanConnection() => _db.CanConnection();

        public IEnumerable<ClientDTO> FindAll()
        {
            var listEntity = _db.FindAll();
            var listDto = _mapepr.Map<List<ClientEntity>, List<ClientDTO>>(listEntity);
            return listDto;
        }
        public IEnumerable<ClientDTO> FindAll(int limit, int page)
        {
            List<ClientDTO> listDto;
            List<ClientEntity> listEntities;
            if(limit <= 0 || page <= 0)
            {
                listEntities = _db.FindAll();
            } else
            {
                listEntities = _db.FindAll(limit, page);
            }
            listDto = _mapepr.Map<List<ClientEntity>, List<ClientDTO>>(listEntities);
            return listDto;
        }

        public IEnumerable<ClientDTO> FindByNameAndSurname(string name, string surname)
        {
            List<ClientDTO> listDto;
            List<ClientEntity> listEntities;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                listEntities = new List<ClientEntity>();
            }
            else
            {
                listEntities = _db.FindClient(name, surname);
            }
            listDto = _mapepr.Map<List<ClientEntity>, List<ClientDTO>>(listEntities);
            return listDto;
        }
        public ClientDTO? FindById(Guid id)
        {
            var entity = _db.FindById(id);
            if (entity == null)
                return null;
            var dto = _mapepr.Map<ClientEntity, ClientDTO>(entity);
            return dto;
        }
        public void Create(ClientDTO clientDto)
        {
            var clientEntity = _mapepr.Map<ClientEntity>(clientDto);
            clientEntity.RegistrationDate = DateTime.UtcNow;
            _db.Add(clientEntity);
        }
        public void UpdateAddress(Guid id, AddressEntity address)
        {
            var clientEntity = new ClientEntity { Id = id }; 
            _db.Update(clientEntity, address);
        }
        public void Delete(Guid id)
        {
            _db.DeleteById(id);
        }

    }
}
