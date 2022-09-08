using Microsoft.EntityFrameworkCore;
using StudentSample.Database;
using StudentSample.Models;

namespace StudentSample
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly StudentDbContext _context = null;
        private readonly IConfiguration _configuration;
        #region Teacher
      
        public TeacherRepository(StudentDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Guid> InsertTeacher(TeacherModel model)
        {
            Teacher objTeacher = new Teacher()
            {
                Id = model.Id,
                Name = model.Name,
                Sex = model.Sex,
                Age = model.Age,
                Image = model.Image
            };
            _context.Teachers.Add(objTeacher);
            await _context.SaveChangesAsync();
            return objTeacher.Id;
        }
        public async Task<List<TeacherModel>> SearchTeacher(string? teacherName)
        {
            List<TeacherModel> lstTeacherModel = new List<TeacherModel>();
            List<Teacher> lstTeacher = new List<Teacher>();
            lstTeacher = await _context.Teachers.ToListAsync();

            if (teacherName != string.Empty && teacherName != null)
            {
                lstTeacher = lstTeacher.Where(x => x.Name.Contains(teacherName)).ToList();
            }
            return
                lstTeacher.Select(teachers => new TeacherModel()
                {
                    Id = teachers.Id,
                    Name = teachers.Name,
                    Sex = teachers.Sex,
                    Age = teachers.Age,
                    Image = teachers.Image
                }).ToList();


        }
        public async Task<List<TeacherModel>> GetAllTeacher()
        {
            List<TeacherModel> lstTeacherModel = new List<TeacherModel>();
            List<Teacher> lstTeacher = new List<Teacher>();
            lstTeacher = await _context.Teachers.ToListAsync();
            
            return
                lstTeacher.Select(teachers => new TeacherModel()
                {
                    Id = teachers.Id,
                    Name = teachers.Name,
                    Sex = teachers.Sex,
                    Age = teachers.Age,
                    Image = teachers.Image
                }).ToList();


        }
        public async Task<int> DeleteTeachersAsync(Guid teacherId)
        {
            var teacher = new Teacher { Id = teacherId };
            _context.Remove(teacher);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<TeacherModel> GetTeacherById(Guid id)
        {
            TeacherModel objTeacherModel = new TeacherModel();
            TeacherModel? teacherModel = await _context.Teachers.Where(x => x.Id == id)
                            .Select(teacher => new TeacherModel()
                            {
                                Id = teacher.Id,
                                Name = teacher.Name,
                                Sex = teacher.Sex,
                                Age = teacher.Age,
                                Image = teacher.Image
                            }).FirstOrDefaultAsync();

            objTeacherModel = (teacherModel != null) ? teacherModel : objTeacherModel;
            return objTeacherModel;

        }
        #endregion
        #region TeacherSubjectMapping
        public async Task<Guid> InsertTeacherSubjectMapping(TeacherSubjectMappingModel model)
        {
            TeacherSubjectMapping objTeacherSubjectMapping = new TeacherSubjectMapping()
            {
                Id = model.Id,
                StudentClassId = model.StudentClassId,
                TeacherId = model.TeacherId,
                SubjectId = model.SubjectId,
                LanguageId = model.LanguageId
            };
            _context.TeacherSubjectMapping.Add(objTeacherSubjectMapping);
            await _context.SaveChangesAsync();
            return objTeacherSubjectMapping.Id;
        }
        public async Task<List<TeacherSubjectMappingModel>> SearchTeacherSubjectMapping(Guid? subjectId, Guid? languageId, 
            Guid? studentClassId, Guid? teacherId)
        {
            List<TeacherSubjectMappingModel> lstTeacherSubjectMappingModel = new List<TeacherSubjectMappingModel>();
            List<TeacherSubjectMapping> lstTeacherSubjectMapping = new List<TeacherSubjectMapping>();
            lstTeacherSubjectMapping = await _context.TeacherSubjectMapping.
                Include(x => x.Subject).
                Include(x => x.Teacher).
                Include(x => x.StudentClass).
                Include(x => x.Language).ToListAsync();
            if (subjectId != null && subjectId != Guid.Empty)
            {
                lstTeacherSubjectMapping = lstTeacherSubjectMapping.Where(x => x.SubjectId == subjectId).ToList();
            }
            if (languageId != null && languageId != Guid.Empty)
            {
                lstTeacherSubjectMapping = lstTeacherSubjectMapping.Where(x => x.LanguageId == languageId).ToList();
            }
            if (studentClassId != null && studentClassId != Guid.Empty)
            {
                lstTeacherSubjectMapping = lstTeacherSubjectMapping.Where(x => x.StudentClassId == studentClassId).ToList();
            }
            if (teacherId != null && teacherId != Guid.Empty)
            {
                lstTeacherSubjectMapping = lstTeacherSubjectMapping.Where(x => x.TeacherId == teacherId).ToList();
            }
            return
                lstTeacherSubjectMapping.Select(teacherSubjectMappings => new TeacherSubjectMappingModel()
                {
                    Id = teacherSubjectMappings.Id,
                    SubjectId = teacherSubjectMappings.SubjectId,
                    LanguageId = teacherSubjectMappings.LanguageId,
                    StudentClassId = teacherSubjectMappings.StudentClassId,
                    TeacherId = teacherSubjectMappings.TeacherId,
                    SubjectName = teacherSubjectMappings.Subject.Name,
                    StudentClassName = teacherSubjectMappings.StudentClass.Name,
                    TeacherName= teacherSubjectMappings.Teacher.Name,
                    LanguageName = teacherSubjectMappings.Language.Name
                }).ToList();
        }
        public async Task<int> DeleteTeacherSubjectMappingAsync(Guid teacherSubjectMappingId)
        {
            var teacherSubjectMapping = new TeacherSubjectMapping { Id = teacherSubjectMappingId };
            _context.Remove(teacherSubjectMapping);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<TeacherSubjectMappingModel> GetTeacherSubjectMappingById(Guid id)
        {
            TeacherSubjectMappingModel objTeacherSubjectMappingModel = new TeacherSubjectMappingModel();
            TeacherSubjectMappingModel? teacherSubjectMappingModel = await _context.TeacherSubjectMapping.
                Include(x => x.Subject).
                Include(x => x.StudentClass).
                Include(x => x.Teacher).
                Include(x => x.Language).Where(x => x.Id == id)
                            .Select(teacherSubjectMapping => new TeacherSubjectMappingModel()
                            {
                                Id = teacherSubjectMapping.Id,
                                SubjectId = teacherSubjectMapping.SubjectId,
                                LanguageId = teacherSubjectMapping.LanguageId,
                                TeacherId = teacherSubjectMapping.TeacherId,
                                StudentClassId = teacherSubjectMapping.StudentClassId,
                                SubjectName = teacherSubjectMapping.Subject.Name,
                                StudentClassName = teacherSubjectMapping.StudentClass.Name,
                                TeacherName = teacherSubjectMapping.Teacher.Name,
                                LanguageName = teacherSubjectMapping.Language.Name
                            }).FirstOrDefaultAsync();

            objTeacherSubjectMappingModel = (teacherSubjectMappingModel != null) ? teacherSubjectMappingModel : objTeacherSubjectMappingModel;
            return objTeacherSubjectMappingModel;

        }
        #endregion
    }
}
