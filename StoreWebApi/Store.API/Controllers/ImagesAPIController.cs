using Microsoft.AspNetCore.Mvc;
using Store.API.Services;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/images")]
    public class ImagesAPIController: ControllerBase
    {
        private ImageService _service;
        public ImagesAPIController(ImageService service)
        {
            _service = service;
        }
        [HttpPost("newImage={image}&idProduct={id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create([FromBody] byte[] image, Guid idProduct)
        {
            if (_service.CanConnection() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, "No connection to the database");
            if (image == null && idProduct != Guid.Empty) return BadRequest();
            _service.Create(image, idProduct);
            return Ok();
        }
    }
}
