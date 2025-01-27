using AutoMapper;
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
    }
}
