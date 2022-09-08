using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentSample.Models
{
    public class TeacherModel
    {
        public Guid Id { get; set; }      

        [Required(ErrorMessage = "Please enter the teacher name ")]
        public string Name { get; set; }        

        [Required(ErrorMessage = "Please enter age ")]
        public int Age { get; set; }

        [DisplayName("Gender"), Required(ErrorMessage = "Please select gender ")]
        public string Sex { get; set; }
        public string? Image { get; set; }

        [Display(Name = "Teacher Image")]
        public IFormFile? TeacherPhoto { get; set; }

       
    }
}
