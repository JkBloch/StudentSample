using System.ComponentModel.DataAnnotations;

namespace StudentSample.Models
{
    public class SubjectModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter the subject name ")]
        public string Name { get; set; }
    }
}
