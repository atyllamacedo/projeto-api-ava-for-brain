using AVA.ForBrain.ApplicationCore.Entities;
using AVA.ForBrain.ApplicationCore.ModelAssistant;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Services.Common
{
    public class BookService
    {
        private readonly IMongoCollection<Usuarios> _collection;
        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<Usuarios>(settings.BooksCollectionName);
        }
        public List<Usuarios> Get() => _collection.Find(aluno => true).ToList();
        public Usuarios Get(string id) =>
            _collection.Find<Usuarios>(aluno => aluno.IdUsuario == id).FirstOrDefault();

        public Usuarios Create(Usuarios entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Update(string id, Usuarios entity) =>
            _collection.ReplaceOne(aluno => aluno.IdUsuario == id, entity);

        public void Remove(Usuarios entity) =>
            _collection.DeleteOne(aluno => aluno.IdUsuario == entity.IdUsuario);

        public void Remove(string id) =>
            _collection.DeleteOne(entity => entity.IdUsuario == id);
    }
}
