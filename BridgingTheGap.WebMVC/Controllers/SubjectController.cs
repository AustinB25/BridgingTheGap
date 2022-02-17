using BridgingTheGap.Models;
using BridgingTheGap.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BridgingTheGap.WebMVC.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject        
        public ActionResult Index()
        {
            var service = CreateSubjectService();
            var subjectList = service.GetSubjects().ToList();
            var orderedList = subjectList.OrderBy(e => e.Name).ToList();
            return View(orderedList);
        }
        //Get: Subject/Create
        public ActionResult Create()
        {
            return View();
        }
        //Post: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateSubjectService();
            if (service.CreateSubject(model))
            {
                TempData["SaveResult"] = " Your subject was created.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //Get: Subject/Details/{id}
        public ActionResult Details(int id)
        {           
            var service = CreateSubjectService();
            var model = service.GetSubjectById(id);
            return View(model);
        }
        //Get Subject/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateSubjectService();
            var modelDetails = service.GetSubjectById(id);
            var model =
                new SubjectDetail
                {
                    SubjectId = modelDetails.SubjectId,
                    Name = modelDetails.Name,
                    Students = modelDetails.Students
                };
            return View(model);
        }
        //Post: Subject/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSubjectService();
            service.DeleteSubject(id);
            TempData["SaveResult"] = "The subject was deleted";
            return RedirectToAction("Index");
        }
        //Get: Subject/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateSubjectService();
            var modelDetails = service.GetSubjectById(id);
            var model =
                new SubjectEdit
                {
                    SubjectId = modelDetails.SubjectId,
                    OwnerId = modelDetails.OwnerId,
                    Name = modelDetails.Name
                };
            return View(model);
        }
        //Post: Subject/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubjectEdit editModel)
        {
            if (!ModelState.IsValid) return View(editModel);
            if (editModel.SubjectId != id)
            {
                ModelState.AddModelError(" ", "That SubjectId does not exsist.");
                return View(editModel);
            }
            var service = CreateSubjectService();
            if (service.UpdateSubject(editModel))
            {
                TempData["SaveResult"] = " The subject was updated.";
                return RedirectToAction("Index");
            }
            return View(editModel);
        }
        //Get: Subject/GetAllStudentsInSubject/{id}
        [HttpGet]
        public ActionResult ViewAllStudentsInSubject(int id)
        {
            var service = CreateSubjectService();
            var subject = service.GetSubjectById(id);
            var entity =
                new SubjectDetail
                {
                    SubjectId = subject.SubjectId,
                    Name = subject.Name,
                    Students = subject.Students
                };
            return View(entity);
        }        
        public SubjectService CreateSubjectService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SubjectService(userId);
            return service;
        }
    }

}