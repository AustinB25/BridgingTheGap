 using BridgingTheGap.Data;
using BridgingTheGap.Models;
using BridgingTheGap.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BridgingTheGap.WebMVC.Controllers
{
    public class TutorController : Controller
    {
        // GET: Tutor
        public ActionResult Index()
        {
            var tService = CreateTutorService();
            List<TutorListItem> tutorList = tService.GetAllTutors().ToList();
            List<TutorListItem> orderedList = tutorList.OrderBy(t => t.FullName).ToList();
            return View(orderedList);
        }

        //Get: Tutor/Create
        public ActionResult Create()
        {                  
            return View();
        }
        //Post: Tutor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TutorCreate model)
        {
            //Check that the model state is valid
            if (!ModelState.IsValid) return View(model);
            //Bring in tutor service 
            var tService = CreateTutorService();
            //Check to make sure bool for Create method returns true
            if (tService.CreateTutor( model))
            {
                //If so Save this string in temp data to be taken back to the view
                TempData["SaveResult"] = "Your tutor was created.";
                //Return the user back the the Tutor index
                return RedirectToAction("Index");
            }
            //If the tutor could not be created give the user the model back
            return View(model);
        }

        //Get: Tutor/Details{id}
        public ActionResult Details(int id)
        {
            var tService = CreateTutorService();
            var model = tService.GetTutorById(id);
            return View(model);
        }
        //Get: Tutor/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var tService = CreateTutorService();
            var model = tService.GetTutorById(id);
            return View(model);
        }
        //Post: Tutor/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var tService = CreateTutorService();
            tService.DeleteTutor(id);
            TempData["SaveResult"] = "The Tutor was deleted. ";
            return RedirectToAction("Index");
        }
        //Get: Tutor/Edit/{id}
        public ActionResult Edit(int id)
        {
            //Create an instance of the Tutor service Class
            var tService = CreateTutorService();
            //Find the tutor the user wants to edit
            var modelDetails = tService.GetTutorById(id);
            //Change the model from a tutor data model to a TutorEdit model
            var model =
                new TutorEdit
                {
                    TutorId = modelDetails.TutorId,
                    FirstName = modelDetails.FirstName,
                    LastName = modelDetails.LastName,
                    OwnerId = modelDetails.OwnerId
                };
            //Return the model back the user inside the view
            return View(model);
        }
        //Post: Tutor/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TutorEdit editModel)
        {
            if (!ModelState.IsValid) return View(editModel);
            if (editModel.TutorId != id)
            {
                ModelState.AddModelError(" ", "That Id does not match any registered tutors.");
                return View(editModel);
            }
            var tService = CreateTutorService();
            if (tService.UpdateTutor(editModel))
            {
                TempData["SaveResult"] = " The Tutor was updated. ";
                return RedirectToAction("Index");
            }
            return View(editModel);
        }     
        //Get: Tutor/AddSubjectToTutor{id}
        [HttpGet]
        public ActionResult AddSubjectToTutor(int id)
        {
            //Create Subect service to interact with subect data table
            var subService = CreateSubjectService();
            //Create SelectList for the Subjects in database and put them in the ViewBag foir the view
            ViewBag.SubjectId = new SelectList(subService.GetSubjects().ToList(), "SubjectId", "Name");
            //Create tutor service to interact with tutor data table
            var tutorService = CreateTutorService();                        
            //Go get the tutor we are going to add a subject to using service
            var tutor = tutorService.GetTutorById(id);
            var entity =
                new TutorDetails
                {
                    TutorId = tutor.TutorId,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    Subjects = tutor.Subjects
                };
            //Return the AddSubjectToTutor View with the tutorDetail
            return View(entity);
        }
        //Post: Tutor/AddSubjectToTutor/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubjectToTutor(int subjectId, TutorDetails tutorModel)
        {
            if (!ModelState.IsValid) return View(tutorModel);            
            var tService = CreateTutorService();
            if (tService.AddSubjectToTutor(subjectId, tutorModel.TutorId))
            {
                TempData["SaveResult"] = " The subject was added to the tutor ";
                return RedirectToAction("Index", "Tutor");
            }
            return View(tutorModel);
        }
        private SubjectService CreateSubjectService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var subjectService = new SubjectService(userId);
            return subjectService;
        }
        private TutorService CreateTutorService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var tutorService = new TutorService(userId);
            return tutorService;
        }
    }
}