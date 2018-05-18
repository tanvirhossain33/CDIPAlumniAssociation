using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDIPAlumniAssociation.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }

        public virtual List<Admin> Admins { get; set; }
    }
}