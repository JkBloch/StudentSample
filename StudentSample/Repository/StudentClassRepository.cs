using Microsoft.EntityFrameworkCore;
using StudentSample.Database;
using StudentSample.Models;

namespace StudentSample
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private readonly StudentDbContext _context = null;
        private readonly IConfiguration _configuration;
        public StudentClassRepository(StudentDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<StudentClassModel>> GetAllStudentClass()
        {           
            return await _context.StudentClass
               .Select(studentClass => new StudentClassModel()
               {
                   Id = studentClass.Id,
                   Name = studentClass.Name
               }).ToListAsync();
            // await InsertDefaultStudentClassLanguageData();
           
        }

        public async Task InsertDefaultStudentClassLanguageData()
        {
            List<StudentClass> lstStudentClass = new List<StudentClass>();
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 1" });
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 2" });
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 3" });
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 4" });
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 5" });
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 6" });
            lstStudentClass.Add(new StudentClass { Id = Guid.NewGuid(), Name = "Class 7" });
            foreach (var item in lstStudentClass)
            {
                _context.StudentClass.Add(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
