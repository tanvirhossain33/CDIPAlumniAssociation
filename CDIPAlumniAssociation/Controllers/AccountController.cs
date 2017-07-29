using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CDIPAlumniAssociation.Models;
using CDIPAlumniAssociation.Context;

namespace CDIPAlumniAssociation.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db = new ApplicationContext();


        public ActionResult Login()
        {
            var registerUser = Session["user"] as User;

            if (registerUser != null)
            {
                return RedirectToAction("index", "Home");
            }

            ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            

            var any = db.Users.Any(c => c.Email == user.UserName && c.Password == user.Password && c.Approval == true);

            if (any)
            {
                var userInfo = db.Users.FirstOrDefault(c => c.Email == user.UserName);

                Session["user"] = new User()
                {
                    Id = userInfo.Id,
                    StudentId = userInfo.StudentId,
                    Name = userInfo.Name,
                    Address = userInfo.Address,
                    MobileNo = userInfo.MobileNo,
                    Email = userInfo.Email,
                    Password = userInfo.Password,
                    CurrentJobInfo = userInfo.CurrentJobInfo
                };

                return RedirectToAction("Index", "Home");
                
            }

            ViewBag.Message = "username and password is incorrect";

            return View();
        }

        public ActionResult Create()
        {

            var registerUser = Session["user"] as User;

            if (registerUser != null)
            {
                return RedirectToAction("index", "Home");
            }

            ViewBag.Programs = db.Programs.ToList();
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            ViewBag.Message = null;

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, HttpPostedFileBase file)
        {
            ViewBag.Programs = db.Programs.ToList();
            string message = " ";
            db.Users.Add(user);
            int saveConfirm = 0;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileType = Path.GetExtension(fileName);
                    if (fileType == ".png" || fileType == ".jpeg" || fileType == ".jpg" || fileType == ".gif")
                    {
                        fileName = user.Email + fileType;
                        var path = Path.Combine(Server.MapPath("~/UserImage"), fileName);
                        file.SaveAs(path);
                        saveConfirm = 1;
                    }
                    else
                    {
                        saveConfirm = 0;
                        message = "account create failed !! please upload png or jpeg or jpg or gif formate file";

                    }

                }
                catch (Exception ex)
                {
                    saveConfirm = 0;
                    message = "ERROR:" + ex.Message.ToString();
                }
            }

            else
            {
                saveConfirm = 0;
                message = "You have not specified a Image.";
            }

            if (saveConfirm == 1)
            {
                var rowChange = db.SaveChanges();
                if (rowChange > 0)
                {
                    message = "Account Created successfully";
                }
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);

            ViewBag.Message = message;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
       
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!db.Users.Any(c => c.Email == email), JsonRequestBehavior.AllowGet);
        }

        


        [HttpPost]
        public JsonResult GetBatchByProgram(int programId)
        {
            var batches = db.Batches;


            var batchList = batches.Where(a => a.ProgramId == programId).Select(c => new
            {
                id = c.Id,
                batchNumber = c.BatchNumber
            });
            return Json(batchList, JsonRequestBehavior.AllowGet);
        }


        

    }
}
