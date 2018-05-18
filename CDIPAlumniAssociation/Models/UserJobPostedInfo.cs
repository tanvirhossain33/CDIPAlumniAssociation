using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CDIPAlumniAssociation.Models
{
    public class UserJobPostedInfo
    {
        public int Id { get; set; }

        public int JobInfoId { get; set; }

        public int? UserId { get; set; }

        public int? AdminId { get; set; }

        [ForeignKey("JobInfoId")]
        public virtual JobInfo JobInfo { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }

    }
}