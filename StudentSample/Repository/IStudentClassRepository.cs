using StudentSample.Models;

namespace StudentSample
{
    public interface IStudentClassRepository
    {
        Task<List<StudentClassModel>> GetAllStudentClass();
    }
}