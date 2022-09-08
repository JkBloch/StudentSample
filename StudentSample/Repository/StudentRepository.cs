using Microsoft.EntityFrameworkCore;
using StudentSample.Database;
using StudentSample.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace StudentSample
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context = null;
        private readonly IConfiguration _configuration;
        public StudentRepository(StudentDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Guid> InsertStudent(StudentModel model)
        {
            Student objStudent = new Student()
            {
                Id = model.Id,
                StudentClassId = (model.StudentClassId==Guid.Empty?null: model.StudentClassId),
                Name = model.Name,
                RollNo = model.RollNo,
                Age = model.Age,
                Image = model.Image
            };
            _context.Students.Add(objStudent);
            await _context.SaveChangesAsync();
            return objStudent.Id;
        }
        public async Task<List<StudentModel>> SearchStudent(Guid? studentClassId,string? studentName)
        {
            List<StudentModel> lstStudentModel = new List<StudentModel>();
            List<Student> lstStudent = new List<Student>();
            lstStudent = await _context.Students.Include(x=>x.StudentClass).ToListAsync();
            if (studentClassId !=null && studentClassId!=Guid.Empty)
            {
                lstStudent = lstStudent.Where(x => x.StudentClassId==studentClassId).ToList();
            }
            if (studentName!=string.Empty && studentName != null)
            {
                lstStudent = lstStudent.Where(x => x.Name.Contains(studentName)).ToList();
            }
            return
                lstStudent.Select(students => new StudentModel()
                {
                    Id = students.Id,
                    StudentClassId = students.StudentClassId,
                    Name = students.Name,
                    RollNo = students.RollNo,
                    Age = students.Age,
                    Image = students.Image,
                    ClassName = (students.StudentClass != null)?students.StudentClass.Name : string.Empty
                }).ToList();


        }
        public async Task<int> DeleteStudentAsync(Guid studentId)
        {
            var student = new Student { Id = studentId };
            _context.Remove(student);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<StudentModel> GetStudentById(Guid id)
        {
            StudentModel objStudentModel = new StudentModel();
            StudentModel? studentModel = await _context.Students.Where(x => x.Id == id)
                            .Select(student => new StudentModel()
                            {
                                Id = student.Id,
                                StudentClassId = student.StudentClassId,
                                Name = student.Name,
                                RollNo = student.RollNo,
                                Age = student.Age,
                                Image = student.Image,
                                ClassName = student.StudentClass.Name
                            }).FirstOrDefaultAsync();

            objStudentModel = (studentModel != null)?studentModel: objStudentModel;
            return objStudentModel;

        }
        

    }
}
