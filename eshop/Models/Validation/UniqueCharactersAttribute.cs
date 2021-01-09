using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    public class UniqueCharactersAttribute : ValidationAttribute, IClientModelValidator
    {
        private int charactersCountRequired;

        public UniqueCharactersAttribute(int Characters)
        {
            charactersCountRequired = Characters;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = (string)value;
            int charCount = password.Distinct().Count();
            if (charCount >= charactersCountRequired)
            {
                return ValidationResult.Success;                
            }
            return new ValidationResult($"Password not met requirements. Unique characters at least {charactersCountRequired}!");
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-uniquecharacters", $"Unique characters must be at least {charactersCountRequired}!");
            context.Attributes.Add("data-val-uniquecharacters-characters", charactersCountRequired.ToString());
        }


    }
}
