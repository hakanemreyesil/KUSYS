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
    public class StudentService : IStudentService
    {
        private readonly IRepository repo;

        public StudentService(IRepository repo)
        {
            this.repo = repo;
        }

        public int CreateStudent(Student student)
        {
            repo.Add(student);

            repo.SaveChanges();
            return student.StudentId;
        }

        public int DeleteStudent(int id)
        {
            var deletedStudent = repo.GetEntityObject<Student>(id);
            repo.Remove(deletedStudent);
            return repo.SaveChanges();
        }

        public int DeleteStudent(Student student)
        {
            repo.Remove(student);
            return repo.SaveChanges();
        }

        public List<Student> GetAllStudents()
        {
            return repo.GetIQueryableObject<Student>().ToList();
        }

        public Student GetStudentById(int id)
        {
            return repo.GetEntityObject<Student>(id);
        }

        public int UpdateStudent(Student student)
        {
            repo.Modify(student);
            return repo.SaveChanges();
        }
    }
}
