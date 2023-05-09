using BusinessLayer.Abstract;
using DataAccessLayer.Abstracts;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concretes
{
    public class CourseService : ICourseService
    {
        private readonly IRepository repo;
        public CourseService(IRepository repo)
        {
            this.repo = repo;
        }
        public List<Course> GetAllCourses()
        {
            return repo.GetIQueryableObject<Course>().ToList();
        }
    }
}
