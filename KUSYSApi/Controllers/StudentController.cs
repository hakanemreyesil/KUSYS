using BusinessLayer.Abstract;
using DataAccessLayer;
using EntityLayer;
using KUSYSApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;

namespace KUSYSApi.Controllers
{
    //mvc ile oluşturduğum contrellarların apisini olusturdum swaggerdan denedim çalışıyor 
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
        [HttpPost("createstudent")]
        public IActionResult CreateStudent(Student model)
        {
            try
            {
                var student = new Student()
                {
                    StudentId = model.StudentId,
                    CourseId = model.CourseId,
                    BirthDate = DateTime.Now,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                studentService.CreateStudent(student);

                return Redirect("studentslist");
            }
            catch (Exception ex)
            {
                return Ok();

            }
        }
        [HttpDelete("{id}")]
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
        [HttpPut("editstudent")]
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
                return Ok();
            }

        }
        [HttpGet("studentslist")]      
        public IActionResult StudentsList()
        {
            var students = studentService.GetAllStudents();
            return Ok(students);
        }
        [HttpGet("studentcourselist")]    
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
            return Ok(model);
        }
        [HttpGet("studentdetails")]
        public IActionResult StudentDetails(int id)
        {
            var model = studentService.GetStudentById(id);
            return Ok(model);
        }
    }
}
