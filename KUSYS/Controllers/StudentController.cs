using BusinessLayer.Abstract;
using CoreLayer.Enums;
using EntityLayer;
using KUSYS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService studentService;
        private ICourseService courseService;
        private IUserService userService;

        public StudentController(IStudentService studentService, ICourseService courseService, IUserService userService)
        {
            this.studentService = studentService;
            this.courseService = courseService;
            this.userService = userService;
        }
        [HttpGet("createstudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateStudent()
        {
            ViewBag.courses = courseService.GetAllCourses().Select(x => new SelectListItem() { Value = x.CourseId, Text = x.CourseName });

            return View();
        }
        [HttpPost("createstudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateStudent(Student model)
        {
            try
            {
                var student = new Student()
                {
                    StudentId = model.StudentId,
                    CourseId = model.CourseId,
                    BirthDate = model.BirthDate,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                 studentService.CreateStudent(student);
               
                return Redirect("studentslist");
            }
            catch (Exception ex)
            {
                return View();

            }


        }

        [HttpGet("editstudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditStudent(int id)
        {
            var student = studentService.GetStudentById(id);
            User user = userService.GetAllUsers().FirstOrDefault(x => x.StudentId == id);
            var model = new StudentUserViewModel()
            {
                StudentId = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                IsUser = user != null,
                UserId = user != null ? user.UserId : 0,
                BirthDate = student.BirthDate,
                CourseId = student.CourseId,
                Password = user != null ? user.Password : String.Empty,
                UserName = user != null ? user.UserName : String.Empty,
            };
            return View(model);
        }
        [HttpPost("editstudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditStudent(Student model)
        {
            try
            {

                model.StudentId = model.StudentId;
                model.CourseId = model.CourseId;
                model.BirthDate = model.BirthDate;
                model.FirstName = model.FirstName;
                model.LastName = model.LastName;
               
                studentService.UpdateStudent(model);
                
                return Redirect("studentslist");
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                studentService.DeleteStudent(id);
                return Json(new { success = true, message = "Silme işlemi başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpGet("studentslist")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult StudentsList()
        {
            var students = studentService.GetAllStudents();
            return View(students);
        }

        [HttpGet("studentcourselist")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult StudentCourseList()
        {

            ViewBag.courses = courseService.GetAllCourses().Select(x => new SelectListItem() { Value = x.CourseId, Text = x.CourseName });
            var model = (from s in studentService.GetAllStudents()
                         from c in courseService.GetAllCourses().Where(x => x.CourseId == s.CourseId).DefaultIfEmpty()
                         select new StudentCourseViewModel
                         {
                             StudentId = s.StudentId,
                             CourseId = c.CourseId,
                             FirstName = s.FirstName,
                             LastName = s.LastName,
                             BirthDate = s.BirthDate,
                             CourseName = c.CourseName,
                         }).ToList();
            return View(model);
        }


        [HttpGet("studentdetails")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult StudentDetails(int id)
        {
            var model = studentService.GetStudentById(id);
            return View(model);
        }
    }
}
