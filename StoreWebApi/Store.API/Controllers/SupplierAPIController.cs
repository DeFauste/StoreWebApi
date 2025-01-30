using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.API.Services;
using Store.DataAccess.Postgress.Models;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/supplier")]
    public class SupplierAPIController:ControllerBase
    {
        private SupplierService _service;
        public SupplierAPIController(SupplierService service)
        {
            _service = service;
        }

        [HttpPost("newSupplier=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create([FromBody] SupplierDTO dto)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (dto == null) return BadRequest();
            dto.Id = Guid.Empty;
            _service.Create(dto);
            return Ok();
        }
        [HttpPatch("updateAddress:id={id}&{address}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateAddress(Guid id, [FromBody] AddressEntity addressEntity)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (id == Guid.Empty || addressEntity == null) return BadRequest();
            _service.UpdateAddress(id, addressEntity);
            return Ok();
        }
        [HttpDelete("delete=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(Guid guid)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (guid == Guid.Empty)
                return BadRequest();
            _service.Delete(guid);
            return Ok();
        }
        [HttpGet("limit={limit:int}&page={page:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SupplierDTO>> GetAll()
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            var listDto = _service.FindAll();
            return Ok(listDto);
        }

        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SupplierDTO> GetById(Guid id)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            var dto = _service.FindById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
    }
}
