using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
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
        public bool CanConnection()
        {
            return _repository.CanConnection();
        }
        public void Create(ProductDTO dto)
        {
            var entity = _mapper.Map<ProductDTO,ProductEntity>(dto);
            _repository.Add(entity);
        }
        public void UpdateQuantities(Guid id, long decreases)
        {
            var entity = new ProductEntity { Id = id };
            _repository.Update(entity, decreases);
        }
        public ProductDTO? FindById(Guid id)
        {
            var entity = _repository.FindById(id);
            if (entity == null)
                return null;
            var dto = _mapper.Map<ProductEntity, ProductDTO>(entity);
            return dto;
        }
        public IEnumerable<ProductDTO> FindAll()
        {
            var listEntity = _repository.FindAll();
            var listDto = _mapper.Map<List<ProductEntity>, List<ProductDTO>>(listEntity);
            return listDto;
        }

        public void Delete(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
