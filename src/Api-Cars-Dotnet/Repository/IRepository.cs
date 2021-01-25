using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Api_Cars_Dotnet
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity Insert(TEntity entity);
        void Update(string id, TEntity entity);
        void Delete(TEntity entity);
        void Delete(string id);

        List<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll();
        TEntity GetById(string id);
    }
}