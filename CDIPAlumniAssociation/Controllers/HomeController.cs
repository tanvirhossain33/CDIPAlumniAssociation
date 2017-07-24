using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CDIPAlumniAssociation.Context;
using CDIPAlumniAssociation.Models;

namespace CDIPAlumniAssociation.Controllers
{
    public class HomeController : Controller
    {

        ApplicationContext db = new ApplicationContext();

        public ActionResult Index()
        {
            return View();
        }


    }
}