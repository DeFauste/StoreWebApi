using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v1/client")]
    public class ClientAPIController : ControllerBase
    {
        [HttpGet("get")]
        public IEnumerable<int> GetInts() => new int[] { 1, 2, 3 };
    }
}
