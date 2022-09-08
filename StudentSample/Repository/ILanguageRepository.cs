using StudentSample.Models;

namespace StudentSample
{
    public interface ILanguageRepository
    {      
        Task<List<LanguageModel>> GetAllLanguage();
    }
}