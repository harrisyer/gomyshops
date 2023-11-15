using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoMyShops.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace GoMyShops.BAL.WebAPI
{
    public interface IApiLoginBAL
    {
     
       
    }

    public class ApiLoginBAL: IApiLoginBAL
    {
        private readonly ILogger<ApiLoginBAL> _logger;
        IUnitOfWork _uow;

        public ApiLoginBAL(ILogger<ApiLoginBAL> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }



    }//end class
}//end namespace