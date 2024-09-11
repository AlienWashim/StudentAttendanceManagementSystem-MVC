using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendenceSystem.Controllers
{
    public class HomeController : Controller
    {
        private SMEntities db = new SMEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Student Management System!";

            var viewModel1 = new HomeViewMode1
            {
                DepartmentCount = db.Departments.Count(),
                StudentCount = db.Students.Count(),
                Students = db.Students.ToList(),
                Departments = db.Departments.ToList(),

            };
            return View(viewModel1);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About the Student Management System";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";
            return View();
        }
    }
}
