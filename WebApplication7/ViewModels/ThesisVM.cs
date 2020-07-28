using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;

namespace WebApplication7.ViewModels
{
    public enum Status
    {
        [Display(Name = "Frei")]
        Frei = 0,
        [Display(Name = "Vorgemerkt")]
        Vorgemerkt = 1,
        [Display(Name = "Angemeldet")]
        Angemeldet = 2,
        [Display(Name = "Abgegeben")]
        Abgegeben = 3,
        [Display(Name = "Bewertet")]
        Bewertet = 4
    }

    //requiredif Funktionalität
    public class RequiredIfAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessageFormatString = "The {0} field is required.";
        private readonly string[] _dependentProperties;

        public RequiredIfAttribute(string[] dependentProperties)
        {
            _dependentProperties = dependentProperties;
            ErrorMessage = DefaultErrorMessageFormatString;
        }

        protected override ValidationResult IsValid(Object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();

            foreach (string s in _dependentProperties)
            {
                Object propertyValue = type.GetProperty(s).GetValue(instance, null);
                if (propertyValue == null || value != null)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Bitte " + context.DisplayName + " des Studenten angeben. ");
        }
    }




    //Deutsche Begriffe im Status
    public static class ErrorLevelExtensions
    {
        public static string ToFriendlyString(this Status me)
        {
            switch (me)
            {
                case Status.Frei:
                    return "Frei";
                case Status.Vorgemerkt:
                    return "Vorgemerkt";
                case Status.Angemeldet:
                    return "Angemeldet";
                case Status.Abgegeben:
                    return "Abgegeben";
                case Status.Bewertet:
                    return "Bewertet";
                default:
                    return "Get your damn dirty hands off me you FILTHY APE!";
            }
        }
    }

    public enum Typ
    {
        [Display(Name = "Bachelor")]
        Bachelor = 0,
        [Display(Name = "Master")]
        Master = 1

    }
    public class ThesisVM : Thesis
    {
        public int Id { get; set; }
        public IFormFile Pdf { get; set; }
    }
}
