using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentSample.Models
{
    public class LanguageModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
