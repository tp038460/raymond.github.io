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
    [SessionAgent]
    public class scheduleBookingsController : Controller
    {
        private
            MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();

        // GET: scheduleBookings
        public ActionResult Index()
        {
            int useiid = Convert.ToInt32(Session["UserID"]);

            var scheduleBookings = db.scheduleBookings.Where(s => s.agentID == useiid).Include(s => s.agent).Include(s => s.schedule).ToList();
            //.Where(x => x.agentID == (int)Session["UserID"])
            return View(scheduleBookings);
        }

        // GET: scheduleBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var scheduleBooking = db.scheduleBookings.Find(id);//Include(i => i.schedule.ship).SingleOrDefault(x => x.scheduleBookingID == id);
            
            if (scheduleBooking == null)
            {
                return HttpNotFound();
            }
            return View(scheduleBooking);
        }

        // GET: scheduleBookings/Create
        public ActionResult Create()
        {
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername");
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleDeparture");
            return View();
        }

        // POST: scheduleBookings/Create
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

        // GET: scheduleBookings/Edit/5
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

        // POST: scheduleBookings/Edit/5
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

        // GET: scheduleBookings/Delete/5
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

        // POST: scheduleBookings/Delete/5
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




        public JsonResult GetSchedule()
        {
            List<scheduleData> ScheduleInfo = db.Database.SqlQuery<scheduleData>("SELECT s.*,cast(ISNULL(t2.requiredTEU,0)as decimal(18,2)) as requiredTEU,t3.* FROM schedule s LEFT JOIN( select s2.scheduleID, sum(s2.requiredTEU) as requiredTEU from scheduleBooking s2 where s2.status='confirm' group by s2.scheduleID ) as t2 on s.scheduleID = t2.scheduleID left join (select * from ship) as t3 on t3.shipID=s.shipID").ToList();

            return new JsonResult { Data = ScheduleInfo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public void bookschedule(int scheduleID,decimal requiredTEUbook, int agentID)
        {
            scheduleBooking scheduleBooking = new scheduleBooking();
            scheduleBooking.agentID = agentID;
            scheduleBooking.requiredTEU = requiredTEUbook;
            scheduleBooking.scheduleID = scheduleID;
            scheduleBooking.status = "pending";
            db.scheduleBookings.Add(scheduleBooking);
            db.SaveChanges();

        }
    }
}
