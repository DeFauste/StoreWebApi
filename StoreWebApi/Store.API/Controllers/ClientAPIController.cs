using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/client")]
    public class ClientAPIController : ControllerBase
    {
        private readonly IClientRepository _db;
        private readonly IMapper _mapepr;
        public ClientAPIController(IClientRepository db, IMapper mapper)
        {
            _db = db;
            _mapepr = mapper;
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> Get()
        {
            var listEntity = _db.FindAll();
            var listDto = _mapepr.Map<List<ClientEntity>, List<ClientDTO>>(listEntity);
            return Ok(listDto);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ClientDTO clientDto)
        {
            var clientEntity = _mapepr.Map<ClientEntity>(clientDto);
            clientEntity.RegistrationDate = DateTime.UtcNow;
            _db.Add(clientEntity);
            return Ok();
        }
    }
}
