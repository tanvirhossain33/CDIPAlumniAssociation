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
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Admin", ErrorMessage = "Email is already exists.")]
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
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        //[Required]
        //[NotMapped]
        //[EmailAddress]
        //[DisplayName("Email")]
        //public string UserName { get; set; }



        public int GenderId { get; set; }


        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        public virtual List<UserJobPostedInfo> UserJobPostedInfos { get; set; }
    }
}