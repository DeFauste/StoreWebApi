using Store.DataAccess.Postgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IAddressRepository: IRepositiry<AddressEntity, AddressEntity, Guid>
    {
    }
}
