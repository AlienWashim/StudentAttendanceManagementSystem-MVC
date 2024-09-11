using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AttendenceSystem;

namespace AttendenceSystem.Controllers
{
    public class AttendencesController : Controller
    {
        private SMEntities db = new SMEntities();

        // GET: Attendences
        public ActionResult Index()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Attendences/GetStudents
        [HttpPost]
        public ActionResult GetStudents(int departmentId)
        {
            var students = db.Students.Where(s => s.departmentId == departmentId).ToList();
            return PartialView("_AttendanceTable", students);
        }



        [HttpPost]
        public ActionResult CheckAttendance(int departmentId, DateTime attendanceDate)
        {
            var exists = db.Attendences.Any(a => a.attendenceDate == attendanceDate && a.Student.departmentId == departmentId);
            return Json(new { exists });
        }


        [HttpPost]
        public ActionResult GetAttendance(int departmentId, DateTime attendanceDate)
        {
            var attendances = db.Attendences.Where(a => a.attendenceDate == attendanceDate && a.Student.departmentId == departmentId).ToList();
            return PartialView("_AttendanceEditTable", attendances);
        }

        [HttpPost]
        public ActionResult SaveAttendance(List<Attendence> attendances)
        {
            try
            {
                foreach (var attendance in attendances)
                {
                    // Check if there is existing attendance data for the specified date
                    var existingAttendance = db.Attendences.FirstOrDefault(a => a.attendenceDate == attendance.attendenceDate && a.studentId == attendance.studentId);

                    if (existingAttendance != null)
                    {
                        // Update existing attendance record
                        existingAttendance.isPresent = attendance.isPresent;
                        db.Entry(existingAttendance).State = EntityState.Modified;
                    }
                    else
                    {
                        // Insert new attendance record
                        db.Attendences.Add(attendance);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ModelState.AddModelError("", "An error occurred while saving attendance data." + ex);
                // Return to the view with the current model state
                return View("YourView", attendances);
            }
            return RedirectToAction("Index");
        }






        // Other existing actions...

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
