using Microsoft.EntityFrameworkCore;
using StudentSample.Database;
using StudentSample.Models;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace StudentSample
{

    public class LanguageRepository : ILanguageRepository
    {
        private readonly StudentDbContext _context = null;
        private readonly IConfiguration _configuration;
        public LanguageRepository(StudentDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }      
        public async Task<List<LanguageModel>> GetAllLanguage()
        {
            return await _context.Languages
             .Select(languages => new LanguageModel()
             {
                 Id = languages.Id,
                 Name = languages.Name
             }).ToListAsync();

        }
        public async Task InsertDefaultLanguageData()
        {
            List<Language> lstLanguageModel = new List<Language>();
            lstLanguageModel.Add(new Language { Id = Guid.NewGuid(), Name = "English" });
            lstLanguageModel.Add(new Language { Id = Guid.NewGuid(), Name = "Hindi" });
            lstLanguageModel.Add(new Language { Id = Guid.NewGuid(), Name = "Arabic" });
            lstLanguageModel.Add(new Language { Id = Guid.NewGuid(), Name = "Spanish" });
            lstLanguageModel.Add(new Language { Id = Guid.NewGuid(), Name = "Japanese" });
            foreach (var item in lstLanguageModel)
            {
                _context.Languages.Add(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}
