using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
    }
}
