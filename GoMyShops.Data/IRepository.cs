
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoMyShops.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Count(Expression<Func<TEntity, bool>>? filter = null);
        void Delete(object key);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        TEntity GetByKey(object key);
        void Insert(TEntity entity);
        //IEnumerable<TEntity> Paginate(out int totalRecords, Expression<Func<TEntity, bool>> filter = null, string orderBy = null, int pageSize = 0, int currentPage = 0);
        void Update(TEntity entity);
        void Update(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry);
    }
}