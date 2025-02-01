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
        public ActionResult Create(ProductDTO dto)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (dto == null)
                return new BadRequestObjectResult("Object cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };

            try
            {
                dto.Id = Guid.Empty;
                var entity = _mapper.Map<ProductDTO, ProductEntity>(dto);
                _repository.Add(entity);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };
            }
            return new OkObjectResult("New product added")
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult UpdateQuantities(Guid id, long decreases)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };
           
            if (id == Guid.Empty || decreases < 1) 
                return new BadRequestObjectResult("Object cannot be null")
            { StatusCode = StatusCodes.Status400BadRequest };

            var entity = new ProductEntity { Id = id };

            _repository.Update(entity, decreases);
            return new OkObjectResult("New product added")
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<ProductDTO> FindById(Guid id)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty)
                return new BadRequestObjectResult("Object cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };

            var entity = _repository.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var dto = _mapper.Map<ProductEntity, ProductDTO>(entity);
            return dto;
        }
        public ActionResult<IEnumerable<ProductDTO>> FindAll()
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var listEntity = _repository.FindAll();
            var listDto = _mapper.Map<List<ProductEntity>, List<ProductDTO>>(listEntity);
            return listDto;
        }

        public ActionResult Delete(Guid id)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty)
                return new ObjectResult($"The object with the Guid {id} was not exist")
                { StatusCode = StatusCodes.Status404NotFound };

            _repository.DeleteById(id);

            return new OkObjectResult($"Client with Guid {id} deleted")
            { StatusCode = StatusCodes.Status200OK };
        }
    }
}
