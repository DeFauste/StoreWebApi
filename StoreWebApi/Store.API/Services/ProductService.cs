using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public bool CanConnection()
        {
            return _repository.CanConnection();
        }
        public ActionResult<ProductReadDTO> Create(ProductCreateDTO productCreateDto)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (productCreateDto == null)
                return new BadRequestObjectResult("Object cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };

           try
           {
                var productEntity = _mapper.Map<ProductEntity>(productCreateDto);
                _repository.Create(productEntity);
                _repository.SaveChange();

                var productReadDto = _mapper.Map<ProductReadDTO>(productEntity);
                return new OkObjectResult(productReadDto)
                { StatusCode = StatusCodes.Status201Created };
           }
           catch (Exception ex)
           {
               return new BadRequestObjectResult("Invalid data")
               { StatusCode = StatusCodes.Status400BadRequest };
           }
        }
        public ActionResult<ProductReadDTO> UpdateQuantities(Guid id, long decreases)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };
           
            if (id == Guid.Empty || decreases < 1) 
                return new BadRequestObjectResult("Object cannot be null or decreases < 1")
            { StatusCode = StatusCodes.Status400BadRequest };

            var entity = new ProductEntity { Id = id };

            _repository.Update(entity, decreases);
            _repository.SaveChange();

            var productReadDto = _mapper.Map<ProductReadDTO>(_repository.FindById(id));

            return new OkObjectResult($"Product {id} uodated")
            { StatusCode = StatusCodes.Status200OK, Value = productReadDto };
        }
        public ActionResult<ProductReadDTO> FindById(Guid id)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty)
                return new BadRequestObjectResult("Object Guid cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };

            var entity = _repository.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var productReadDto = _mapper.Map<ProductReadDTO>(entity);
            return new OkObjectResult(productReadDto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<IEnumerable<ProductReadDTO>> FindAll()
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var listEntity = _repository.FindAll();
            var listReadDto = _mapper.Map<List<ProductReadDTO>>(listEntity);

            return new OkObjectResult(listReadDto)
            { StatusCode = StatusCodes.Status200OK };
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
