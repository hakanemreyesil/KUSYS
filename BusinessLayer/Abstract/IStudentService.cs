using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
        int CreateStudent(Student student);
        int UpdateStudent(Student student);
        int DeleteStudent(int id);
        int DeleteStudent(Student student);

    }
}
