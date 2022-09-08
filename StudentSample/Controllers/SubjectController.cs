using Microsoft.AspNetCore.Mvc;
using StudentSample.Models;
using StudentSample;

namespace StudentSample.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _iHostingEnvironment = null;
        public SubjectController(ISubjectRepository subjectRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment iHostingEnvironment)
        {
            _subjectRepository = subjectRepository;
            _iHostingEnvironment = iHostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.IsSuccess = false;
            return View();
        }
        #region Subject
     
        public async Task<ViewResult> InsertSubject(Guid subjectId, bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.SubjectId = subjectId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertSubject(SubjectModel subjectModel)
        {
            if (ModelState.IsValid)
            {               
                var id = await _subjectRepository.InsertSubject(subjectModel);
                if (id != Guid.Empty)
                {
                    return RedirectToAction(nameof(InsertSubject), new { subjectId = id, isSuccess = true });
                }
            }
            ViewBag.IsSuccess = false;
            return View();
        }     
        public async Task<ViewResult> GetSubject(Guid id)
        {
            var data = await _subjectRepository.GetSubjectById(id);
            return View(data);
        }
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var data = await _subjectRepository.DeleteSubjectAsync(id);
            return RedirectToAction("SubjectList", new SubjectListModel());
        }
        public async Task<ViewResult> SubjectList(SubjectListModel subjectListModel)
        {
            var data = await _subjectRepository.SearchSubject(subjectListModel.SubjectName);
            subjectListModel.SubjectList = data;
            return View(subjectListModel);
        }
        #endregion

        #region SubjectLanguageMapping

        public async Task<ViewResult> InsertSubjectLanguageMapping(Guid subjectLanguageMappingId, bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.SubjectLanguageMappingId = subjectLanguageMappingId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertSubjectLanguageMapping(SubjectLanguageMappingModel subjectLanguageMappingModel)
        {
            if (ModelState.IsValid)
            {
                var id = await _subjectRepository.InsertSubjectLanguageMapping(subjectLanguageMappingModel);
                if (id != Guid.Empty)
                {
                    return RedirectToAction(nameof(InsertSubjectLanguageMapping), new { subjectLanguageMappingId = id, isSuccess = true });
                }
            }
            ViewBag.IsSuccess = false;
            return View();
        }
        public async Task<ViewResult> GetSubjectLanguageMapping(Guid id)
        {
            var data = await _subjectRepository.GetSubjectLanguageMappingById(id);
            return View(data);
        }
        public async Task<IActionResult> DeleteSubjectLanguageMapping(Guid id)
        {
            var data = await _subjectRepository.DeleteSubjectLanguageMappingAsync(id);
            return RedirectToAction("SubjectLanguageMappingList", new SubjectLanguageMappingListModel());
        }
        public async Task<ViewResult> SubjectLanguageMappingList(SubjectLanguageMappingListModel subjectLanguageMappingListModel)
        {
            var data = await _subjectRepository.SearchSubjectLanguageMapping(subjectLanguageMappingListModel.SubjectId, subjectLanguageMappingListModel.LanguageId);
            subjectLanguageMappingListModel.SubjectLanguageMappingList = data;
            return View(subjectLanguageMappingListModel);
        }
        #endregion
    }
}
