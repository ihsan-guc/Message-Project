using Message.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Message.Data.DAL.Repository.Core
{
    public interface IRepository<TEntity>
        where TEntity : IEntity<Guid>
    {
        IQueryable<TEntity> GetQueryable();
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where);
        TEntity GetSingle(Guid id);
        TEntity GetById(Guid id);
        TModel GetById<TModel>(Func<TEntity, TModel> selector, Guid id);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        void SaveChanges();
    }
}
