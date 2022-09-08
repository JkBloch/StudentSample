using System.Reflection.Metadata.Ecma335;

namespace StudentSample.Models
{
    public class StudentListModel
    {
        public Guid? StudentClassId { get; set; }
        public string? StudentName { get; set; }

        public List<StudentModel>? StudentList { get; set; }
    }
}
