using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Html;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace GoMyShops.Models.Helpers
{
    public static class ModelStateHelper
    {

        public static string Errorsstr(this ModelStateDictionary modelState, bool excludePropertyErrors, bool includeExternalErrors=false, string validationSummaryHeader = null)
        {
            //<div class="validation-summary-errors"><span>Please clean the following errors and try again</span>
            //<ul><li>Require at least 1 product</li>
            //</ul></div>}
            //    base: {<div class="validation-summary-errors"><span>Please clean the following errors and try again</span>
            //<ul><li>Require at least 1 product</li>
            //</ul></div>

            StringBuilder sb = new StringBuilder();
            if (!modelState.IsValid)
            {
                sb.Append("<div class=\"validation-summary-errors alert-danger\">");
                sb.Append("<span>");
                sb.Append(validationSummaryHeader == null ? CommonSetting.ValidationSumarryHeader : validationSummaryHeader);
                sb.Append("</span>");
                sb.Append("<ul>");

                int i = 0;

                int count = 0;

                var a = Errors(modelState);

                foreach (var key in modelState.Keys)
                {
                    if (excludePropertyErrors)
                    {
                        if (key != null && (key != "" && !includeExternalErrors))
                        {
                            continue;
                        }
                    }

                    if (modelState[key].Errors.Count == 0) continue;

                    foreach (var value1 in modelState[key].Errors)
                    {
                        


                        var error = value1; //modelState[key].Errors.FirstOrDefault();
                                            //"field is required"
                        //if (error.ErrorMessage.Contains("field is required")) continue;

                        if (error != null && error.ErrorMessage != "")
                        {
                            if (CommonFunctions.isEvenNumber(i))
                            {
                                sb.Append("<li class=\"field-validation-error evenclass \">");
                            }
                            else
                            {
                                sb.Append("<li class=\"field-validation-error oddclass\">");
                            }//end if-else

                            sb.Append(error.ErrorMessage);
                            sb.Append("</li>");
                            i = i + 1;
                            count = count + 1;
                        }

                    }//end foreach
                }//end foreach
                sb.Append("</ul></div>");

                if (count > 0)
                {
                    return sb.ToString();
                }
                else
                {
                    return "";
                }//end if-else

            }
            return null;
        }

        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Count() > 0);
            }
            return null;
        }

        public static bool IsErrors(this ModelStateDictionary modelState,string param1)
        {
            //int count = 0;
            if (!modelState.IsValid)
            {
                var b = modelState.Where(x => x.Value.Errors.Count() > 0).ToList();

                if (b.Count() == 1 )
                {
                    foreach (var kvp in b)
                    {
                        var key = kvp.Key;
                        var value = kvp.Value;
                        if (key== param1)
                        {
                            return false;
                        }
                        // do something with key and/or value
                    }


                }
                else
                {
                    return true;
                }//end if-else

                return true;


            }
            return false;
        }

        public static bool IsErrors(this ModelStateDictionary modelState, string param1,string param2)
        {
            //int count = 0;
            if (!modelState.IsValid)
            {
                var b = modelState.Where(x => x.Value.Errors.Count() > 0).ToList();

                if (b.Count() >=1 )
                {
                    foreach (var kvp in b)
                    {
                        var key = kvp.Key;
                        var value = kvp.Value;
                        if (key == param1 || key == param2)
                        {
                            return false;
                        }
                        // do something with key and/or value
                    }


                }
                else
                {
                    return false;
                }//end if-else

                return true;


            }
            return false;
        }

        public static string LogErrors(this ModelStateDictionary modelState)
        {
            var modelStateErrors = modelState.Keys.SelectMany(key => modelState[key].Errors);
            StringBuilder sb = new StringBuilder();
            foreach (var p in modelStateErrors)
            {
                sb.Append(p.ErrorMessage + "--");
            }
            return sb.ToString();
        }

        //public List<string> GetModelStateErrors(ModelStateDictionary ModelState)
        //{
        //    List<string> errorMessages = new List<string>();

        //    var validationErrors = ModelState.Values.Select(x => x.Errors);
        //    validationErrors.ToList().ForEach(ve =>
        //    {
        //        var errorStrings = ve.Select(x => x.ErrorMessage);
        //        errorStrings.ToList().ForEach(em =>
        //        {
        //            errorMessages.Add(em);
        //        });
        //    });

        //    return errorMessages;
        //}

        public static string CustomValidationSummary(this IHtmlHelper html, bool excludePropertyErrors, string validationMessage)
        {

            if (!html.ViewData.ModelState.IsValid)
            {
                return "<div class=\"validation-summary\">" + html.ValidationSummary(excludePropertyErrors, validationMessage) + "</div>";
            }

            return "";
        }

        public static HtmlString CustomValidationSummary(this IHtmlHelper html, bool excludePropertyErrors)
        {

            if (!html.ViewData.ModelState.IsValid)
            {
                //return "<div class=\"validation-summary\">" + html.ValidationSummary(excludePropertyErrors,CommonSetting.ValidationSumarryHeader) + "</div>";
                string errorsStr = Errorsstr(html.ViewData.ModelState, excludePropertyErrors);

                if (String.IsNullOrEmpty(errorsStr) || String.IsNullOrWhiteSpace(errorsStr))
                    return new HtmlString("");
                //else
                return new HtmlString("<div class=\"alert alert-danger\">" + errorsStr + "</div>");
            }

            return new HtmlString("");
        }

        public static string CustomValidationSummaryWithCustomHeader(this IHtmlHelper html, bool excludePropertyErrors, string validationSummaryHeader)
        {
            
            if (!html.ViewData.ModelState.IsValid)
            {
                //return "<div class=\"validation-summary\">" + html.ValidationSummary(excludePropertyErrors,CommonSetting.ValidationSumarryHeader) + "</div>";
                string errorsStr = Errorsstr(html.ViewData.ModelState, excludePropertyErrors, false, validationSummaryHeader);

                if (String.IsNullOrEmpty(errorsStr) || String.IsNullOrWhiteSpace(errorsStr))
                    return String.Empty;
                //else
                return "<div class=\"alert alert-danger\">" + errorsStr + "</div>";
            }

            return "";
        }

    }//end class
}//end namespace
