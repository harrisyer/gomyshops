using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Http;
namespace GoMyShops.Data
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        
    }

    public interface IUnitOfWork //: IDisposable
    {
        bool isError { get; set; }
        string? Error { get; set; }      
        //string ParentValue { get; set; }       
        bool Save();
        void DetachAll();
        void DetachAllEntities(List<EntityEntry> entry);       
        Task<bool> SaveAsync();
        DataContext Context { get; set; }
        Repository<T> Repository<T>() where T : class;

        Task<Repository<T>> RepositoryAsync<T>() where T : class;
    }

    public class UnitOfWorkFactory: IUnitOfWorkFactory
    {
        //IUnitOfWork _uow;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        SimpleInjector.Container _container;
        private readonly ILogger<UnitOfWork> _logger;
        //Microsoft.Extensions.DependencyInjection.ServiceProvider _serviceProvider;
        DataContext _context;
        public UnitOfWorkFactory(DataContext context, ILogger<UnitOfWork> logger, SimpleInjector.Container container)
        {
            //_httpContextAccessor = httpContextAccessor;
            _container = container;
            _context = context;
            _logger = logger;
            //_serviceProvider = serviceProvider;
            // _uow = new UnitOfWork();
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_context,_logger, _container);
        }

    }


}
