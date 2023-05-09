using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        //student tablosuyla ilişki foreignkey
        public virtual List<Student> Students { get; set; }
    }
}
