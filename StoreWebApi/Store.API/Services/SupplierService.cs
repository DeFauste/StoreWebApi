using AutoMapper;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class SupplierService
    {
        ISupplierRepository _repository;
        IMapper _mapper;
        public SupplierService(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public bool CanConnection()
        {
            return _repository.CanConnection();
        }
        public void Create(SupplierDTO dto)
        {
            var entity = _mapper.Map<SupplierDTO, SupplierEntiry>(dto);
            _repository.Add(entity);
        }
        public void UpdateAddress(Guid id, AddressEntity address)
        {
            var entity = new SupplierEntiry { Id = id };
            _repository.Update(entity, address);
        }
        public SupplierDTO? FindById(Guid id)
        {
            var entity = _repository.FindById(id);
            if (entity == null)
                return null;
            var dto = _mapper.Map<SupplierEntiry, SupplierDTO>(entity);
            return dto;
        }
        public IEnumerable<SupplierDTO> FindAll()
        {
            var listEntity = _repository.FindAll();
            var listDto = _mapper.Map<List<SupplierEntiry>, List<SupplierDTO>>(listEntity);
            return listDto;
        }

        public void Delete(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
