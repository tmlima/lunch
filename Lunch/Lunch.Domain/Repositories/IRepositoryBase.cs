using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        T Get( int id );
    }
}
