using AVA.ForBrain.ApplicationCore.Entities;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories;
using AVA.ForBrain.ApplicationCore.Interfaces.Repositories.Common;
using AVA.ForBrain.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AVA.ForBrain.Infrastructure.Repositories.Common
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;

            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual TEntity Add(TEntity obj)
        {
            try
            {
                Context.AddCommand(() => DbSet.InsertOneAsync(obj));

                //DbSet.InsertOne(obj);

                return obj;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var data = DbSet.Find(predicate).FirstOrDefault();
            return data;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual void Update(TEntity obj)
        {
            Context.AddCommand(() =>
            DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj));
        }

        public virtual void Remove(Guid id)
        {
            Context.AddCommand(() =>
            DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            var query = DbSet.AsQueryable<TEntity>();

            if (filter != null)
            {
                query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            var idRequisicao = new BsonDocument("_id", (int)id);
            var result = DbSet.Find(idRequisicao).FirstOrDefaultAsync().Result;
            return result;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await (from i in DbSet.AsQueryable<TEntity>()
                                  select i).ToListAsync();
            return entities;
        }

        public virtual Usuarios Authenticate(string username, string password)
        {
            var collection = Context.GetCollection<Usuarios>("Usuarios");

            var filter = Builders<Usuarios>.Filter.Where(t => t.User.Equals(username) && t.Password.Equals(password));
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        public TEntity Insert(TEntity obj)
        {
            DbSet.InsertOne(obj);
            Context.SaveChanges();
            return obj;
        }
    }
}
