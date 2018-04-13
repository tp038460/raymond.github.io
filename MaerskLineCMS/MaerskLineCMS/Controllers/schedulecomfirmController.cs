using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaerskLineCMS.Logic;
using MaerskLineCMS.Models;

namespace MaerskLineCMS.Controllers
{
    [SessionAdmin]
    public class schedulecomfirmController : Controller
    {
        private MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();

        // GET: schedulecomfirm
        public ActionResult Index()
        {
            var scheduleBookings = db.scheduleBookings.Include(s => s.agent).Include(s => s.schedule);
            return View(scheduleBookings.ToList());
        }

        // GET: schedulecomfirm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scheduleBooking scheduleBooking = db.scheduleBookings.Find(id);
            var scheduleid = scheduleBooking.scheduleID;
            var shiprequiredTEU = db.Database.SqlQuery<decimal>("select cast(ISNULL(sum(s2.requiredTEU),0)as decimal(18,2)) as shiprequiredTEU from scheduleBooking s2 where s2.status = 'confirm' AND s2.scheduleID =" + scheduleid + " group by s2.scheduleID").FirstOrDefault();
            //var passdata = shiprequiredTEU.GetType().GetProperties().FirstOrDefault();
            if (shiprequiredTEU != null)
            {
                //var passdata2 = passdata.GetValue("shiprequiredTEU");
                ViewBag.shiprequiredTEU = shiprequiredTEU;
            }
            else
            {
                ViewBag.shiprequiredTEU = 0;
            }
            if (scheduleBooking == null)
            {
                return HttpNotFound();
            }
            return View(scheduleBooking);
        }

        // GET: schedulecomfirm/Create
        public ActionResult Create()
        {
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername");
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleDeparture");
            return View();
        }

        // POST: schedulecomfirm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scheduleBookingID,scheduleID,agentID,status,requiredTEU")] scheduleBooking scheduleBooking)
        {
            if (ModelState.IsValid)
            {
                db.scheduleBookings.Add(scheduleBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername", scheduleBooking.agentID);
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleDeparture", scheduleBooking.scheduleID);
            return View(scheduleBooking);
        }

        // GET: schedulecomfirm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scheduleBooking scheduleBooking = db.scheduleBookings.Find(id);
            if (scheduleBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername", scheduleBooking.agentID);
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleDeparture", scheduleBooking.scheduleID);
            return View(scheduleBooking);
        }

        // POST: schedulecomfirm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scheduleBookingID,scheduleID,agentID,status,requiredTEU")] scheduleBooking scheduleBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername", scheduleBooking.agentID);
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleDeparture", scheduleBooking.scheduleID);
            return View(scheduleBooking);
        }

        // GET: schedulecomfirm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scheduleBooking scheduleBooking = db.scheduleBookings.Find(id);
            if (scheduleBooking == null)
            {
                return HttpNotFound();
            }
            return View(scheduleBooking);
        }

        // POST: schedulecomfirm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            scheduleBooking scheduleBooking = db.scheduleBookings.Find(id);
            db.scheduleBookings.Remove(scheduleBooking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult confirm(int id,decimal shiprequiredTEU, decimal requiredTEU,decimal shipTEU)
        {
            var remain = shipTEU - shiprequiredTEU;
            remain = remain - requiredTEU;
            if (remain > 0)
            {
                scheduleBooking scheduleBooking = db.scheduleBookings.Find(id);
                scheduleBooking.status = "confirm";
                db.Entry(scheduleBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                return Content("Cannot confirm because TEU no enought");
            }
        }

        public ActionResult reject(int id)
        {
            scheduleBooking scheduleBooking = db.scheduleBookings.Find(id);
            scheduleBooking.status = "reject";
            db.Entry(scheduleBooking).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
