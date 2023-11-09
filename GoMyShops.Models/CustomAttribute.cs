using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
//using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;

namespace GoMyShops.Models
{
    public class CustomAttribute
    {

    }//end class

    //public class AlphaSpaceAttributeAdapterProvider
    //   : ValidationAttributeAdapterProvider, IValidationAttributeAdapterProvider
    //{
    //    public AlphaSpaceAttributeAdapterProvider()
    //    {
    //    }

    //    IAttributeAdapter IValidationAttributeAdapterProvider.GetAttributeAdapter(
    //        ValidationAttribute attribute,
    //        IStringLocalizer stringLocalizer)
    //    {
    //        IAttributeAdapter adapter;
    //        if (attribute is RegularExpressionAttribute)
    //        {
    //            adapter = new AlphaSpaceAttribute((RegularExpressionAttribute)attribute, stringLocalizer);
    //        }            
    //        else
    //        {
    //            adapter = base.GetAttributeAdapter(attribute, stringLocalizer);
    //        }

    //        return adapter;
    //    }
    //}


    /*
  * Create by    : Harris Yer
  * Date         : 2016-11-24
  * Descriptions : Inherit the RegularExpressionAttribute to Overcome new AlphaSpaceAttribute.
  *              : 
  *              : 
  */
    public class AlphaSpaceAttribute :ValidationAttribute
    {
        private readonly string attributelocal = @"^([a-zA-Z ]*)\s*";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field is A-Z or space.";
        }

    }
    public class AlphaSpaceAttributeAdapter : AttributeAdapterBase<AlphaSpaceAttribute>
    {
        private readonly string attributelocal = @"^([a-zA-Z ]*)\s*";

        public AlphaSpaceAttributeAdapter(AlphaSpaceAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), @"The Field is A-Z and Space."
                );
        }

        //public AlphaSpaceAttribute()
        //    : base(@"^([a-zA-Z ]*)\s*")
        //{
        //}

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    MergeAttribute(context.Attributes, "data-val", "true");
        //    var errorMessage = GetErrorMessage(context);
        //    MergeAttribute(context.Attributes, "data-val-regex", errorMessage);
        //}

        //private bool MergeAttribute(
        //IDictionary<string, string> attributes,
        //string key,
        //string value)
        //{
        //    if (attributes.ContainsKey(key))
        //    {
        //        return false;
        //    }
        //    attributes.Add(key, value);
        //    return true;
        //}

        //private string GetErrorMessage(ClientModelValidationContext context)
        //{
        //    return "The Field is A-Z and Space.";
        //    //return string.Format("{0} The Field is A-Z and Space.",
        //    //    context.ModelMetadata.GetDisplayName());
        //}

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    if (value != null)
        //    {
        //        string alphaSpace = value.ToString();

        //        if (Regex.IsMatch(alphaSpace, @"^([a-zA-Z ]*)\s*", RegexOptions.IgnoreCase))
        //        {
        //            return ValidationResult.Success;
        //        }
        //        else
        //        {
        //            return new ValidationResult("The Field is A-Z or space.");
        //        }
        //    }
        //    else
        //    {
        //        return new ValidationResult("" + validationContext.DisplayName + " is required");
        //    }
        //}


      
    }


    /*
    * Create by    : Harris Yer
    * Date         : 2016-11-24
    * Descriptions : Inherit the RegularExpressionAttribute to Overcome new AlphaSpaceAttribute.
    *              : 
    *              : 
    */
    public class AlphaAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^([a-zA-Z]*)";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field is A-Z.";
        }

    }

    public class AlphaAttributeAdapter : AttributeAdapterBase<AlphaAttribute>
    {
        private readonly string attributelocal = @"^([a-zA-Z]*)";

        public AlphaAttributeAdapter(AlphaAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);

        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), "The Field is A-Z."
                );
        }
    }

    /*
  * Create by    : Harris Yer
  * Date         : 2017-12-19
  * Descriptions : Inherit the RegularExpressionAttribute to Overcome new IP Address.
  *              : 
  *              : 
  */
    public class IPAddressAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field is IP address";
        }

    }

    public class IPAddressAttributeAdapter : AttributeAdapterBase<IPAddressAttribute>
    {
        private readonly string attributelocal= @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        public IPAddressAttributeAdapter(IPAddressAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);

            //MergeAttribute(context.Attributes, "name", context.ModelMetadata.PropertyName);
            //MergeAttribute(context.Attributes, $"#{context.ModelMetadata.PropertyName}", string.Empty);
            //MergeAttribute(context.Attributes, "minlength", this.min);
        }
      
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), "Invalid IP Address"
                );
        }
    }

    /*
   * Create by    : Harris Yer
   * Date         : 2016-11-24
   * Descriptions : Inherit the RegularExpressionAttribute to Overcome new AlphaNumericAttribute.
   *              : 
   *              : 
   */

    public class AlphaNumericAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^[a-zA-Z0-9]+$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field is A-Z or 0-9.";
        }

    }
    public class AlphaNumericAttributeAdapter : AttributeAdapterBase<AlphaNumericAttribute>
    {
        private readonly string attributelocal = @"^[a-zA-Z0-9]+$";

        public AlphaNumericAttributeAdapter(AlphaNumericAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);

            //MergeAttribute(context.Attributes, "name", context.ModelMetadata.PropertyName);
            //MergeAttribute(context.Attributes, $"#{context.ModelMetadata.PropertyName}", string.Empty);
            //MergeAttribute(context.Attributes, "minlength", this.min);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), "The Field is A-Z or 0-9."
                );
        }
    }

    /*
  * Create by    : Harris Yer
  * Date         : 2016-11-24
  * Descriptions : Inherit the RegularExpressionAttribute to Overcome new AlphaNumericAttribute.
  *              : 
  *              : 
  */
    public class AlphaNumericSpaceAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^[a-zA-Z0-9 ]+$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " is A-Z or 0-9 or Space.";
        }

    }
    public class AlphaNumericSpaceAttributeAdapter : AttributeAdapterBase<AlphaNumericSpaceAttribute>
    {
        private readonly string attributelocal = @"^[a-zA-Z0-9 ]+$";

        public AlphaNumericSpaceAttributeAdapter(AlphaNumericSpaceAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), "The Field is A-Z or 0-9 or Space."
                );
        }
    }

    /*
 * Create by    : Harris Yer
 * Date         : 2016-11-24
 * Descriptions : Inherit the RegularExpressionAttribute to Overcome new DescriptionsAttribute.
 *              : Any Character except `^{}\'"""
 *              : 
 */
    public class DescriptionsAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^[^`{}\\'\^\""]+$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + @" is any character except `^{}\'""";
        }

    }
    public class DescriptionsAttributeAdapter : AttributeAdapterBase<DescriptionsAttribute>
    {
        private readonly string attributelocal = @"^[^`{}\\'\^\""]+$";

        public DescriptionsAttributeAdapter(DescriptionsAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), @"Any Character except `^{}\'"""
                );
        }
    }

    /*
* Create by    : Harris Yer
* Date         : 2016-11-28
* Descriptions : Inherit the RegularExpressionAttribute to Overcome new DescriptionsAttribute.
*              : The Field is Positive Integer with first digit to be 1-9."
*              : 
*/
    public class PositiveIntegerAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^\d+$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return ValidationResult.Success;
                // return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + @" is positive integer.";
        }

    }
    public class PositiveIntegerAttributeAdapter : AttributeAdapterBase<PositiveIntegerAttribute>
    {
        private readonly string attributelocal = @"^\d+$";

        public PositiveIntegerAttributeAdapter(PositiveIntegerAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), @"The Field is Positive Integer."
                );
        }
    }

    /*
* Create by    : Harris Yer
* Date         : 2016-11-28
* Descriptions : Inherit the RegularExpressionAttribute to Overcome new DescriptionsAttribute.
*              : The Field is Positive Integer with first digit to be 1-9."
*              : 
*/
    public class PositiveIntegerFirstDigitAttribute : ValidationAttribute
    {
        private readonly string attributelocal = @"^[1-9]\d*$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string alphaSpace = value.ToString();

                if (Regex.IsMatch(alphaSpace, attributelocal, RegexOptions.CultureInvariant))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + @" is positive integer with first digit to be 1-9";
        }

    }
    public class PositiveIntegerFirstDigitAttributeAdapter : AttributeAdapterBase<PositiveIntegerFirstDigitAttribute>
    {
        private readonly string attributelocal = @"^[1-9]\d*$";

        public PositiveIntegerFirstDigitAttributeAdapter(PositiveIntegerFirstDigitAttribute attribute, IStringLocalizer stringLocalizer) :
            base(attribute, stringLocalizer)
        {
            //this.attributelocal = this.Attribute.Pattern.ToString(CultureInfo.InvariantCulture);
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }//end if

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-regex", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-regex-pattern", attributelocal);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }//end if

            //return "Invalid IP Address";
            return this.GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(), @"The Field is Positive Integer with first digit to be 1-9."
                );
        }
    }


    /// <summary>
    /// // simple annotation attribute
    /// [ExcludeChar("/.,!@#$%")]
    /// // overriding the error message of data annotation attribute
    /// [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid character.")]
    /// </summary>
    public class ExcludeChar : ValidationAttribute
    {
        private readonly string _chars;

        public ExcludeChar(string chars)
            : base("{0} contains invalid character.")
        {
            _chars = chars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                for (int i = 0; i < _chars.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(_chars[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

}//end namespace
