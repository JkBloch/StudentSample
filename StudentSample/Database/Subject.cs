using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentSample
{
    public class Subject
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(50)]
        public string Name { get; set; }
        public ICollection<SubjectLanguageMapping> SubjectLanguageMapping { get; set; }
        public ICollection<TeacherSubjectMapping> TeacherSubjectMapping { get; set; }
    }
}
