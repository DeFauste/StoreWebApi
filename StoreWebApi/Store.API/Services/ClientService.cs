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
        public ActionResult Create(ClientDTO clientDto)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database") 
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (clientDto == null) 
                return new BadRequestObjectResult("Object cannot be null")
                { StatusCode = StatusCodes.Status400BadRequest };
            
            clientDto.Id = Guid.Empty;
            try
            {
                var clientEntity = _mapper.Map<ClientEntity>(clientDto);
                clientEntity.RegistrationDate = DateTime.UtcNow;
                var created = _clientRepository.Add(clientEntity);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new OkObjectResult("New client added")
                { StatusCode = StatusCodes.Status200OK };

        }
        public ActionResult<IEnumerable<ClientDTO>> FindAll()
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var listEntity = _clientRepository.FindAll();
            var listDto = _mapper.Map<List<ClientEntity>, List<ClientDTO>>(listEntity);
            return listDto;
        }
        public ActionResult<IEnumerable<ClientDTO>> FindAll(int limit, int page)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };
            if(limit <= 0 || page <= 0) 
                return new BadRequestObjectResult("The limit and page values must be greater than zero")
                { StatusCode = StatusCodes.Status400BadRequest };

            var listEntities = _clientRepository.FindAll(limit, page); 
            var listDto = _mapper.Map<List<ClientEntity>, List<ClientDTO>>(listEntities);
            return listDto;
        }

        public ActionResult<IEnumerable<ClientDTO>> FindByNameAndSurname(string name, string surname)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            List<ClientDTO> listDto;
            List<ClientEntity> listEntities;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                listEntities = new List<ClientEntity>();
            }
            else
            {
                listEntities = _clientRepository.FindClient(name, surname);
            }
            listDto = _mapper.Map<List<ClientEntity>, List<ClientDTO>>(listEntities);
            return listDto;
        }
        public ActionResult<ClientDTO> FindById(Guid id)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            var entity = _clientRepository.FindById(id);
            if (entity == null)
                return new ObjectResult($"The object with the Guid {id} was not found")
                { StatusCode = StatusCodes.Status404NotFound };

            var dto = _mapper.Map<ClientEntity, ClientDTO>(entity);
            return new OkObjectResult(dto)
            { StatusCode = StatusCodes.Status200OK }; ;
        }
        public ActionResult UpdateAddress(Guid id, AddressDTO address)
        {
            if (_clientRepository.CanConnection() == false)
                return new ObjectResult("No connection to the database")
                { StatusCode = StatusCodes.Status500InternalServerError };

            if (id == Guid.Empty || address == null) 
                return new BadRequestObjectResult("Invalid data")
                { StatusCode = StatusCodes.Status400BadRequest };

            var clientEntity = new ClientEntity { Id = id };
            address.Id = Guid.Empty;
            var addressEntity = _mapper.Map<AddressDTO, AddressEntity>(address);
            var createdAddress = _addressRepository.Add(addressEntity);
            _clientRepository.Update(clientEntity, createdAddress);

            return new OkObjectResult($"Client with Guid {id} updated")
                    { StatusCode = StatusCodes.Status200OK };
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
            return new OkObjectResult($"Client with Guid {id} deleted")
            { StatusCode = StatusCodes.Status200OK };
        }

    }
}
