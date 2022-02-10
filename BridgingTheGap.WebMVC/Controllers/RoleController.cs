using BridgingTheGap.Data;
using BridgingTheGap.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BridgingTheGap.WebMVC.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            /*var service = CreateRoleService();
            List<IdentityRole> identityList = service.GetRoles().ToList();
            List<IdentityRole> orderedList = identityList.OrderBy(t => t.Name).ToList();*/
            var roles = _ctx.Roles.ToList();
            return View(roles);
        }
        //Get: Role/Create 
        public ActionResult Create()
        {
            var role = new IdentityRole();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole roleName)
        {
            /* if (!ModelState.IsValid) return View(roleName);
             var service = CreateRoleService();
             if (service.CreateRole(roleName))
             {
                 TempData["SaveResult"] = "The new Role was created.";                
                 return RedirectToAction("Index");
             }
             return View(roleName);*/
            _ctx.Roles.Add(roleName);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Role");
        }
        private RoleService CreateRoleService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RoleService(userId);
            return service;
        }
    }
}