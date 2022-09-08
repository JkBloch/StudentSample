using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSample
{
    public class Student
    {
        [Key]
        public Guid Id{ get; set; }
        [ForeignKey("StudentClass")]
        public Guid? StudentClassId { get; set; }

        [Column(TypeName = "nvarchar"), StringLength(100)]
        public string Name { get; set; }
        public int RollNo { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "varchar"), StringLength(150)]
        public string? Image { get; set; }
        public StudentClass StudentClass { get; set; }        

    }
}
