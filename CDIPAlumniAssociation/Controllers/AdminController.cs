using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using CDIPAlumniAssociation.Context;
using CDIPAlumniAssociation.Models;
using Microsoft.Ajax.Utilities;

namespace CDIPAlumniAssociation.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private ApplicationContext db = new ApplicationContext();

        public ActionResult AddAdmin()
        {

            var admin = Session["admin"];

            if (admin.Equals(false))
            {
                return RedirectToAction("index", "Home");
            }

            
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            ViewBag.Message = null;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdmin(Admin admin)
        {
            string message = "";

            db.Admins.Add(admin);
            var rowAffected = db.SaveChanges();

            if (rowAffected > 0)
            {
                message = "Admin Account Registration successfully";
            }
            else
            {
                message = "Account Registration Failed";
            }

            ViewBag.Message = message;
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", admin.GenderId);

            return View();
        }





        public ActionResult AccountRegistrationRequest()
        {
            var admin = Session["admin"];
            if (admin.Equals(true))
            {
                var user = db.Users.Where(c => c.Approval == false).ToList();
                
                ViewBag.User = user;
            }


            ViewBag.Message = "";
            return View();
        }


        [HttpPost]
        public ActionResult AccountRegistrationRequest(int[] approvals)
        {
            string message = "";

            int count = 0;

            if (approvals != null)
            {
                foreach (var id in approvals)
                {
                    var user = db.Users.Find(id);
                    string pass = user.Password;
                    user.ConfirmPassword = pass;
                    user.Approval = true;
                    //user.ConfirmPassword = null;
                    db.Entry(user).State = EntityState.Modified;

                    try
                    {
                        var rowAffected = db.SaveChanges();

                        if (rowAffected > 0)
                        {
                            count++;
                        }
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string m = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(m, raise);
                            }
                        }
                        throw raise;
                    }

                }
            }

            if (count > 0)
            {
                message = count + " User Approval Successfull";
            }
            else
            {
                message = "No User Approved Yet !!";
            }
            ViewBag.Message = message;

            var admin = Session["admin"];
            if (admin.Equals(true))
            {
                var unapprovedUser = db.Users.Where(c => c.Approval == false).ToList();

                ViewBag.User = unapprovedUser;
            }

            return View();
        }


        public ActionResult JobPostRequest()
        {
            var admin = Session["admin"];
            if (admin.Equals(true))
            {
                var jobs = db.JobInfos.Where(c => c.Approval == false).ToList();

                ViewBag.Jobs = jobs;
            }

            ViewBag.Message = "";
            return View();

        }

        [HttpPost]
        public ActionResult JobPostRequest(int[] approvals)
        {
            string message = "";

            int count = 0;

            if (approvals != null)
            {
                foreach (var id in approvals)
                {
                    var jobs = db.JobInfos.Find(id);
                    
                    jobs.Approval = true;
                   
                    db.Entry(jobs).State = EntityState.Modified;

                    try
                    {
                        var rowAffected = db.SaveChanges();

                        if (rowAffected > 0)
                        {
                            count++;
                        }
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string m = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(m, raise);
                            }
                        }
                        throw raise;
                    }

                }
            }

            if (count > 0)
            {
                message = count + " Job Approval Successfull";
            }
            else
            {
                message = "No Job Approved Yet !!";
            }
            ViewBag.Message = message;

            var admin = Session["admin"];
            if (admin.Equals(true))
            {
                var unapprovedJobs = db.JobInfos.Where(c => c.Approval == false).ToList();

                ViewBag.User = unapprovedJobs;
            }

            return View();
        }

        public ActionResult ChangePassword()
        {
            
            var user = Session["user"] as User;
            if (user == null)
            {
                return RedirectToAction("index", "Home");
            }
            ViewBag.Message = null;

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Admin admin)
        {
            string message = "";
            var user = Session["user"] as User;
            var adminUser = Session["admin"];

            if (user.Password != admin.OldPassword)
            {
                message = "Your current password is not matched !! Try Again .";
            }
            else if (user.Password == admin.Password)
            {
                message = "This is your current Password, Use Different Password";
            }
            else if (user.Password == admin.OldPassword)
            {
                if (adminUser.Equals(true))
                {
                    var use = db.Admins.Find(user.Id);
                    use.Password = admin.Password;
                    use.ConfirmPassword = admin.Password;
                    use.OldPassword = user.Password;
                    db.Entry(use).State = EntityState.Modified;
                    var rowAffected = db.SaveChanges();
                    if (rowAffected > 0)
                    {
                        message = "Password Change Successfull..";
                    }
                    else
                    {
                        message = "Password Change Failed !!";
                    }
                }
                else
                {
                    var use = db.Users.Find(user.Id);
                    use.Password = admin.Password;
                    use.ConfirmPassword = admin.Password;
                    db.Entry(use).State = EntityState.Modified;
                    var rowAffected = db.SaveChanges();
                    if (rowAffected > 0)
                    {
                        message = "Password Change Successfull..";
                    }
                    else
                    {
                        message = "Password Change Failed !!";
                    }
                }
            }
            
            ViewBag.Message = message;
            return View();
        }


        public ActionResult UpdateJobStatus()
        {

            var user = Session["user"] as User;
            var admin = Session["admin"];

            if (user == null)
            {
                return RedirectToAction("index", "Home");
            }
            else if (admin.Equals(true))
            {
                return RedirectToAction("index", "Home");
            }

            var users = db.Users.Find(user.Id);
            ViewBag.Users = users;

            ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateJobStatus(User user)
        {
            string message = "";
            var userInfo = Session["user"] as User;

            var users = db.Users.Find(userInfo.Id);
            users.CurrentJobInfo = user.CurrentJobInfo;
            users.Password = userInfo.Password;
            users.ConfirmPassword = userInfo.Password;

            db.Entry(users).State = EntityState.Modified;

            var rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                message = "Job Status Update Successful..";
            }
            else
            {
                message = "Job Status Update Failed !! Please try again";
            }

            ViewBag.Users = users;
            ViewBag.Message = message;

            return View();
        }




        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!db.Admins.Any(c => c.Email == email), JsonRequestBehavior.AllowGet);
        }
	}
}