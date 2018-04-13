using MaerskLineCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskLineCMS.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginAsAgent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAsAgent(agent objUser)
        {
            if (ModelState.IsValid)
            {
                using (
                    MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities())
                {
                    var obj = db.agents.Where(a => a.agentUsername.Equals(objUser.agentUsername) && a.agentPassword.Equals(objUser.agentPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.agentID.ToString();
                        Session["UserName"] = obj.agentName.ToString();
                        Session["UserCategory"] = "agent";
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(objUser);
        }
    }
}
