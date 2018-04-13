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
    public class itemsController : Controller
    {
        private MaerskLineContainerManagementSystemEntities db = new MaerskLineContainerManagementSystemEntities();

        // GET: items
        
        public ActionResult Index(int? id)
        {
            ViewBag.invoiceid = id;
            var items = db.items.Include(i => i.deliveryInvoice).Where(i=>i.deliveryInvoiceID==id).ToList();
            
            return View(items);
        }

        // GET: items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: items/Create
        public ActionResult Create(int? id)
        {
            ViewBag.invoiceid = id;
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itemID,itemName,itemCategory,itemVolume,itemMass,deliveryInvoiceID")] item item)
        {
            if (ModelState.IsValid)
            {
                db.items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = item.deliveryInvoiceID });
            }

            //ViewBag.deliveryInvoiceID = new SelectList(db.deliveryInvoices, "deliveryInvoiceID", "deliveryInvoiceID", item.deliveryInvoiceID);
            return View(item);
        }

        // GET: items/Edit/5
        public ActionResult Edit(int? id,int invoiceid)
        {
            ViewBag.invoiceid = invoiceid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.deliveryInvoiceID = new SelectList(db.deliveryInvoices, "deliveryInvoiceID", "deliveryInvoiceID", item.deliveryInvoiceID);
            return View(item);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "itemID,itemName,itemCategory,itemVolume,itemMass,deliveryInvoiceID")] item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = item.deliveryInvoiceID });
            }
            ViewBag.deliveryInvoiceID = new SelectList(db.deliveryInvoices, "deliveryInvoiceID", "deliveryInvoiceID", item.deliveryInvoiceID);
            return View(item);
        }

        // GET: items/Delete/5
        public ActionResult Delete(int? id,int invoiceid)
        {
            ViewBag.invoiceid = invoiceid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            item item = db.items.Find(id);
            db.items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index",new { id = item.deliveryInvoiceID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Detailsback(int? id)
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
            return RedirectToAction("details", "deliveryInvoices", new { id });
        }

        public ActionResult getinvoice(int? id) {
            ViewBag.invoiceid = id;
            deliveryInvoice deliveryInvoice = db.deliveryInvoices.Where(d => d.deliveryInvoiceID == id).Include(d => d.customer).Include(d => d.scheduleBooking).Include(d => d.scheduleBooking.schedule).FirstOrDefault();
            return PartialView(deliveryInvoice);
        }
    }

    //public class itemanddeliveryInvoice
    //{
    //    public List<item> items { get; set; }
    //    public deliveryInvoice deliveryInvoice { get; set; }
    //}
}
