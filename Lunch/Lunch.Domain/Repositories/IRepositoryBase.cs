using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get( int id );
    }
}
