using GoMyShops.BAL;
using GoMyShops.Controllers;
using GoMyShops.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoMyShops.Web.Components
{
    public class PopulateCreate : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PopulateCreate(IHttpContextAccessor httpContextAccessor )
        {
          
            _httpContextAccessor = httpContextAccessor;
      
        }
        public IViewComponentResult Invoke()
        {
            //var model = new SignUpDetailsViewModels(_httpContextAccessor);
            var model = new SignUpDetailsViewModels();
            return View("Default", model);
        }

        //public async Task<IViewComponentResult> InvokeAsync(string userId)
        //{
        //    var items = SomeItems;
        //    return View("Default", items);
        //}
    }//end class
}//end namespace
