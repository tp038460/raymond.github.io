using MaerskLineCMS.Logic;
using MaerskLineCMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MaerskLineCMS.Controllers
{
    [SessionAdmin]
    public class SignupController : Controller
    {
        MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();


        public ActionResult ManageAdmin()
        {
            var admin_view = db.admins;
            return View(admin_view.ToList());
        }

        public ActionResult CreateNewAdmin() {

            return View();
        }

        [HttpPost]
        public ActionResult CreateNewAdmin(admin obj)
        {
            if (ModelState.IsValid)
            {
                var db = new MaerskLineContainerManagementSystemEntities();
                var duplicate = db.admins.Where(b => b.adminUsername == obj.adminUsername).FirstOrDefault();
                if (duplicate!=null) {
                    ModelState.AddModelError("", "The user name has been used.");
                    return View();
                }

                db.admins.Add(obj);
                db.SaveChanges();
                return RedirectToAction("ManageAdmin");
            }
            return View(obj);
        }

        public ActionResult DetailsAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        public ActionResult EditAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin(admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageAdmin");
            }
            return View(admin);
        }

        public ActionResult DeleteAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin admin = db.admins.Find(id);
            db.admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("ManageAdmin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public ActionResult ManageAgent()
        {
            var agent_view = db.agents;
            return View(agent_view.ToList());
        }

        public ActionResult CreateNewAgent()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateNewAgent(agent obj)

        {
            if (ModelState.IsValid)
            {
                var db = new MaerskLineContainerManagementSystemEntities();
                var duplicate = db.agents.Where(b => b.agentUsername == obj.agentUsername).FirstOrDefault();
                if (duplicate != null)
                {
                    ModelState.AddModelError("", "The user name has been used.");
                    return View();
                }

                db.agents.Add(obj);
                db.SaveChanges();
                return RedirectToAction("ManageAgent");
            }
            return View(obj);
        }


        public ActionResult DetailsAgent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent agent = db.agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        public ActionResult EditAgent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent agent = db.agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAgent(agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageAgent");
            }
            return View(agent);
        }

        public ActionResult DeleteAgent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent agent = db.agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        [HttpPost, ActionName("DeleteAgent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            agent agent = db.agents.Find(id);
            db.agents.Remove(agent);
            db.SaveChanges();
            return RedirectToAction("ManageAgent");
        }
    }
}