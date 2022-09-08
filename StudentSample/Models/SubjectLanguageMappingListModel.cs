namespace StudentSample.Models
{
    public class SubjectLanguageMappingListModel
    {
        public Guid? SubjectId { get; set; }
        public Guid? LanguageId { get; set; }
        public List<SubjectLanguageMappingModel>? SubjectLanguageMappingList { get; set; }
    }
}
