using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Application.Interfaces
{
    public interface IUserAppService
    {
        int CreateUser();
        User GetUser(int userId); 
    }
}
