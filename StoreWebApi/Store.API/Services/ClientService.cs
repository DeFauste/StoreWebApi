using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository repository, IAddressRepository addressRepository, IMapper mapper)
        {
            _clientRepository = repository;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }
        
        public bool CanConnection() => _clientRepository.CanConnection();
        public ActionResult<ClientReadDTO> Create(ClientCreateDTO clientCreateDto)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database") 
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (clientCreateDto == null) 
                return new BadRequestObjectResult("Object cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };
            
            try
            {
                var clientEntity = _mapper.Map<ClientEntity>(clientCreateDto);
                clientEntity.RegistrationDate = DateTime.UtcNow;
                _clientRepository.Create(clientEntity);
                _clientRepository.SaveChange();
                var clientReadDto = _mapper.Map<ClientReadDTO>(clientEntity);

                return new OkObjectResult("New client added")
                { StatusCode = StatusCodes.Status201Created, Value = clientReadDto };
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
        public ActionResult<IEnumerable<ClientReadDTO>> FindAll()
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var listEntity = _clientRepository.FindAll();
            var listReadDto = _mapper.Map<List<ClientReadDTO>>(listEntity);

            return new OkObjectResult(listReadDto)
            { StatusCode = StatusCodes.Status200OK};
        }
        public ActionResult<IEnumerable<ClientReadDTO>> FindAll(int limit, int page)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if(limit <= 0 || page <= 0) 
                return new BadRequestObjectResult("The limit and page values must be greater than zero")
                { StatusCode = StatusCodes.Status400BadRequest };

            var listEntities = _clientRepository.FindAll(limit, page); 
            var listReadDto = _mapper.Map<List<ClientReadDTO>>(listEntities);
            return new OkObjectResult(listReadDto)
            { StatusCode = StatusCodes.Status200OK }; ;
        }

        public ActionResult<IEnumerable<ClientReadDTO>> FindByNameAndSurname(string name, string surname)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                return new BadRequestObjectResult("Name or Surname is null or empty")
                { StatusCode = StatusCodes.Status400BadRequest };
            }

            IEnumerable<ClientEntity> listEntities = _clientRepository.FindClient(name, surname);
            IEnumerable<ClientReadDTO> listReadDto = _mapper.Map<IEnumerable<ClientEntity>, IEnumerable<ClientReadDTO>>(listEntities);
          
            return new OkObjectResult(listReadDto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<ClientReadDTO> FindById(Guid id)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var entity = _clientRepository.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var clientReadDto = _mapper.Map<ClientEntity, ClientReadDTO>(entity);

            return new OkObjectResult(clientReadDto)
            { StatusCode = StatusCodes.Status200OK };
        }
        public ActionResult<ClientReadDTO> UpdateAddress(Guid id, AddressCreateDTO addressCreateDto)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty || addressCreateDto == null) 
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };

            var clientEntity = new ClientEntity { Id = id };

            var addressEntity = _mapper.Map<AddressEntity>(addressCreateDto);
            _addressRepository.Create(addressEntity);
            _clientRepository.Update(clientEntity, addressEntity);
            _clientRepository.SaveChange();
            
            var addressReadDto = _mapper.Map<AddressReadDTO>(addressEntity);

            return new OkObjectResult($"Client with Guid {id} updated")
                    { StatusCode = StatusCodes.Status200OK, Value = addressReadDto};
        }
        public ActionResult Delete(Guid id)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty)
                return new ObjectResult($"The object with the Guid {id} was not exist")
                { StatusCode = StatusCodes.Status404NotFound };

            _clientRepository.DeleteById(id);
            _clientRepository.SaveChange();

            return new OkObjectResult($"Client with Guid {id} deleted")
            { StatusCode = StatusCodes.Status200OK };
        }

    }
}
