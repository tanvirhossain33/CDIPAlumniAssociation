using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CDIPAlumniAssociation.Models;
using CDIPAlumniAssociation.Context;

namespace CDIPAlumniAssociation.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Batch).Include(u => u.Gender);
            return View(users.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        public ActionResult Create()
        {

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

        
       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "BatchNumber", user.BatchId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,StudentId,Name,Address,MobileNo,Email,Password,CurrentJobInfo,Approval,BatchId,GenderId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "BatchNumber", user.BatchId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
