using Microsoft.AspNetCore.Mvc;
using StudentSample.Models;
using StudentSample;
using Microsoft.EntityFrameworkCore;

namespace StudentSample.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _iHostingEnvironment = null;
        public TeacherController(ITeacherRepository teacherRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment iHostingEnvironment)
        {
            _teacherRepository = teacherRepository;
            _iHostingEnvironment = iHostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.IsSuccess = false;
            return View();
        }
        #region Teacher
       
        public async Task<ViewResult> InsertTeacher(Guid teacherId, bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.TeacherId = teacherId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertTeacher(TeacherModel teacherModel)
        {
            if (ModelState.IsValid)
            {
                if (teacherModel.TeacherPhoto != null)
                {
                    string sTeacherImagePath = "Images/TeacherImage/";
                    teacherModel.Image = await UploadImage(sTeacherImagePath, teacherModel.TeacherPhoto);
                }
                var id = await _teacherRepository.InsertTeacher(teacherModel);
                if (id != Guid.Empty)
                {
                    return RedirectToAction(nameof(InsertTeacher), new { teacherId = id, isSuccess = true });
                }
            }
            ViewBag.IsSuccess = false;
            return View();
        }
     
        public async Task<ViewResult> GetTeacher(Guid id)
        {
            var data = await _teacherRepository.GetTeacherById(id);
            return View(data);
        }
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var data = await _teacherRepository.DeleteTeachersAsync(id);
            return RedirectToAction("TeacherList", new TeacherListModel());
        }
        public async Task<ViewResult> TeacherList(TeacherListModel teacherListModel)
        {
            var data = await _teacherRepository.SearchTeacher(teacherListModel.TeacherName);
            teacherListModel.TeacherList = data;
            return View(teacherListModel);
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + file.FileName;
            string sServerFolder = Path.Combine(_iHostingEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(sServerFolder, FileMode.Create));
            return "/" + folderPath;
        }
        #endregion
        #region TeacherSubjectMapping

        public async Task<ViewResult> InsertTeacherSubjectMapping(Guid teacherSubjectMappingId, bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.TeacherSubjectMappingId = teacherSubjectMappingId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertTeacherSubjectMapping(TeacherSubjectMappingModel teacherSubjectMappingModel)
        {
            if (ModelState.IsValid)
            {
                var id = await _teacherRepository.InsertTeacherSubjectMapping(teacherSubjectMappingModel);
                if (id != Guid.Empty)
                {
                    return RedirectToAction(nameof(InsertTeacherSubjectMapping), new { teacherSubjectMappingId = id, isSuccess = true });
                }
            }
            ViewBag.IsSuccess = false;
            return View();
        }
        public async Task<ViewResult> GetTeacherSubjectMapping(Guid id)
        {
            var data = await _teacherRepository.GetTeacherSubjectMappingById(id);
            return View(data);
        }
        public async Task<IActionResult> DeleteTeacherSubjectMapping(Guid id)
        {
            var data = await _teacherRepository.DeleteTeacherSubjectMappingAsync(id);
            return RedirectToAction("TeacherSubjectMappingList", new TeacherSubjectMappingListModel());
        }
        public async Task<ViewResult> TeacherSubjectMappingList(TeacherSubjectMappingListModel teacherSubjectMappingListModel)
        {
            var data = await _teacherRepository.SearchTeacherSubjectMapping(teacherSubjectMappingListModel.SubjectId,
                teacherSubjectMappingListModel.LanguageId, teacherSubjectMappingListModel.StudentClassId, teacherSubjectMappingListModel.TeacherId);
            teacherSubjectMappingListModel.TeacherSubjectMappingModelList = data;
            return View(teacherSubjectMappingListModel);
        }
        #endregion
    }
}
