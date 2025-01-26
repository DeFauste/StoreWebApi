using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.API.Services;
using Store.DataAccess.Postgress.Models;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/client")]
    public class ClientAPIController : ControllerBase
    {
        private ClientService _clientService;
        public ClientAPIController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ClientDTO>> GetAll()
        {
            if (_clientService.CanConnection() == false) return StatusCode(500, "No connection to the database");
            var listDto = _clientService.FindAll();
            return Ok(listDto);
        }

        [HttpGet("limit={limit:int}&page={page:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ClientDTO>> GetAll(int limit, int page)
        {
            if (_clientService.CanConnection() == false) 
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            var listDto = _clientService.FindAll(limit, page);
            return Ok(listDto);
        }

        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientDTO> GetById(Guid id)
        {
            if (_clientService.CanConnection() == false) 
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            var dto = _clientService.FindById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet("name={name}&page={surname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ClientDTO>> GetByNameAndSurname(string name, string surname)
        {
            if (_clientService.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");

            var listDto = _clientService.FindByNameAndSurname(name, surname);
            return Ok(listDto);
        }

        [HttpPost("newClient=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create([FromBody]ClientDTO clientDto)
        {
            if (_clientService.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (clientDto == null) return BadRequest();
            clientDto.Id = Guid.Empty;
            _clientService.Create(clientDto);
            return Ok();
        }

        [HttpPatch("updateAddress:id={id}&{address}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateA(Guid id, [FromBody]AddressEntity addressEntity)
        {
            if (_clientService.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (id == Guid.Empty || addressEntity == null) return BadRequest();
            _clientService.UpdateAddress(id, addressEntity);
            return Ok();
        }

        [HttpDelete("delete=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(Guid guid)
        {
            if (_clientService.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (guid == Guid.Empty) 
                return BadRequest();
            _clientService.Delete(guid);
            return Ok();
        }
    }
}
