using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.API.Services;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/client")]
    public class ClientAPIController : ControllerBase
    {
        private ClientService _service;
        public ClientAPIController(ClientService service)
        {
            _service = service;
        }
        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientReadDTO> GetById(Guid id)
        {
            return _service.FindById(id);
        }

        [HttpPost("newClient=")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientReadDTO> Create([FromBody] ClientCreateDTO clientDto)
        {
            return _service.Create(clientDto);
        }

        [HttpDelete("delete=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(Guid guid)
        {
            return _service.Delete(guid);
        }

        [HttpGet("name={name}&surname={surname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ClientReadDTO>> GetByNameAndSurname(string name, string surname)
        {
            return _service.FindByNameAndSurname(name, surname);
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ClientReadDTO>> GetAll()
        {
            return _service.FindAll();
        }

        [HttpGet("limit={limit:int}&offset={page:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ClientReadDTO>> GetAll(int limit, int page)
        {
            return _service.FindAll(limit, page);
        }

        [HttpPatch("updateAddress:id={id}&address=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientReadDTO> UpdateAddress(Guid id, [FromBody]AddressCreateDTO addressdto)
        {       
            return _service.UpdateAddress(id, addressdto);
        }

    }
}
