using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSample
{
    public class TeacherSubjectMapping
    {
        public Guid Id { get; set; }

        [ForeignKey("StudentClass")]
        public Guid StudentClassId { get; set; }
        
        [ForeignKey("Teachers")]
        public Guid TeacherId { get; set; }

        [ForeignKey("Subjects")]
        public Guid SubjectId { get; set; }

        [ForeignKey("Languages")]
        public Guid LanguageId { get; set; }

        public StudentClass StudentClass { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Language Language { get; set; }
    }
}
