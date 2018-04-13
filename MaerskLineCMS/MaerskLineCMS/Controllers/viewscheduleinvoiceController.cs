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
    public class viewscheduleinvoiceController : Controller
    {
        private MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();

        // GET: viewscheduleinvoice
        public ActionResult Index()
        {
            var scheduleBookings = db.scheduleBookings.Where(s=>s.status=="confirm").Include(s => s.agent).Include(s => s.schedule);
            return View(scheduleBookings.ToList());
        }

        // GET: viewscheduleinvoice/Details/5
        public ActionResult Details(int? id)
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
            return View("index2",scheduleBooking);
        }

        public ActionResult getinvoice(int? id)
        {
            ViewBag.invoiceid = id;
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Where(d => d.deliveryInvoiceID == id).Include(d => d.customer).Include(d => d.scheduleBooking).Include(d => d.scheduleBooking.schedule).FirstOrDefault();
            return PartialView(deliveryInvoice);
        }

        public ActionResult Detailsback(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schedule = db.deliveryInvoices.Where(s => s.deliveryInvoiceID == id).FirstOrDefault();
            int scheduleid = schedule.scheduleBookingID;
            var deliveryInvoices = db.deliveryInvoices.Where(s => s.scheduleBookingID == scheduleid).Include(d => d.agent).Include(d => d.customer).Include(d => d.scheduleBooking).Include(d => d.scheduleBooking.schedule);
            if (deliveryInvoices == null)
            {
                return HttpNotFound();
            }
            return View("index2", deliveryInvoices);/*RedirectToAction("details", "deliveryInvoices", new { id })*/;
        }

        public ActionResult Invoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deliveryInvoices = db.deliveryInvoices.Where(s => s.scheduleBookingID == id).Include(d => d.agent).Include(d => d.customer).Include(d => d.scheduleBooking).Include(d => d.scheduleBooking.schedule);
            if (deliveryInvoices == null)
            {
                return HttpNotFound();
            }
            return View("index2", deliveryInvoices);
        }
        
            public ActionResult Detailsitem(int? id)
        {
            ViewBag.invoiceid = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemlist = db.items.Include(i => i.deliveryInvoice).Where(i => i.deliveryInvoiceID == id).ToList();
            if (itemlist == null)
            {
                return HttpNotFound();
            }
            return View("index3", itemlist);
        }

        // GET: viewscheduleinvoice/Create
        public ActionResult Create()
        {
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername");
            ViewBag.scheduleID = new SelectList(db.schedules, "scheduleID", "scheduleDeparture");
            return View();
        }

        // POST: viewscheduleinvoice/Create
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

        // GET: viewscheduleinvoice/Edit/5
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

        // POST: viewscheduleinvoice/Edit/5
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

        // GET: viewscheduleinvoice/Delete/5
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

        // POST: viewscheduleinvoice/Delete/5
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
    }
}
