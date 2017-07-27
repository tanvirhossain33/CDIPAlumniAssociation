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
        [Display(Name = "Position")]
        public string JobTitle { get; set; }

        [Required]
        [Display(Name = "Job Nature")]
        public string JobNature { get; set; }

        [Required]
        [Display(Name = "Educational Requirement")]
        public string EducationalRequirement { get; set; }

        [Required]
        [Display(Name = "Experience Requirement")]
        public string ExperienceRequirement { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Job Requirement")]
        public string JobRequirement { get; set; }


        [Required]
        [Display(Name = "Vacancies")]
        public int NumberOfVacancies { get; set; }

        [Required]
        [Display(Name = "Salary Range")]
        public string SalaryRange { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Other Benefit")]
        public string OtherBenefit { get; set; }

        [Required]
        [Display(Name = "Publish Time")]
        public DateTime PublishTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Application Deadline")]
        public DateTime ApplicationDeadline { get; set; }

        [Required]
        public bool Approval { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        

        public virtual List<AppliedJobInfo> AppliedJobInfos { get; set; }

        public virtual List<UserJobPostedInfo> UserJobPostedInfos { get; set; }
    }
}