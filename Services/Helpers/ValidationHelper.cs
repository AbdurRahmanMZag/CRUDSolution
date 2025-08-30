using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Helpers
{
    internal static class ValidationHelper
    {
        public static void ModelValidation(object obj)
        {
            //Model validations
            ValidationContext validationContext = new( obj );
            List<ValidationResult> results = [];
            bool isValid = Validator.TryValidateObject ( obj, validationContext, results, true );
            if (!isValid)
            {
                throw new ArgumentException ( results.FirstOrDefault ( )?.ErrorMessage );
            }
        }
    }
}
