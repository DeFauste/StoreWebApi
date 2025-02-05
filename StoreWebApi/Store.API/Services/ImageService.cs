using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ImageService
    {
        IImageRepository _repository;
        IMapper _mapper;
        IProductRepository _productRepository;
        public ImageService(IImageRepository repository, IProductRepository productRepository ,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public bool CanConnection()
        {
            return _repository.CanConnection();
        }

        public ActionResult Create(byte[] image, Guid idProduct)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (idProduct == Guid.Empty || image == null)
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };

            var productEntity = _productRepository.FindById(idProduct);
            if (productEntity == null)
                return new BadRequestObjectResult("Product not exist")
                { StatusCode = StatusCodes.Status404NotFound};

            if(productEntity.ImageId != Guid.Empty)
                return new BadRequestObjectResult("Image exist, using update")
                { StatusCode = StatusCodes.Status400BadRequest };

            var entity = new ImagesEntity { Id = Guid.Empty, Image = image };
            var created = _repository.Add(entity);
            var dto = _mapper.Map<ImagesEntity, ImageDTO>(created);

            productEntity.ImageId = dto.Id;
            _productRepository.Update(productEntity, 0);

            return new OkObjectResult(dto);
        }
        public ActionResult<ImageDTO> FindById(Guid id)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var entity = _repository.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var dto = _mapper.Map<ImagesEntity, ImageDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK }; ;
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
            return new OkObjectResult($"Image with Guid {id} deleted")
            { StatusCode = StatusCodes.Status200OK };
        }

        public ActionResult Update(Guid id, byte[] image)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var entity = _repository.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            _repository.Update(entity, image);
            var dto = _mapper.Map<ImagesEntity, ImageDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<ImageDTO> GetByIdProduct(Guid id)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var product = _productRepository.FindById(id);
            if (product == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var entity = _repository.FindById(product.ImageId);
            if(entity == null) entity = new ImagesEntity();

            var dto = _mapper.Map<ImagesEntity, ImageDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK }; ;
        }
    }
}
