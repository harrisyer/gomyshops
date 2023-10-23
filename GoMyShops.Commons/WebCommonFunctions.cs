using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Commons
{
    public static class  WebCommonFunctions
    {
        public static bool ModelStateIsValid(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var details = new ValidationProblemDetails(modelState);
                details.Status = StatusCodes.Status400BadRequest;
                return false;
            }//end if
            return true;
        }//end function
    }//end class
}//end namespace
