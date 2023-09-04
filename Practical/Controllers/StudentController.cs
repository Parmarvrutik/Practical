using Practical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical.Controllers
{
    public class StudentController : Controller
    {

        // GET: Student
        public ActionResult Index(string departmentFilter)
        {
            StudentDBHandle sdb = new StudentDBHandle();

            ViewBag.DeptList = sdb.getDropdownDept(); // Populate the department dropdown

            if (string.IsNullOrEmpty(departmentFilter) || departmentFilter == "All Departments")
            {
                ModelState.Clear();
                return View(sdb.GetStudent());
            }
            else
            {
                List<Students> students = sdb.GetStudentByDepartment(departmentFilter); // Modify this method in your StudentDBHandle
                return View(students);
            }
        }


        // GET: Student/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: Student/Create
        public ActionResult Create()
        {
            StudentDBHandle sdb = new StudentDBHandle();
            ViewBag.Department = sdb.getDropdownDept();
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Students student)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    StudentDBHandle sdb = new StudentDBHandle();
                    ViewBag.Department = sdb.getDropdownDept();
                    if (sdb.AddStudent(student))
                    {
                        ViewBag.Message = "<script>alert('Student Details Added Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int Stud_ID)
        {
            StudentDBHandle sdb = new StudentDBHandle();
            ViewBag.Department = sdb.getDropdownDept();
            return View(sdb.GetStudent().Find(StudentModel => StudentModel.Stud_ID == Stud_ID));
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int Stud_ID, Students student)
        {
            try
            {
                // TODO: Add update logic here
                StudentDBHandle sdb = new StudentDBHandle();
                ViewBag.Department = sdb.getDropdownDept();
                sdb.UpdateDetails(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int Stud_ID)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                if (sdb.DeleteStudent(Stud_ID))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*// POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        public JsonResult getCourse(int id)
        {
            List<SelectListItem> list_course;
            StudentDBHandle sdb = new StudentDBHandle();
            list_course = sdb.getDropdownCourse(id);
            return Json(new SelectList(list_course, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Students student)
        {
            StudentDBHandle sdb = new StudentDBHandle();
            var row = sdb.GetStudent().Find(m => m.Stud_name == student.Stud_name && m.Email == student.Email);
            if (row == null)
            {
                ViewBag.LoginMessage = "<script>alert('Invalid credentials')</script>";
                return View();
            }
            else
            {
                ViewBag.LoginMessage = "<script>alert('Login Successful')</script>";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Register()
        {
            StudentDBHandle sdb = new StudentDBHandle();
            ViewBag.Department = sdb.getDropdownDept();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Students student)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                ViewBag.Department = sdb.getDropdownDept(); sdb.AddStudent(student);
                ViewBag.RMessage = "<script>alert('Register Successful')</script>";
                return RedirectToAction("Login");

            }
            catch
            {
                return View();
            }
        }
    }
}