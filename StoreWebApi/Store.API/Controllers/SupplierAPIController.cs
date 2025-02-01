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
            return _service.Create(dto);
        }
        [HttpPatch("updateAddress:id={id}&")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateAddress(Guid id, [FromBody] AddressDTO addressEntity)
        {
            return _service.UpdateAddress(id, addressEntity);
        }
        [HttpDelete("delete=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(Guid guid)
        {
            return _service.Delete(guid);
        }
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SupplierDTO>> GetAll()
        {
            return _service.FindAll();
        }

        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SupplierDTO> GetById(Guid id)
        {
            return _service.FindById(id);
        }
    }
}
