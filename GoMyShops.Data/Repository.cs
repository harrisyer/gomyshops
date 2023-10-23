using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

//using System.Data;
//using System.Data.Entity;
//using SimpleInjector;
//using System.Data.Entity.Core.Objects;
namespace GoMyShops.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DataContext dbContext;
        public DbSet<TEntity> dbSet;
        protected IQueryable<TEntity> Entities;

        public Repository(DataContext context)
        {
            //var a = Lifestyle.Singleton.CreateProducer<DbContext, MerchantradeContext>(container);
            //var b = a.GetInstance();
            //var context1 = serviceProvider.GetService(typeof(MerchantradeContext));


            this.dbContext = context;
            this.dbSet = dbContext.Set<TEntity>();

        }

        ////public Repository(MerchantradeContext context, SimpleInjector.Container container)
        ////{
        ////    var a = Lifestyle.Singleton.CreateProducer<DbContext, MerchantradeContext>(container);
        ////    var b = a.GetInstance();
        ////    //var context1 = serviceProvider.GetService(typeof(MerchantradeContext));


        ////    this.dbContext = context;
        ////    this.dbSet = dbContext.Set<TEntity>();

        ////}

        protected IList<TReturn> Get<TResult, TKey, TGroup, TReturn>(
        List<Expression<Func<TEntity, bool>>> predicates,
        Expression<Func<TEntity, TResult>> firstSelector,
        Expression<Func<TResult, TKey>> orderSelector,
        Func<TResult, TGroup> groupSelector,
        Func<IGrouping<TGroup, TResult>, TReturn> selector)
        {
            return predicates
                .Aggregate(Entities, (current, predicate) => current.Where(predicate))
                .Select(firstSelector)
                .OrderBy(orderSelector)
                .GroupBy(groupSelector)
                .Select(selector)
                .ToList();

            //sample
             // This is what I think you should expose to the application
            //// This is the usage of the expression fest above.
            //public IEnumerable<GroupedHorses> GetGroupedByColor() {
            //    return horseRepo.GetAllPaged(
            //        new List<Expression<Func<Horse, bool>>> {
            //            h => h.Name != string.Empty, 
            //            h => h.Id > 0
            //        },
            //        h => new HorseShape { Id = h.Id, Name = h.Name, Color = h.Color },
            //        hs => hs.Name,
            //        hs => hs.Color,
            //        g => new GroupedHorses
            //        {
            //            Color = g.Key,
            //            Horses = g.ToList()
            //        }
            //    );
            //}
     }


        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
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

            //var trace = ((ObjectQuery)query).ToTraceString();
        }

        public IQueryable<TEntity> GetAsQueryable(
           Expression<Func<TEntity, bool>>? filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public TEntity GetByKey(object key)
        {
            return dbSet.Find(key);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                var dbEntityEntry = dbContext.Entry(entity);
                foreach (var property in dbEntityEntry.OriginalValues.Properties)
                {
                    var name = property.Name;
                    var original = dbEntityEntry.OriginalValues[name];
                    var current = dbEntityEntry.CurrentValues[name];
                    //var original = dbEntityEntry.OriginalValues.GetValue<object>(name);
                    //var current = dbEntityEntry.CurrentValues.GetValue<object>(name);
                    if (original != null && !original.Equals(current))
                        dbEntityEntry.Property(name).IsModified = true;
                }
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
               
            //dbSet.Attach(entity);
            //dbContext.Entry(entity).State = EntityState.Modified;
            var dbEntityEntry = dbContext.Entry(entity);
            foreach (var property in dbEntityEntry.OriginalValues.Properties)
            {
                var name = property.Name;
                //var original = dbEntityEntry.OriginalValues.GetValue<object>(name);
               // var current = dbEntityEntry.CurrentValues.GetValue<object>(name);
                var original = dbEntityEntry.OriginalValues[name];              
                var current = dbEntityEntry.CurrentValues[name];
                if (original != null && !original.Equals(current))
                    dbEntityEntry.Property(name).IsModified = true;
            }
        }

        public void Update(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry)
        {
            if (entityEntry == null)
            {
                return;
            }

            //Todo Harris (Test) Modify Core
            try {
                foreach (var property in entityEntry.OriginalValues.Properties)
                {
                    var name = property.Name;
                    var original = entityEntry.OriginalValues[name];
                    //var original = entityEntry.OriginalValues.GetValue<object>(property);
                    var current = entityEntry.CurrentValues[name];//.GetValue<object>(name);
                    if (original != null && !original.Equals(current))
                    {
                        entityEntry.Property(name).IsModified = true;
                    }
                    else
                    {
                        entityEntry.Property(name).IsModified = false;
                    }

                    if (original == null && current != null)
                    {
                        entityEntry.Property(name).CurrentValue = current;
                        entityEntry.Property(name).IsModified = true;
                    }

                }
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }


          
        }

        public void Delete(object key)
        {
            TEntity entityToDelete = dbSet.Find(key);
            if (entityToDelete !=null)
            Delete(entityToDelete);
        }

        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        /// <summary>
        /// Consider the filter criteria and return the row count of DbSet.
        /// </summary>
        public int Count(Expression<Func<TEntity, bool>>? filter = null)
        {
            int count = 0;
            IEnumerable<TEntity> result;

            result = (filter != null) ? dbSet.Where(filter) : dbSet;

            if (result != null)
            {
                count = result.OrderBy(m => 0).Count();
            }

            return count;
        }


        ///// <summary>
        ///// Consider the filter criteria and return the DbSet after applying skip, take and orderBy.
        ///// </summary>
        //public IEnumerable<TEntity> Paginate(out int totalRecords, Expression<Func<TEntity, bool>> filter = null, string orderBy = null, int pageSize = 0, int currentPage = 0)
        //{
        //    IQueryable<TEntity> query = (filter != null) ? dbSet.Where(filter) : dbSet;

        //    totalRecords = query.Count();

        //    int totalPages = (int)Math.Ceiling((float)totalRecords / (int)pageSize);

        //    int page = totalPages < currentPage ? currentPage - 1 : currentPage;

        //    if (page == 0)
        //    {
        //        page = 1;
        //    }

        //    int skip = (page - 1) * pageSize;

        //    query = !String.IsNullOrEmpty(orderBy) ? query.OrderBy(orderBy) : query.OrderBy(m => 0);

        //    if (skip >= 0)
        //    {
        //        query = query.Skip(skip).Take(pageSize);
        //    }

        //    return query.ToList();
        //}
    }
}