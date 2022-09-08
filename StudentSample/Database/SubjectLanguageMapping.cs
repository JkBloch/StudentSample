using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSample
{
    public class SubjectLanguageMapping
    {
        [Key]
        public Guid Id { get; set; }
        
        [ForeignKey("Subjects")]
        public Guid SubjectId { get; set; }

        [ForeignKey("Languages")]        
        public Guid LanguageId { get; set; }
        public Subject Subject { get; set; }
        public Language Language { get; set; }
        
    }
}
