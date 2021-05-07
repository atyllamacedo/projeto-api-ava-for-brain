using AVA.ForBrain.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Interfaces.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
