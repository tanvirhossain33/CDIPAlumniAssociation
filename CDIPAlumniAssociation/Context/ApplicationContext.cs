using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CDIPAlumniAssociation.Models;

namespace CDIPAlumniAssociation.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Program> Programs { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JobInfo> JobInfos { get; set; }
        public DbSet<AppliedJobInfo> AppliedJobInfos { get; set; }

        public DbSet<UserJobPostedInfo> UserJobPostedInfos { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Admin> Admins { get; set; }

    }
}