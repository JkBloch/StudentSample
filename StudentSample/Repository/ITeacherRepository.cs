using StudentSample.Models;

namespace StudentSample
{
    public interface ITeacherRepository
    {
        Task<int> DeleteTeachersAsync(Guid teacherId);
        Task<TeacherModel> GetTeacherById(Guid id);
        Task<Guid> InsertTeacher(TeacherModel model);
        Task<List<TeacherModel>> SearchTeacher(string? teacherName);
        Task<List<TeacherModel>> GetAllTeacher();
        Task<int> DeleteTeacherSubjectMappingAsync(Guid teacherSubjectMappingId);
        Task<TeacherSubjectMappingModel> GetTeacherSubjectMappingById(Guid id);
        Task<Guid> InsertTeacherSubjectMapping(TeacherSubjectMappingModel model);
        Task<List<TeacherSubjectMappingModel>> SearchTeacherSubjectMapping(Guid? subjectId, Guid? languageId,
            Guid? studentClassId, Guid? teacherId);
    }
}