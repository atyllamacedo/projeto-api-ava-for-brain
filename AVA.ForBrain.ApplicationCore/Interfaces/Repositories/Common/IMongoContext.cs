using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common
{

    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func = null);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
