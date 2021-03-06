﻿using Lunch.Application.Interfaces;
using Lunch.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private List<User> users = new List<User>();

        public int CreateUser()
        {
            int userId = users.Count + 1;
            users.Add(new User(userId));
            return userId;
        }

        public User GetUser(int userId)
        {
            return users.Single(x => x.Id == userId);
        }
    }
}
