using AutoMapper;
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
        public void Create(byte[] image, Guid idProduct)
        {
            var entity = new ImagesEntity { Id = Guid.Empty, Image = image };
            var productEntity = _productRepository.FindById(idProduct);
            _repository.Add(entity);
        }

    }
}
