using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Application.Interfaces
{
    public interface IRestaurantAppService
    {
        int Add(string name);
        Restaurant Get(int id);
    }
}
