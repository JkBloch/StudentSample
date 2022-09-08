using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentSample.Models
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public Guid? StudentClassId { get; set; }

        [Required(ErrorMessage = "Please enter the student name ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter roll no ")]
        public int RollNo { get; set; }

        [Required(ErrorMessage = "Please enter age ")]
        public int Age { get; set; }
      
        public string? Image { get; set; }

        [Display(Name = "Student Image")]        
        public IFormFile? StudentPhoto { get; set; }

        public string? ClassName { get; set; }
    }
}
