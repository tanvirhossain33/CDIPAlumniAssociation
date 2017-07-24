using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CDIPAlumniAssociation.Models
{
    public class JobInfo
    {
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string JobNature { get; set; }

        [Required]
        public string EducationalRequirement { get; set; }

        [Required]
        public string ExperienceRequirement { get; set; }

        [Required]
        public string AdditionalRequirement { get; set; }

        [Required]
        public int NumberOfVacancies { get; set; }

        [Required]
        public string SalaryRange { get; set; }

        [Required]
        public string OtherBenefit { get; set; }

        [Required]
        public DateTime PublishTime { get; set; }

        [Required]
        public DateTime ApplicationDeadline { get; set; }

        [Required]
        public bool Approval { get; set; }

        

        public virtual List<AppliedJobInfo> AppliedJobInfos { get; set; }

        public virtual List<UserJobPostedInfo> UserJobPostedInfos { get; set; }
    }
}