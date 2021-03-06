using BridgingTheGap.Data;
using BridgingTheGap.Models;
using BridgingTheGap.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BridgingTheGap.WebMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student        
        public ActionResult Index()
        {
            var service = CreateStudentService();
            var studentList = service.GetStudents().ToList();
            var orderedList = studentList.OrderBy(e => e.FullName).ToList();
            return View(orderedList);
        }
        //Get: Student/Create
        public ActionResult Create()
        {
            return View();
        }
        //Post: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateStudentService();
            if (service.CreateStudent(model))
            {
                TempData["SaveResult"] = " Your student was created.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //Get: Student/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateStudentService();
            var model = service.GetStudentById(id);
            return View(model);
        }
        //Get Student/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateStudentService();
            var modelDetails = service.GetStudentById(id);
            var model =
                new StudentDetail
                {
                    StudentId = modelDetails.StudentId,
                    FirstName = modelDetails.FirstName,
                    LastName = modelDetails.LastName,
                    
                };
                return View(model);
        }
        //Post: Student/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateStudentService();
            service.DeleteStudent(id);
            TempData["SaveResult"] = "The student was deleted";
            return RedirectToAction("Index");
        }
        //Get: Student/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateStudentService();
            var modelDetails = service.GetStudentById(id);
            var model =
                new StudentEdit
                {
                    StudentId = modelDetails.StudentId,
                    FirstName = modelDetails.FirstName,
                    LastName = modelDetails.LastName,
                    OwnerId = modelDetails.OwnerId
                };
            return View(model);
        }
        //Post: Student/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StudentEdit editModel)
        {
            if (!ModelState.IsValid) return View(editModel);
            if(editModel.StudentId != id)
            {
                ModelState.AddModelError(" ", "That StudentId does not exsist.");
                return View(editModel);
            }
            var service = CreateStudentService();
            if (service.UpdateStudent(editModel))
            {
                TempData["SaveResult"] = " The student was updated.";
                return RedirectToAction("Index");
            }
            return View(editModel);
        }
        //Get: Student/AddSubjectToStudent/{id}
        [HttpGet]
        public ActionResult AddSubjectToStudent(int id)
        {            
            //Create Subect service to interact with subect data table
            var subService = CreateSubjectService();
            //Create SelectList for the Subjects and put them in the ViewBag foir the view
            ViewBag.SubjectId = new SelectList(subService.GetSubjects(), "SubjectId", "Name");
            //Create student service to interact with tutor data table
            var studentService = CreateStudentService();
            //Go get the student we are going to add a subject to using service
            var student = studentService.GetStudentById(id);
            var entity =
                new StudentDetail
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Subjects = student.Subjects,
                    OwnerId = student.OwnerId                 
                };
            //Return the AddSubjectToStudent View with the studentDetail
            return View(entity);
        }
        //Post: Tutor/AddSubjectToStudent/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubjectToStudent(StudentDetail studentModel)
        {
            if (!ModelState.IsValid) return View(studentModel);
            var studentService = CreateStudentService();
            if (studentService.AddSubjectToStudent(studentModel.SubjectId, studentModel.StudentId))
            {
                TempData["SaveResult"] = " The subject was added to the student ";
                return RedirectToAction("Index", "Student");
            }
            return View(studentModel);
        }
        //Get: Student/RemoveSubjectFromStudent/{id}
        [HttpGet]
        public ActionResult RemoveSubjectFromStudent(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            //Create Subect service to interact with subject data table
            var subService = CreateSubjectService();
            //Create student service to interact with student data table
            var studentService = CreateStudentService();
            //Go get the student we are going to remove a subject from using service
            var student = studentService.GetStudentById(id);
            var entity =
                new StudentRemoveSubject
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Subjects = student.Subjects                    
                };
            //Create SelectList for the Subjects that the student has and put them in the ViewBag foir the view
            ViewBag.SubjectId = new SelectList(entity.Subjects, "SubjectId", "Name");
            //Return the AddSubjectToStudent View with the studentDetail
            return View(entity);
        }
        //Post: Tutor/RemoveSubjectFromStudent/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveSubjectFromStudent(StudentRemoveSubject studentModel)
        {
            if (!ModelState.IsValid) return View(studentModel);
            var studentService = CreateStudentService();
            if (studentService.RemoveSubjectFromStudent(studentModel.SubjectId, studentModel.StudentId))
            {
                TempData["SaveResult"] = " The subject was removed to the student ";
                return RedirectToAction("Index", "Student");
            }
            return View(studentModel);
        }
        public StudentService CreateStudentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var sService = new StudentService(userId);
            return sService;
        }
        private SubjectService CreateSubjectService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var subjectService = new SubjectService(userId);
            return subjectService;
        }
    }
}