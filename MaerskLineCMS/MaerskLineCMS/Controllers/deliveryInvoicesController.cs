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
    public class deliveryInvoicesController : Controller
    {
        private MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();

        // GET: deliveryInvoices
        
        public ActionResult Index()
        {
            int useiid = Convert.ToInt32(Session["UserID"]);

            var deliveryInvoices = db.deliveryInvoices.Where(s => s.agentID == useiid).Include(d => d.agent).Include(d => d.customer).Include(d => d.scheduleBooking).Include(d => d.scheduleBooking.schedule);
            return View(deliveryInvoices.ToList());
        }

        // GET: deliveryInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Find(id);
            if (deliveryInvoice == null)
            {
                return HttpNotFound();
            }
            return View(deliveryInvoice);
        }

        // GET: deliveryInvoices/Create
        public ActionResult Create()
        {
            int useiid = Convert.ToInt32(Session["UserID"]);
            ViewBag.agentID = new SelectList(db.agents.Where(c => c.agentID == useiid), "agentID", "agentUsername");
            ViewBag.customerID = new SelectList(db.customers.Where(c=>c.agentID== useiid), "customerID", "customerName");
            ViewBag.scheduleBookingID = new SelectList(db.scheduleBookings.Where(c => c.agentID == useiid), "scheduleBookingID", "scheduleBookingID");
            return View();
        }

        // POST: deliveryInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deliveryInvoiceID,scheduleBookingID,agentID,customerID,invoiceTEU")] deliveryInvoice deliveryInvoice)
        {
            if (ModelState.IsValid)
            {
                db.deliveryInvoices.Add(deliveryInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int useiid = Convert.ToInt32(Session["UserID"]);
            ViewBag.agentID = new SelectList(db.agents.Where(c => c.agentID == useiid), "agentID", "agentUsername");
            ViewBag.customerID = new SelectList(db.customers.Where(c => c.agentID == useiid), "customerID", "customerName");
            ViewBag.scheduleBookingID = new SelectList(db.scheduleBookings.Where(c => c.agentID == useiid), "scheduleBookingID", "scheduleBookingID");
            return View(deliveryInvoice);
        }

        // GET: deliveryInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Find(id);
            if (deliveryInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername", deliveryInvoice.agentID);
            ViewBag.customerID = new SelectList(db.customers, "customerID", "customerEmail", deliveryInvoice.customerID);
            ViewBag.scheduleBookingID = new SelectList(db.scheduleBookings, "scheduleBookingID", "status", deliveryInvoice.scheduleBookingID);
            return View(deliveryInvoice);
        }

        // POST: deliveryInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deliveryInvoiceID,scheduleBookingID,agentID,customerID")] deliveryInvoice deliveryInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.agentID = new SelectList(db.agents, "agentID", "agentUsername", deliveryInvoice.agentID);
            ViewBag.customerID = new SelectList(db.customers, "customerID", "customerEmail", deliveryInvoice.customerID);
            ViewBag.scheduleBookingID = new SelectList(db.scheduleBookings, "scheduleBookingID", "status", deliveryInvoice.scheduleBookingID);
            return View(deliveryInvoice);
        }

        // GET: deliveryInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Find(id);
            if (deliveryInvoice == null)
            {
                return HttpNotFound();
            }
            return View(deliveryInvoice);
        }

        // POST: deliveryInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Find(id);
            db.deliveryInvoices.Remove(deliveryInvoice);
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

        public ActionResult itemlist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Find(id);
            if (deliveryInvoice == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("index", "items" , new {  id });
        }


    }
}
