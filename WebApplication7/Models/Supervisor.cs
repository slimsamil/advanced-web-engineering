using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{

    public partial class Supervisor
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Vorname"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Nachname"), Required]
        public string LastName { get; set; }

        [Display(Name = "Voller Name")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [Display(Name = "Aktiv"), Required]
        public bool Active { get; set; }

        [Display(Name = "Email"), Required, DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        public virtual ICollection<Thesis> Thesen { get; set; }



        }
    }

