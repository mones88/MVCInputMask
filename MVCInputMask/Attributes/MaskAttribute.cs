using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInputMask.Attributes
{
    public class MaskAttribute : ValidationAttribute, IClientValidatable
    {
        public String Format { get; set; }

        public MaskAttribute(String format)
        {
            this.Format = format;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //qui andrebbe validato server-side
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule() {
                ErrorMessage = "",
                ValidationType = "maskui"
            };
            modelClientValidationRule.ValidationParameters.Add("inputmask", Format);
            yield return modelClientValidationRule;
        }
    }
}