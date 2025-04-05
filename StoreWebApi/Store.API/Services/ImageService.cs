using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ImageService
    {
        IImageRepository _repositoryImage;
        IMapper _mapper;
        IProductRepository _productRepository;
        public ImageService(IImageRepository repositoryImage, IProductRepository productRepository, IMapper mapper)
        {
            _repositoryImage = repositoryImage;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public bool CanConnection()
        {
            return _repositoryImage.CanConnection();
        }

        public ActionResult<ImageReadDTO> Create(byte[] image, Guid idProduct)
        {
            if (_repositoryImage.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (idProduct == Guid.Empty || image == null)
                return new BadRequestObjectResult("Invalid data Guid or image is null")
                { StatusCode = StatusCodes.Status400BadRequest };

            var productEntity = _productRepository.FindById(idProduct);
            if (productEntity == null)
                return new BadRequestObjectResult($"Product Guid {idProduct} not exist")
                { StatusCode = StatusCodes.Status404NotFound };

            if (productEntity.ImageId != Guid.Empty)
                return new BadRequestObjectResult("Image exist, using update")
                { StatusCode = StatusCodes.Status400BadRequest };

            var entity = new ImagesEntity { Id = Guid.Empty, Image = image };
            _repositoryImage.Create(entity);
            _repositoryImage.SaveChange();

            var dto = _mapper.Map<ImageReadDTO>(entity);

            productEntity.ImageId = dto.Id;
            _productRepository.Update(productEntity, 0);
            _productRepository.SaveChange();

            return new OkObjectResult(dto) { StatusCode = StatusCodes.Status201Created };
        }
        public ActionResult<ImageReadDTO> FindById(Guid id)
        {
            if (_repositoryImage.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var entity = _repositoryImage.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var dto = _mapper.Map<ImageReadDTO>(entity);

            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK }; ;
        }
        public ActionResult Delete(Guid id)
        {
            if (_repositoryImage.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty)
                return new ObjectResult($"The object with the Guid {id} was not exist")
                { StatusCode = StatusCodes.Status404NotFound };

            _repositoryImage.DeleteById(id);
            return new OkObjectResult($"Image with Guid {id} deleted")
            { StatusCode = StatusCodes.Status200OK };
        }

        public ActionResult<ImageReadDTO> Update(Guid id, byte[] image)
        {
            if (_repositoryImage.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var entity = _repositoryImage.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            _repositoryImage.Update(entity, image);
            var dto = _mapper.Map<ImageReadDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<ImageReadDTO> GetByIdProduct(Guid id)
        {
            if (_repositoryImage.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var product = _productRepository.FindById(id);
            if (product == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var entity = _repositoryImage.FindById(product.ImageId);
            if (entity == null) entity = new ImagesEntity();

            var dto = _mapper.Map<ImageReadDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK }; ;
        }
    }
}
