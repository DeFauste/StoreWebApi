using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Postgress.Models;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/client")]
    public class ClientAPIController : ControllerBase
    {
        private readonly ClientsRepository _db;
        public ClientAPIController(ClientsRepository db)
        {
            _db = db;
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ClientEntity>>> Get()
        {
            return Ok(await _db.Get());
        }
    }
}
