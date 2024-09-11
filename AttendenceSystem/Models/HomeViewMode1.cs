using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendenceSystem.Controllers
{
    public class HomeViewMode1 : Controller
    {
        public int DepartmentCount {  get; set; }
        public int StudentCount { get; set; }

        public List<Student> Students { get; set; }
        public List<Department> Departments { get; set; }
    }
}