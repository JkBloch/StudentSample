namespace StudentSample.Models
{
    public class TeacherSubjectMappingListModel
    {
        public Guid? StudentClassId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? LanguageId { get; set; }
        public List<TeacherSubjectMappingModel>? TeacherSubjectMappingModelList { get; set; }
    }
}
