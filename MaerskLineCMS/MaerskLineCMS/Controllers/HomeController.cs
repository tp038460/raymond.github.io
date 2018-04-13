using MaerskLineCMS.Logic;
using MaerskLineCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskLineCMS.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }
        [SessionAdmin]
        public ActionResult getadminnotic() {
            MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();
            var listofpending = db.scheduleBookings.Where(s => s.status == "pending").ToList();
            var countofpending = (int)listofpending.Count();
            ViewBag.pending = countofpending;
            if (countofpending > 0)
            {
                return PartialView("getadminnotic", null);
            }
            else { return null; }
        }
    }
}