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

        public ActionResult Create()
        {
            var registerUser = Session["user"] as User;

            if (registerUser == null)
            {
                return RedirectToAction("index", "Home");
            }


            ViewBag.Message = null;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobInfo jobinfo)
        {
            var registerUser = Session["user"] as User;

            var job = new JobInfo()
            {
                JobTitle = jobinfo.JobTitle,
                JobNature = jobinfo.JobNature,
                EducationalRequirement = jobinfo.EducationalRequirement,
                ExperienceRequirement = jobinfo.ExperienceRequirement,
                JobRequirement = jobinfo.JobRequirement,
                NumberOfVacancies = jobinfo.NumberOfVacancies,
                SalaryRange = jobinfo.SalaryRange,
                OtherBenefit = jobinfo.OtherBenefit,
                PublishTime = DateTime.Today,
                ApplicationDeadline = jobinfo.ApplicationDeadline,
                Approval = true,
                CompanyName = jobinfo.CompanyName
            };

            db.JobInfos.Add(job);
            
            var rowChanged = db.SaveChanges();

            if (rowChanged > 0)
            {

                var userJobPostedInfo = new UserJobPostedInfo()
                {
                    JobInfoId = job.Id,
                    UserId = registerUser.Id
                };

                db.UserJobPostedInfos.Add(userJobPostedInfo);
                var changedRow = db.SaveChanges();
                if (changedRow > 0)
                {
                    ViewBag.Message = "Job Posted  successfully";
                }
                else
                {
                    ViewBag.Message = "Job Post not  successful ! Please try again ...";
                }
                
            }
            else
            {
                ViewBag.Message = "Job Post not  successful ! Please try again ...";
            }

            return View(jobinfo);
        }

        public ActionResult ViewAllPostedJob()
        {
            var jobs = db.JobInfos.ToList();

            ViewBag.Jobs = jobs;
            ViewBag.JobCount = jobs.Count;

            return View();
        }

        public ActionResult JobDetails(int id)
        {
            var registerUser = Session["user"] as User;

            var jobs = db.JobInfos.Find(id);

            var identity = db.UserJobPostedInfos.First(c => c.JobInfoId == id);

            ViewBag.CurrentJobOwnerId = identity.UserId;
            ViewBag.jobs = jobs;
            if (registerUser != null)
            {
                var appliedForThisJob = db.AppliedJobInfos.FirstOrDefault(c => c.UserId == registerUser.Id && c.JobInfoId == id);
                ViewBag.AppliedForThisJob = appliedForThisJob;
            }
            

            return View();
        }


        public ActionResult ApplyJobs(int id)
        {
            var registerUser = Session["user"] as User;

            var identity = db.UserJobPostedInfos.First(c => c.JobInfoId == id);

            if (registerUser != null && registerUser.Id != identity.UserId)
            {
                var jobs = db.JobInfos.First(c => c.Id == id);

                ViewBag.Jobs = jobs;
                ViewBag.Message = null;
            }
            else
            {
                return RedirectToAction("JobDetails", new {id = id});
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult ApplyJobs(AppliedJobInfo appliedJobInfo, int id)
        {
            var registerUser = Session["user"] as User;
            AppliedJobInfo application = new AppliedJobInfo()
            {
                CoverLetter = appliedJobInfo.CoverLetter,
                ExpectedSalary = appliedJobInfo.ExpectedSalary,
                AppliedTime = DateTime.Today,
                JobInfoId = id,
                UserId = registerUser.Id
            };

            db.AppliedJobInfos.Add(application);

            var rowChange = db.SaveChanges();

            if (rowChange > 0)
            {
                ViewBag.Message = "congratulation, Your Job Application Successful..";
            }
            else
            {
                ViewBag.Message = "Application Unsuccessful !! Please try again...";
            }

            var jobs = db.JobInfos.First(c => c.Id == id);
            ViewBag.Jobs = jobs;

            return View();
        }

        public ActionResult PersonalJobApplication()
        {

            var registerUser = Session["user"] as User;

            if (registerUser != null)
            {
                var jobs = db.AppliedJobInfos.Where(c => c.UserId == registerUser.Id).ToList();
                ViewBag.Jobs = jobs;
                ViewBag.JobCount = jobs.Count;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           

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
