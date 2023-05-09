namespace KUSYS.Models
{
    public class StudentUserViewModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CourseId { get; set; }
        public bool IsUser { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
