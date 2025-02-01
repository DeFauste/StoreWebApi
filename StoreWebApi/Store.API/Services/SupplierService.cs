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
        public ActionResult Create(SupplierDTO dto)
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
                var entity = _mapper.Map<SupplierDTO, SupplierEntiry>(dto);
                var created = _repository.Add(entity);
                dto = _mapper.Map<SupplierEntiry, SupplierDTO>(created);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };
            }
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK };

        }
        public ActionResult UpdateAddress(Guid id, AddressDTO address)
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty || address == null)
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };

            var entity = new SupplierEntiry { Id = id };
            address.Id = Guid.Empty;
            var addressEntity = _mapper.Map<AddressDTO, AddressEntity>(address);
            var createdAddress = _addressRepository.Add(addressEntity);
            _repository.Update(entity, addressEntity);

            return new OkObjectResult($"Client with Guid {id} updated")
            { StatusCode = StatusCodes.Status200OK };

        }
        public ActionResult<SupplierDTO> FindById(Guid id)
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

            var dto = _mapper.Map<SupplierEntiry, SupplierDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<IEnumerable<SupplierDTO>> FindAll()
        {
            if (_repository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var listEntity = _repository.FindAll();
            var listDto = _mapper.Map<List<SupplierEntiry>, List<SupplierDTO>>(listEntity);
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
