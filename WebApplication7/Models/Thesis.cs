using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
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


        public partial class Thesis
        {
            [Display(Name = "Id")]
            public int Id { get; set; }

            [Display(Name = "Titel"), Required(ErrorMessage = "Es muss ein Titel angegeben werden.")]
            public string Title { get; set; }

            //mehrzeiliger Text
            [Display(Name = "Beschreibung"), Required(ErrorMessage = "Es muss eine Beschreibung angegeben werden.")]
            public string Description { get; set; }

            [Display(Name = "Betreuer/-in")]
            public int? SupervisorId { get; set; }
            public Supervisor Supervisor { get; set; }

            [Display(Name = "Bachelor")]
            public bool Bachelor { get; set; }

            [Display(Name = "Master")]
            public bool Master { get; set; }

            [Display(Name = "Status"), Required]
            public Status Status { get; set; }

            [Display(Name = "Name des Studenten"), RequiredIf(new String[] { "StudentId" })]
            public string StudentName { get; set; }

            //Matrikelnummer
            [Display(Name = "Matrikelnummer"), Range(1000000, 9999999, ErrorMessage = "Die Matrikelnummer muss 7-stellig sein."), RequiredIf(new String[] { "StudentName" })]
            public int? StudentId { get; set; }

             [Display(Name = "Studiengang")]
             public int? ProgrammeId { get; set; }
    
            [Display(Name = "Studiengang")]
            public Programme Programme { get; set; }

            //Anmeldedatum
            [Display(Name = "Anmeldedatum"), DataType(DataType.Date)]
            public DateTime? Registration { get; set; }

            //Abgabedatum
            [Display(Name = "Abgabedatum"), DataType(DataType.Date)]
            public DateTime? Filing { get; set; }

            [Display(Name = "Typ")]
            public Typ? Type { get; set; }

            //mehrzeiliger Text
            [Display(Name = "Zusammenfassung")]
            public string Summary { get; set; }

            //mehrzeiliger Text
            [Display(Name = "Stärken")]
            public string Strenghts { get; set; }

            //mehrzeiliger Text
            [Display(Name = "Schwächen")]
            public string Weaknesses { get; set; }

            //mehrzeiliger Text
            [Display(Name = "Evaluation")]
            public string Evaluation { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Inhaltsbewertung")]
            public int? ContentVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Layoutbewertung")]
            public int? LayoutVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Strukturbewertung")]
            public int? StructureVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Stylebewertung")]
            public int? StyleVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Literaturbewertung")]
            public int? LiteratureVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Schwierigkeitsbewertung")]
            public int? DifficultyVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Neuheitsbewertung")]
            public int? NoveltyVal { get; set; }

            [Range(1, 5, ErrorMessage = "Nur Werte von 1 bis 5."), Display(Name = "Reichhaltigskeitsbewertung")]
            public int? RichnessVal { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Inhaltsgewichtung")]
            public int ContentWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Layoutgewichtung")]
            public int LayoutWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Strukturgewichtung")]
            public int StrucutreWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Stylegewichtung")]
            public int StyleWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Literaturgewichtung")]
            public int LiteratureWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Schwierigkeitsgewichtung")]
            public int DifficultyWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Neuheitsgewichtung")]
            public int NoveltyWt { get; set; }

            [Range(0, 100, ErrorMessage = "Nur Werte von 0 bis 100."), Display(Name = "Reichhaltigkeitsgewichtung")]
            public int RichnessWt { get; set; }

            [Range(100, 100, ErrorMessage = "Die Gewichtung muss 100 ergeben."), Display(Name = "Summe der Gewichtungen")]
            public int? WeightSum { get; set; }

            [Display(Name = "Note")]
            public double? Grade { get; set; }

            [Display(Name = "Zuletzt bearbeitet"), DataType(DataType.Date)]
            public DateTime? LastModified { get; set; }

            public string PdfPath { get; set; }

            public string SupervisorEMail { get; set; }

        public string SupervisorName{ get; set; }


    }
}

