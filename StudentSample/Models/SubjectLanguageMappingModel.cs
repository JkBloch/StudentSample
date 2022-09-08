using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentSample.Models
{
    public class SubjectLanguageMappingModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please select subject ")]
        public Guid SubjectId { get; set; }

        [Required(ErrorMessage = "Please select language ")]
        public Guid LanguageId { get; set; }
        public string? SubjectName { get; set; }
        public string? LanguageName { get; set; }

    }
}
