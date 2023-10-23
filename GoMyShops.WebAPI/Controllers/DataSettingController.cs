using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoMyShops.Data;
using Microsoft.Extensions.Caching.Distributed;
using MyBGList.Controllers;
using MyBGList.DTO;
using MyBGList.Models;
using GoMyShops.BAL;
using GoMyShops.Models;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace GoMyShops.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DataSettingController : ControllerBase
    {
        #region Definitions
        private readonly DataContext _context;

        private readonly ILogger<DataSettingController> _logger;

        private readonly IDistributedCache _distributedCache;
        IDataSettingBAL _dataSettingBAL;
        #endregion
        #region Constructor
        public DataSettingController(
            DataContext context,
            ILogger<DataSettingController> logger, IDataSettingBAL dataSettingBAL,
            IDistributedCache distributedCache)
        {
            _context = context;
            _logger = logger;
            _dataSettingBAL = dataSettingBAL;
            _distributedCache = distributedCache;
        }
        #endregion
        #region Public Functions
        [HttpGet(Name = "GetDataSettingList")]
        [ResponseCache(CacheProfileName = "Any-60")]
        public async Task<List<DataSettingListModel>> GetDataSettingListAsync(
           )
        {
            var data= await _dataSettingBAL.GetDataSettingListAsync();
            return data;
        }

        [HttpPost]        
        //[ResponseCache(CacheProfileName = "NoCache")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(DataDetailsSettingModels model)
        {
            if (!ModelState.IsValid)
            {
               // var errorMsg = ModelState.Select(x => x.Value.Errors);
                var details = new ValidationProblemDetails(ModelState);
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
                //return View(model);
            }//end if

            bool isError = false;

            isError =await _dataSettingBAL.CreateAsync(model, ModelState);

            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }//end if
            return Ok();
       
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(DataDetailsSettingModels model)
        {
            if (!ModelState.IsValid)
            {  
                return new BadRequestObjectResult(ModelState);    
            }//end if

            bool isError = false;

            isError = await _dataSettingBAL.EditAsync(model, ModelState);

            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
                //var details = new ValidationProblemDetails(ModelState);              
                //details.Status = StatusCodes.Status400BadRequest;
                //return new BadRequestObjectResult(details);
            }//end if
            return Ok();

        }
        #endregion

    }
}
