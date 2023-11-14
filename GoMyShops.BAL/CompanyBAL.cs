using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.ViewModels;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.Extensions.Logging;

namespace GoMyShops.BAL
{
    public interface ICompanyBAL
    {
        List<SelectListItem> GetActiveCompanyList();
    }

    public class CompanyBAL:ICompanyBAL
    {
        private readonly ILogger<CompanyBAL> _logger;
        //private UrlHelper _urlHelper;
        IUnitOfWorkFactory _uowFactory;
        IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyBAL(IUnitOfWorkFactory uowFactory, IHttpContextAccessor httpContextAccessor, ILogger<CompanyBAL> logger)
        {
            _uowFactory = uowFactory;
            _uow = uowFactory.Create();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public List<SelectListItem> GetActiveCompanyList()
        {
            List<SelectListItem> infos = null;// = new List<SelectListItem>();
            try
            {               
                infos = _uow.Repository<Company>().GetAsQueryable()
                            .Where(r => r.Status == "1")    
                            .Select(r => new SelectListItem()
                            {
                                Text = r.CompanyCode + " - " + r.Name,
                                Value = r.CompanyCode
                            }).ToList();
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
            }
            finally { }
            return infos.IsNullThenNew(_httpContextAccessor);
        }

    }//end class
}//end namespace
