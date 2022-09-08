using StudentSample.Models;

namespace StudentSample
{
    public interface IStudentRepository
    {
        Task<int> DeleteStudentAsync(Guid studentId);       
        Task<Guid> InsertStudent(StudentModel model);
        Task<StudentModel> GetStudentById(Guid id);
        Task<List<StudentModel>> SearchStudent(Guid? studentClassId, string? studentName);
    }
}