﻿using AutoMapper;
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
        public async Task<ActionResult<IEnumerable<ClientEntity>>> Get()
        {
            var listEntity = await _db.FindAll();
            var listDto = _mapepr.Map<List<ClientEntity>, List<ClientDto>>(listEntity);
            return Ok(listDto);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ClientDto clientDto)
        {
            var clientEntity = _mapepr.Map<ClientEntity>(clientDto);
            clientEntity.RegistrationDate = DateTime.UtcNow;
            await _db.Add(clientEntity);
            return Ok();
        }
    }
}
