﻿using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.API.Services;
using System.Text;
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

        [HttpPost("idProduct={idProduct}&image=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create(Guid idProduct,[FromHeader] byte[] image)
        {
            return _service.Create(image, idProduct); 
        }
        [HttpGet("id=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ImageDTO> GetById(Guid id)
        {
            return _service.FindById(id);
        }

        [HttpDelete("delete=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(Guid guid)
        {

            return _service.Delete(guid);
        }
        [HttpPatch("id={id}&image=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateAddress(Guid id, [FromHeader] byte[] image)
        {
            return _service.Update(id, image);
        }
        [HttpGet("idProfuct=")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ImageDTO> GetByIdProduct(Guid id)
        {
            return _service.GetByIdProduct(id);
        }
    }
}
