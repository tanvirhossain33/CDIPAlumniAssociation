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
    public class AlumniController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        
        

        public ActionResult Create()
        {
            ViewBag.ProgramId = new SelectList(db.Programs, "Id", "Name");
            ViewBag.Users = null;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,BatchNumber,ProgramId")] Batch batch)
        {
            

            if (batch != null)
            {
                
                var list = db.Users.Where(c => c.Batch.ProgramId == batch.ProgramId).ToList();
                ViewBag.Users = list;
            }

            ViewBag.ProgramId = new SelectList(db.Programs, "Id", "Name", batch.ProgramId);
            return View(batch);
        }

        public ActionResult Details(int id)
        {
            var details = db.Users.Where(c => c.Id == id).ToList();

            ViewBag.UserDetails = details;

            return View();
        }



        public ActionResult UploadResume()
        {

            var registerUser = Session["user"] as User;

            if (registerUser == null)
            {
                return RedirectToAction("Index", "Home");
                
            }

            ViewBag.Message = null;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadResume(HttpPostedFileBase file)
        {
            var registerUser = Session["user"] as User;

            int userId = registerUser.Id;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileType = Path.GetExtension(fileName);
                    if (fileType != ".pdf")
                    {
                        ViewBag.Message = "upload failed !! please upload PDF formate file";
                    }
                    else
                    {
                        fileName = userId + fileType;
                        var path = Path.Combine(Server.MapPath("~/Content/Resume"), fileName);
                        file.SaveAs(path);
                        ViewBag.message = "Resume uploaded successfully";
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.message = "ERROR:" + ex.Message.ToString();
                }
            }

            else
            {
                ViewBag.message = "You have not specified a file.";
            }
            return View();

        }

        public ActionResult ViewResume()
        {
            var session = Session["user"] as User;
            if (session == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var message = TempData["Message"];

            ViewBag.Message = message;

            return View();
        }

        public ActionResult DownloadResume()
        {
            var session = Session["user"] as User;
            if (session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = session.Id;
            string name = session.Name;
            var fileName = name + ".pdf";

            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Content/Resume"));
            System.IO.FileInfo[] fileNames = dir.GetFiles(userId + ".pdf");




            if (fileNames.Length == 0)
            {
                TempData["Message"] = "Resume Not Uploaded Yet !! Please upload your Resume ..";
                return RedirectToAction("ViewResume", "Alumni");
            }
            else
            {
                var data = File("~/Content/Resume" + userId + ".pdf", System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                return data;
            }
            
        }


        public ActionResult DisplayPDF()
        {
            var session = Session["user"] as User;
            if (session == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int userId = session.Id;

            var fileName = userId + ".pdf";

            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Content/Resume"));
            System.IO.FileInfo[] fileNames = dir.GetFiles(userId + ".pdf");

            if (fileNames.Length == 0)
            {
                TempData["Message"] = "Resume Not Uploaded Yet !! Please upload your Resume ..";
                return RedirectToAction("ViewResume", "Alumni");
            }
            else
            {
                return File(dir.ToString() + userId + ".pdf", "application/pdf");
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
