using Microsoft.EntityFrameworkCore;
using StudentSample.Database;
using StudentSample.Models;

namespace StudentSample
{
    public class SubjectRepository : ISubjectRepository
    {
        #region Declaration
        private readonly StudentDbContext _context = null;
        private readonly IConfiguration _configuration;
        public SubjectRepository(StudentDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #endregion

        #region Subject

        public async Task<Guid> InsertSubject(SubjectModel model)
        {
            Subject objSubject = new Subject()
            {
                Id = model.Id,
                Name = model.Name
            };
            _context.Subjects.Add(objSubject);
            await _context.SaveChangesAsync();
            return objSubject.Id;
        }
        public async Task<List<SubjectModel>> GetAllSubject()
        {
            List<SubjectModel> lstSubjectModel = new List<SubjectModel>();
            List<Subject> lstSubject = new List<Subject>();
            lstSubject = await _context.Subjects.ToListAsync();          
            return
                lstSubject.Select(subjects => new SubjectModel()
                {
                    Id = subjects.Id,
                    Name = subjects.Name
                }).ToList();


        }
        public async Task<List<SubjectModel>> SearchSubject(string? subjectName)
        {
            List<SubjectModel> lstSubjectModel = new List<SubjectModel>();
            List<Subject> lstSubject = new List<Subject>();
            lstSubject = await _context.Subjects.ToListAsync();
            if (subjectName != string.Empty && subjectName != null)
            {
                lstSubject = lstSubject.Where(x => x.Name.Contains(subjectName)).ToList();
            }
            return
                lstSubject.Select(subjects => new SubjectModel()
                {
                    Id = subjects.Id,
                    Name = subjects.Name
                }).ToList();


        }
        public async Task<int> DeleteSubjectAsync(Guid subjectId)
        {
            var subject = new Subject { Id = subjectId };
            _context.Remove(subject);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<SubjectModel> GetSubjectById(Guid id)
        {
            SubjectModel objSubjectModel = new SubjectModel();
            SubjectModel? subjectModel = await _context.Subjects.Where(x => x.Id == id)
                            .Select(subject => new SubjectModel()
                            {
                                Id = subject.Id,
                                Name = subject.Name
                            }).FirstOrDefaultAsync();

            objSubjectModel = (subjectModel != null) ? subjectModel : objSubjectModel;
            return objSubjectModel;

        }
        #endregion
        #region SubjectLanguageMapping
        public async Task<Guid> InsertSubjectLanguageMapping(SubjectLanguageMappingModel model)
        {
            SubjectLanguageMapping objSubjectLanguageMapping = new SubjectLanguageMapping()
            {
                Id = model.Id,
                SubjectId = model.SubjectId,
                LanguageId = model.LanguageId
            };
            _context.SubjectLanguageMapping.Add(objSubjectLanguageMapping);
            await _context.SaveChangesAsync();
            return objSubjectLanguageMapping.Id;
        }
        public async Task<List<SubjectLanguageMappingModel>> SearchSubjectLanguageMapping(Guid? subjectId, Guid? languageId)
        {
            List<SubjectLanguageMappingModel> lstSubjectLanguageMappingModel = new List<SubjectLanguageMappingModel>();
            List<SubjectLanguageMapping> lstSubjectLanguageMapping = new List<SubjectLanguageMapping>();
            lstSubjectLanguageMapping = await _context.SubjectLanguageMapping.Include(x=>x.Subject).Include(x=>x.Language).ToListAsync();
            if (subjectId != null && subjectId != Guid.Empty)
            {
                lstSubjectLanguageMapping = lstSubjectLanguageMapping.Where(x => x.SubjectId == subjectId).ToList();
            }
            if (languageId != null && languageId != Guid.Empty)
            {
                lstSubjectLanguageMapping = lstSubjectLanguageMapping.Where(x => x.LanguageId == languageId).ToList();
            }          
            return
                lstSubjectLanguageMapping.Select(subjectLanguageMappings => new SubjectLanguageMappingModel()
                {
                    Id = subjectLanguageMappings.Id,
                    SubjectId = subjectLanguageMappings.SubjectId,
                    LanguageId = subjectLanguageMappings.LanguageId,
                    SubjectName= subjectLanguageMappings.Subject.Name,
                    LanguageName = subjectLanguageMappings.Language.Name
                }).ToList();
        }
        public async Task<int> DeleteSubjectLanguageMappingAsync(Guid subjectLanguageMappingId)
        {
            var subjectLanguageMapping = new SubjectLanguageMapping { Id = subjectLanguageMappingId };
            _context.Remove(subjectLanguageMapping);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<SubjectLanguageMappingModel> GetSubjectLanguageMappingById(Guid id)
        {
            SubjectLanguageMappingModel objSubjectLanguageMappingModel = new SubjectLanguageMappingModel();
            SubjectLanguageMappingModel? subjectLanguageMappingModel = await _context.SubjectLanguageMapping.Include(x => x.Subject).Include(x => x.Language).Where(x => x.Id == id)
                            .Select(subjectLanguageMapping => new SubjectLanguageMappingModel()
                            {
                                Id = subjectLanguageMapping.Id,
                                SubjectId = subjectLanguageMapping.SubjectId,
                                LanguageId = subjectLanguageMapping.LanguageId,
                                SubjectName = subjectLanguageMapping.Subject.Name,
                                LanguageName = subjectLanguageMapping.Language.Name
                            }).FirstOrDefaultAsync();

            objSubjectLanguageMappingModel = (subjectLanguageMappingModel != null) ? subjectLanguageMappingModel : objSubjectLanguageMappingModel;
            return objSubjectLanguageMappingModel;

        }
        #endregion

    }
}
