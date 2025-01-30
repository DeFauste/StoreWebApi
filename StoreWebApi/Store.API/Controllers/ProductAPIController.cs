using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.API.Services;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductAPIController: ControllerBase
    {
        private ProductService _service;
        public ProductAPIController(ProductService service)
        {
            _service = service;
        }

        [HttpPost("newProduct=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create([FromBody] ProductDTO dto)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (dto == null) return BadRequest();
            dto.Id = Guid.Empty;
            _service.Create(dto);
            return Ok();
        }

        [HttpPatch("updateQuantities:id={id}&q={q:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateQuantities(Guid id, int decreases)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (id == Guid.Empty || decreases < 1) return BadRequest();
            _service.UpdateQuantities(id, decreases);
            return Ok();
        }

        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> GetById(Guid id)
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

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ProductDTO>> GetAll()
        {
            if (_service.CanConnection() == false) return StatusCode(500, "No connection to the database");
            var listDto = _service.FindAll();
            return Ok(listDto);
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
    }
}
