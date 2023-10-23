
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using GoMyShops.Data.Entity;
//using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.AspNetCore.Http.Extensions;
using SimpleInjector;
//using GoMyShops.Commons;
//using Serilog;
using Microsoft.Extensions.Logging;

namespace GoMyShops.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Defination
        //private static readonly ILogger log = Log.ForContext(typeof(UnitOfWork));
        private readonly ILogger<UnitOfWork> _logger;
        //readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private readonly IHttpContextAccessor _httpContextAccessor;
        protected DataContext? hqContext = null;
        private DataContext? _Context = null;
        public bool isError { get; set; }
        public string? Error { get; set; } 
        private Dictionary<string, object>? repositories;      
        SimpleInjector.Container _container;
        DataContext _context;
        #endregion
        #region Contructor
        public UnitOfWork(DataContext context, ILogger<UnitOfWork> logger, SimpleInjector.Container container) :base()
        {
            //_httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _context = context;
            var a = Lifestyle.Scoped.CreateProducer<DataContext, DataContext>(container);
            hqContext = a.GetInstance();
            _container = container;
        }       
        #endregion
        #region Property
       

        public DataContext Context
        {
            get { return hqContext; }
            set{; }
        }
        #endregion       
        #region Public Functions
        public void DetachAll()
        {

            foreach (EntityEntry dbEntityEntry in hqContext.ChangeTracker.Entries())
            {

                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }

        public void DetachAllEntities(List<EntityEntry> entry)
        {
            var changedEntriesCopy = entry
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entity in changedEntriesCopy)
            {
                hqContext.Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        public bool Save()
        {
            try
            {   
                 hqContext.SaveChanges();
         
            }
            catch (ValidationException ve)
            {         
                isError = true;
                var error = ve.Message;//EntityValidationErrors.First().ValidationErrors.First();
                Error = error;
                _logger.LogError(error);
            }
            catch (Exception ex)
            {
                isError = true;
                Error = ex.Message;
                _logger.LogError(ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException.Message);
                }//end if
            }
            return isError;

        }

        public async Task<bool> SaveAsync()
        {
            try
            {

                var a=await hqContext.SaveChangesAsync();
                //if (a > 0)
                //{
                //    isError = false;
                //}//end if
                isError = false;

            }
            catch (ValidationException ve)
            {
                //Todo Harris (Test) Modify Core
                isError = true;
                var error = ve.Message;//EntityValidationErrors.First().ValidationErrors.First();
                Error = error;
                _logger.LogError(error);
            }
            catch (Exception ex)
            {
                isError = true;
                Error = ex.Message;
                _logger.LogError(ex.Message);
            }
            return isError;

        }
        #endregion
        #region IDisposable Members

        ////private bool disposed = false;

        ////protected virtual void Dispose(bool disposing)
        ////{
        ////    if (!this.disposed)
        ////    {
        ////        if (disposing)
        ////        {
        ////            this.hqContext.Dispose();
        ////        }
        ////    }

        ////    this.disposed = true;
        ////}

        ////public void Dispose()
        ////{
        ////    this.Dispose(true);
        ////    GC.SuppressFinalize(this);
        ////}

        #endregion IDisposable Members

        public Repository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;


            try
            {
                if (!repositories.ContainsKey(type))
                {
                    var repositoryType = typeof(Repository<>);
                    var parameters = new object[3];
                    parameters[0] = _context;
                   // parameters[1] = _container;
                    //parameters[2] = hqContext;
//var d=Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), new object[] { parameters });
                    var c=repositoryType.MakeGenericType(typeof(T));
                    //var repositoryInstance1 = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), parameters);
                    //var repositoryInstance = Activator.CreateInstance(typeof(T), parameters);
                    var repositoryInstance = Activator.CreateInstance(c, hqContext);
                    repositories.Add(type, repositoryInstance);

                }
            }
            catch (Exception ex)
            {
                var a = ex;
            }
            finally { }

           
            return (Repository<T>)repositories[type];
        }

        public async Task<Repository<T>> RepositoryAsync<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;


            try
            {
                if (!repositories.ContainsKey(type))
                {
                    var repositoryType = typeof(Repository<>);
                    var parameters = new object[3];
                    parameters[0] = _context;
                    // parameters[1] = _container;
                    //parameters[2] = hqContext;
                    //var d=Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), new object[] { parameters });
                    var c = repositoryType.MakeGenericType(typeof(T));
                    //var repositoryInstance1 = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), parameters);
                    //var repositoryInstance = Activator.CreateInstance(typeof(T), parameters);
                    var repositoryInstance = Activator.CreateInstance(c, hqContext);
                    repositories.Add(type, repositoryInstance);

                }
            }
            catch (Exception ex)
            {
                var a = ex;
            }
            finally { }


            return ( Repository<T>)repositories[type];
        }
    }//end class
}//end namespace