using AVA.ForBrain.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetByID(object id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity obj);
        TEntity Insert(TEntity obj);
        Usuarios Authenticate(string username,string password);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<List<TEntity>> GetAllAsync();
        void Update(TEntity obj);
        void Remove(Guid id);
    }
}
