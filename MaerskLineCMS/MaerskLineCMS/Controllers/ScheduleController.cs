using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaerskLineCMS.Logic;
using MaerskLineCMS.Models;

namespace MaerskLineCMS.Controllers
{
    [SessionAdmin]
    public class ScheduleController : Controller
    {
        private MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();

        // GET: Schedule
        public ActionResult Index()
        {
            var schedules = db.schedules.Include(s => s.ship).Include(s => s.admin);
            ViewBag.shipID = new SelectList(db.ships, "shipID", "shipName");
            ViewBag.adminID = new SelectList(db.admins, "adminID", "adminUsername");
            return View(schedules.ToList());
        }

        public JsonResult GetSchedule()
        {
            List<scheduleData> ScheduleInfo = db.Database.SqlQuery<scheduleData>("SELECT s.*,cast(ISNULL(t2.requiredTEU,0)as decimal(18,2)) as requiredTEU,t3.* FROM schedule s LEFT JOIN( select s2.scheduleID, sum(s2.requiredTEU) as requiredTEU from scheduleBooking s2 where s2.status='confirm' group by s2.scheduleID ) as t2 on s.scheduleID = t2.scheduleID left join (select * from ship) as t3 on t3.shipID=s.shipID").ToList();
        
            return new JsonResult { Data = ScheduleInfo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        // GET: Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scheduleData schedule = db.Database.SqlQuery<scheduleData>("SELECT s.*,cast(ISNULL(t2.requiredTEU,0)as decimal(18,2)) as requiredTEU,t3.* FROM schedule s LEFT JOIN( select s2.scheduleID, sum(s2.requiredTEU) as requiredTEU from scheduleBooking s2 where s2.status='confirm' group by s2.scheduleID) as t2 on s.scheduleID = t2.scheduleID left join (select * from ship) as t3 on t3.shipID=s.shipID where s.scheduleID = " + id+"").FirstOrDefault();
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            ViewBag.shipID = new SelectList(db.ships, "shipID", "shipName");
            ViewBag.adminID = new SelectList(db.admins, "adminID", "adminUsername");
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scheduleID,scheduleDetail,scheduleDeparture,scheduleDepartureDate,scheduleDepartureTime,scheduleArrival,scheduleArrivalDate,scheduleArrivalTime,shipID,adminID")] schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.shipID = new SelectList(db.ships, "shipID", "shipName", schedule.shipID);
            ViewBag.adminID = new SelectList(db.admins, "adminID", "adminUsername", schedule.adminID);
            return View(schedule);
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule schedule = db.schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.shipID = new SelectList(db.ships, "shipID", "shipName", schedule.shipID);
            ViewBag.adminID = new SelectList(db.admins, "adminID", "adminUsername", schedule.adminID);
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scheduleID,scheduleDetail,scheduleDeparture,scheduleDepartureDate,scheduleDepartureTime,scheduleArrival,scheduleArrivalDate,scheduleArrivalTime,shipID,adminID")] schedule schedule)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(schedule).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
            ViewBag.shipID = new SelectList(db.ships, "shipID", "shipName", schedule.shipID);
            ViewBag.adminID = new SelectList(db.admins, "adminID", "adminUsername", schedule.adminID);
            return View(schedule);
        }

        public void UpdateSchedule(schedule schedule)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(schedule).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scheduleData schedule = db.Database.SqlQuery<scheduleData>("SELECT s.*,cast(ISNULL(t2.requiredTEU,0)as decimal(18,2)) as requiredTEU,t3.* FROM schedule s LEFT JOIN( select s2.scheduleID, sum(s2.requiredTEU) as requiredTEU from scheduleBooking s2 where s2.status='confirm' group by s2.scheduleID) as t2 on s.scheduleID = t2.scheduleID left join (select * from ship) as t3 on t3.shipID=s.shipID where s.scheduleID = " + id + "").FirstOrDefault();
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            schedule schedule = db.schedules.Find(id);
            db.schedules.Remove(schedule);
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

        public void DeleteSchedule(int scheduleid)
        {
            schedule schedule = db.schedules.Find(scheduleid);
            db.schedules.Remove(schedule);
            db.SaveChanges();
        }
    }
}
