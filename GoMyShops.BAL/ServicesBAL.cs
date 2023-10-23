using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoMyShops.BAL
{
    public interface IServicesBAL
    {
      
    }//end interface

    public class ServicesBAL : IServicesBAL
    {
        #region Definations
        private readonly ILogger<ServicesBAL> _logger;
       IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public bool isError { get; set; }
        #endregion
        #region Constructor
        public ServicesBAL(ILogger<ServicesBAL> logger ,IHttpContextAccessor httpContextAccessor, IUnitOfWorkFactory uowFactory)
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion
        #region Public Functions      
      
        #endregion
        #region Private Functions
        #endregion
    }//end class
}//end namespace
