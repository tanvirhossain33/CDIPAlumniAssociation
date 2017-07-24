using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CDIPAlumniAssociation.Models
{
    public class AppliedJobInfo
    {
        public int Id { get; set; }

        [Required]
        public string CoverLetter { get; set; }

        [Required]
        public int ExpectedSalary { get; set; }

        [Required]
        public DateTime AppliedTime { get; set; }

        public int JobInfoId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("JobInfoId")]
        public virtual JobInfo JobInfo { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}