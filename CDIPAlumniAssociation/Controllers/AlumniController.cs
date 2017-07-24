using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
                //var list = db.Users.Include(c => c.Batch);
                //list = list.Where(c => c.Batch.ProgramId == batch.ProgramId);

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
