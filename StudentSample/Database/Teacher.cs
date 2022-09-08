using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSample
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(100)]
        public string Name { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "varchar"), StringLength(150)]
        public string? Image { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(10)]
        public string Sex { get; set; }

        public ICollection<TeacherSubjectMapping> TeacherSubjectMapping { get; set; }
        
    }
}
