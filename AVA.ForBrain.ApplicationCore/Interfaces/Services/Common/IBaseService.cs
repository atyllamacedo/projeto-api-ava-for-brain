using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AVA.ForBrain.ApplicationCore.Interfaces.Services.Common
{
    public interface IBaseService<TEntity>
    {
        IQueryable<TEntity> SelectFromTable(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        TEntity Remove(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Add(TEntity entity);
        TEntity Get(long id);
    }
}
