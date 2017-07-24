using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDIPAlumniAssociation.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "Email is already exists.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DisplayName("Confirm Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required]
        [NotMapped]
        [EmailAddress]
        //[Remote("IsEmailRegistered", "Account", ErrorMessage = "This Email is not Registered yet.")]
        [DisplayName("Email")]
        public string UserName { get; set; }

        
        public string CurrentJobInfo { get; set; }

        [Required]
        public bool Approval { get; set; }

        public int BatchId { get; set; }
        public int GenderId { get; set; }

        [ForeignKey("BatchId")]
        public virtual Batch Batch { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        
        public virtual List<AppliedJobInfo> AppliedJobInfos { get; set; }

        public virtual List<UserJobPostedInfo> UserJobPostedInfos { get; set; }

        
    }
}