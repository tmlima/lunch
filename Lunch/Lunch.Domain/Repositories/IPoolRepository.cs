using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Repositories
{
    public interface IPoolRepository : IRepositoryBase<Pool>
    {
        int Add( DateTime closingTime );
    }
}
