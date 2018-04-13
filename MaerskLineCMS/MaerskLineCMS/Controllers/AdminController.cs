using MaerskLineCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskLineCMS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginAsAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAsAdmin(admin objUser)
        {
            if (ModelState.IsValid)
            {
                using (
                    MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities())
                {
                    var obj = db.admins.Where(a => a.adminUsername.Equals(objUser.adminUsername) && a.adminPassword.Equals(objUser.adminPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.adminID.ToString();
                        Session["UserName"] = obj.adminName.ToString();
                        Session["UserCategory"] = "admin";
                        return RedirectToAction("Index","Home");
                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(objUser);
        }




    }    
}
