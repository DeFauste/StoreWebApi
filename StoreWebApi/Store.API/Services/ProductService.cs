using AutoMapper;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ProductService
    {
        IProductRepository _repository;
        IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
