using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace Api_Cars_Dotnet
{
    public class MongoDBRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {

        private readonly IMongoCollection<TEntity> _collection;
        private readonly IMongoDatabase _database;

        public MongoDBRepository(IMongoDatabase database, IDatabaseSettings settings)
        {
            _database = database;

            _collection = _database.GetCollection<TEntity>(settings.CollectionName);
        }

        public void Delete(TEntity entity)
        {
            _collection.DeleteOne(x => x.Id == entity.Id);
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public List<TEntity> GetAll() => _collection.Find(x => true).ToList();

        public TEntity GetById(string id) => _collection.Find<TEntity>(x => x.Id == id).FirstOrDefault();

        public TEntity Insert(TEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public List<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate) => _collection.Find<TEntity>(predicate).ToList();

        public void Update(string id, TEntity entity)
        {
            _collection.ReplaceOne(x => x.Id == id, entity);
        }

    }
}