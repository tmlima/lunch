using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        void Add( T entity );
        IList<T> GetAll();
        T Get( int id );
    }
}
