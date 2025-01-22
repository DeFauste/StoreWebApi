using AutoMapper;
using Store.DataAccess.Postgress.Repositories;

namespace Store.API.Services
{
    public class ClientService
    {
        private readonly IClientRepository _db;
        private readonly IMapper _mapepr;
        public ClientService(IClientRepository db, IMapper mapper)
        {
            _db = db;
            _mapepr = mapper;
        }
    }
}
