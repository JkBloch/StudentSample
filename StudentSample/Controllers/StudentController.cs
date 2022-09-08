using Microsoft.AspNetCore.Mvc;
using StudentSample.Models;
using StudentSample;

namespace StudentSample.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _iHostingEnvironment = null;
        public StudentController(IStudentRepository studentRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment iHostingEnvironment)
        {
            _studentRepository = studentRepository;
            _iHostingEnvironment = iHostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.IsSuccess = false;
            return View();
        }
        public async Task<ViewResult> InsertStudent(Guid studentId,bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.StudentId = studentId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertStudent(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                if (studentModel.StudentPhoto != null)
                {
                    string sStudentImagePath = "Images/StudentImage/";
                    studentModel.Image = await UploadImage(sStudentImagePath, studentModel.StudentPhoto);
                }
                var id = await _studentRepository.InsertStudent(studentModel);
                if (id !=Guid.Empty)
                {
                    return RedirectToAction(nameof(InsertStudent), new { studentId = id,isSuccess = true });
                }
            }
            ViewBag.IsSuccess = false;
            return View();            
        }
 
        public async Task<ViewResult> GetStudent(Guid id)
       {
            var data = await _studentRepository.GetStudentById(id);
            return View(data);
        }
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var data = await _studentRepository.DeleteStudentAsync(id);
            return RedirectToAction("StudentList",new StudentListModel());
        }
        public async Task<ViewResult> StudentList(StudentListModel studentListModel)
        {
            var data = await _studentRepository.SearchStudent(studentListModel.StudentClassId, studentListModel.StudentName);
            studentListModel.StudentList= data;
            return View(studentListModel);
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + file.FileName;
            string sServerFolder = Path.Combine(_iHostingEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(sServerFolder, FileMode.Create));
            return "/" + folderPath;
        }
    
    
    }
}
