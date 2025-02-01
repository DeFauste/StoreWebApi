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
            return _service.Create(dto);
        }

        [HttpPatch("updateQuantities:id={id}&q={q:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateQuantities(Guid id, int decreases)
        {
            return _service.UpdateQuantities(id, decreases); ;
        }

        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> GetById(Guid id)
        {
            return _service.FindById(id);
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ProductDTO>> GetAll()
        {
            return _service.FindAll();
        }

        [HttpDelete("delete=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(Guid guid)
        {
            return _service.Delete(guid);
        }
    }
}
