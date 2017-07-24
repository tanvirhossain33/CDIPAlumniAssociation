using CDIPAlumniAssociation.Models;

namespace CDIPAlumniAssociation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CDIPAlumniAssociation.Context.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CDIPAlumniAssociation.Context.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Users.AddOrUpdate(
            //    new User { StudentId = "1111", Name = "Tanvir", Address = "Dhanmondi", MobileNo = "01817408048", Email = "tanvir@gmail.com", Password = "123456", Approval = true, BatchId = 1, GenderId = 1 },
            //    new User {StudentId = "2222", Name = "Tuhin", Address = "Dhanmondi", MobileNo = "01817408048", Email = "tuhin@gmail.com", Password = "123456", Approval = true, BatchId = 1, GenderId = 1 },
            //    new User {StudentId = "3333", Name = "Ali", Address = "Dhanmondi", MobileNo = "01817408048", Email = "ali@gmail.com", Password = "123456", CurrentJobInfo = "Instructor At UIU", Approval = true, BatchId = 1, GenderId = 1},
            //    new User { StudentId = "4444", Name = "Shabu", Address = "Mohakhali", MobileNo = "01817408048", Email = "shabu@gmail.com", Password = "123456", CurrentJobInfo = "Instructor At UIU", Approval = true, BatchId = 1, GenderId = 1 },
            //    new User {StudentId = "5555", Name = "Abir", Address = "Mohakhali", MobileNo = "01817408048", Email = "abir@gmail.com", Password = "123456", Approval = true, BatchId = 1, GenderId = 1 },
            //    new User {StudentId = "6666", Name = "Galib", Address = "Mohakhali", MobileNo = "01817408048", Email = "galib@gmail.com", Password = "123456", Approval = true, BatchId = 1, GenderId = 1 },
            //    new User { StudentId = "7777", Name = "Junayed", Address = "Uttara", MobileNo = "01817408048", Email = "junayed@gmail.com", Password = "123456", CurrentJobInfo = "Junior Software Engineer at Centre for Energy Research", Approval = true, BatchId = 2, GenderId = 1 },
            //    new User {StudentId = "8888", Name = "Zisan", Address = "Uttara", MobileNo = "01817408048", Email = "zisan@gmail.com", Password = "123456", Approval = true, BatchId = 2, GenderId = 1 },
            //    new User {StudentId = "9999", Name = "Quraishy", Address = "Uttara", MobileNo = "01817408048", Email = "quraishy@gmail.com", Password = "123456", Approval = true, BatchId = 4, GenderId = 1 },
            //    new User {StudentId = "2111", Name = "Salauddin", Address = "Azimpur", MobileNo = "01817408048", Email = "salauddin@gmail.com", Password = "123456", Approval = true, BatchId = 4, GenderId = 1},
            //    new User {StudentId = "3111", Name = "Aditto", Address = "Azimpur", MobileNo = "01817408048", Email = "aditto@gmail.com", Password = "123456", Approval = true, BatchId = 4, GenderId = 1 },
            //    new User {StudentId = "4111", Name = "Mushfiq", Address = "Azimpur", MobileNo = "01817408048", Email = "mushfiq@gmail.com", Password = "123456", Approval = true, BatchId = 4, GenderId = 1 },
            //    new User {StudentId = "5111", Name = "Shanta", Address = "Banani", MobileNo = "01817408048", Email = "shanta@gmail.com", Password = "123456", Approval = true, BatchId = 5, GenderId = 2},
            //    new User {StudentId = "6111", Name = "Chaity", Address = "Banani", MobileNo = "01817408048", Email = "chaity@gmail.com", Password = "123456", Approval = true, BatchId = 5, GenderId = 2 },
            //    new User {StudentId = "7111", Name = "Riya", Address = "Banani", MobileNo = "01817408048", Email = "riya@gmail.com", Password = "123456", Approval = true, BatchId = 5, GenderId = 2 }
                
            //    );
            
        }
    }
}
