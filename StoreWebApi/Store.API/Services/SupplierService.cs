using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class SupplierService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository repository, IAddressRepository addressRepository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }
        public bool CanConnection()
        {
            return _repository.CanConnection();
        }
        public ActionResult<SupplierReadDTO> Create(SupplierCreateDTO suppCreateDto)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (suppCreateDto == null)
                return new BadRequestObjectResult("Object Supplier cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };

            try
            {
                var entity = _mapper.Map<SupplierEntiry>(suppCreateDto);
                _repository.Create(entity);
                _repository.SaveChange();
                var suppReadDto = _mapper.Map<SupplierReadDTO>(entity);

                return new OkObjectResult(suppReadDto)
                { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
        public ActionResult<SupplierReadDTO> UpdateAddress(Guid id, AddressCreateDTO address)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty || address == null)
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };

            var suppEntity = new SupplierEntiry { Id = id };

            var addressEntity = _mapper.Map<AddressEntity>(address);
            _addressRepository.Create(addressEntity);
            _addressRepository.SaveChange();

            _repository.Update(suppEntity, addressEntity);
            _repository.SaveChange();

            var suppReadDto = _mapper.Map<SupplierReadDTO>(FindById(id));

            return new OkObjectResult($"Client with Guid {id} updated")
            { StatusCode = StatusCodes.Status200OK, Value = suppReadDto };

        }
        public ActionResult<SupplierReadDTO> FindById(Guid id)
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

            var suppReadDto = _mapper.Map<SupplierReadDTO>(entity);

            return new OkObjectResult(suppReadDto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<IEnumerable<SupplierReadDTO>> FindAll()
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var listEntity = _repository.FindAll();
            var listDto = _mapper.Map<List<SupplierReadDTO>>(listEntity);
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
