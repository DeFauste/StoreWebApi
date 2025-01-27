using AutoMapper;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ImageService
    {
        IImageRepository _repository;
        IMapper _mapper;
        public ImageService(IImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
