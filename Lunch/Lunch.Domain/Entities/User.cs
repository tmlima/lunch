﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Entities
{
    public class User : EntityBase
    {
        public User(int id)
        {
            this.Id = id;
        }
    }
}
