using StudentSample.Models;

namespace StudentSample
{
    public interface ISubjectRepository
    {
        Task<int> DeleteSubjectAsync(Guid subjectId);
        Task<SubjectModel> GetSubjectById(Guid id);
        Task<Guid> InsertSubject(SubjectModel model);
        Task<List<SubjectModel>> GetAllSubject();
        Task<List<SubjectModel>> SearchSubject(string? subjectName);

        Task<int> DeleteSubjectLanguageMappingAsync(Guid subjectId);
        Task<SubjectLanguageMappingModel> GetSubjectLanguageMappingById(Guid id);
        Task<Guid> InsertSubjectLanguageMapping(SubjectLanguageMappingModel model);
        Task<List<SubjectLanguageMappingModel>> SearchSubjectLanguageMapping(Guid? subjectId, Guid? languageId);
    }
}