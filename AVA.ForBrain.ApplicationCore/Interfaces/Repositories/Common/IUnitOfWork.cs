using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
