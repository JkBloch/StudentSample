using System.ComponentModel.DataAnnotations;

namespace StudentSample.Models
{
    public class TeacherSubjectMappingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please select subject ")]
        public Guid SubjectId { get; set; }

        [Required(ErrorMessage = "Please select language ")]
        public Guid LanguageId { get; set; }

        [Required(ErrorMessage = "Please select teacher ")]
        public Guid TeacherId { get; set; }

        [Required(ErrorMessage = "Please select class ")]
        public Guid StudentClassId { get; set; }
        public string? SubjectName { get; set; }
        public string? LanguageName { get; set; }
        public string? TeacherName { get; set; }
        public string? StudentClassName { get; set; }
    }
}
