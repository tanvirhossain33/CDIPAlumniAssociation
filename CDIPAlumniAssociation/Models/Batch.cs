using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CDIPAlumniAssociation.Models
{
    public class Batch
    {
        public int Id { get; set; }

        [Required]
        public string BatchNumber { get; set; }

        public int ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public virtual Program Program { get; set; }

        public virtual List<User> Users { get; set; }
    }
}