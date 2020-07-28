using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public partial class Programme
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        public virtual ICollection<Thesis> Thesen { get; set; }
    }
}
