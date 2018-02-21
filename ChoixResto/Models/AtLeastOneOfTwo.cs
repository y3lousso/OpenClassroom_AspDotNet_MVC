﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ChoixRestaurant.Models
{
    public class AtLeastOneOfTwo : ValidationAttribute, IClientValidatable
    {
        public string Parametre1 { get; set; }
        public string Parametre2 { get; set; }

        public AtLeastOneOfTwo() : base("Vous devez saisir au moins un moyen de contacter le restaurant")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo[] proprietes = validationContext.ObjectType.GetProperties();
            PropertyInfo info1 = proprietes.FirstOrDefault(p => p.Name == Parametre1);
            PropertyInfo info2 = proprietes.FirstOrDefault(p => p.Name == Parametre2);

            string valeur1 = info1.GetValue(validationContext.ObjectInstance) as string;
            string valeur2 = info2.GetValue(validationContext.ObjectInstance) as string;

            if (string.IsNullOrWhiteSpace(valeur1) && string.IsNullOrWhiteSpace(valeur2))
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule regle = new ModelClientValidationRule();
            regle.ValidationType = "verifcontact";
            regle.ErrorMessage = ErrorMessage;
            regle.ValidationParameters.Add("parametre1", Parametre1);
            regle.ValidationParameters.Add("parametre2", Parametre2);
            return new List<ModelClientValidationRule> { regle };
        }
    }
}
  