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
    public class JobController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        
        public ActionResult Index()
        {
            return View(db.JobInfos.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobInfo jobinfo = db.JobInfos.Find(id);
            if (jobinfo == null)
            {
                return HttpNotFound();
            }
            return View(jobinfo);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,JobTitle,JobNature,EducationalRequirement,ExperienceRequirement,AdditionalRequirement,NumberOfVacancies,SalaryRange,OtherBenefit,PublishTime,ApplicationDeadline,Approval")] JobInfo jobinfo)
        {
            if (ModelState.IsValid)
            {
                db.JobInfos.Add(jobinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobinfo);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobInfo jobinfo = db.JobInfos.Find(id);
            if (jobinfo == null)
            {
                return HttpNotFound();
            }
            return View(jobinfo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,JobTitle,JobNature,EducationalRequirement,ExperienceRequirement,AdditionalRequirement,NumberOfVacancies,SalaryRange,OtherBenefit,PublishTime,ApplicationDeadline,Approval")] JobInfo jobinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobinfo);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobInfo jobinfo = db.JobInfos.Find(id);
            if (jobinfo == null)
            {
                return HttpNotFound();
            }
            return View(jobinfo);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobInfo jobinfo = db.JobInfos.Find(id);
            db.JobInfos.Remove(jobinfo);
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
    }
}
