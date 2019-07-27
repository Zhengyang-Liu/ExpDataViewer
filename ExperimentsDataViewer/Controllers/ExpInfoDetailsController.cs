using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExperimentsDataViewer.Models;

namespace ExperimentsDataViewer.Controllers
{
    public class ExpInfoDetailsController : Controller
    {
        private ExpInfoDetailContext db = new ExpInfoDetailContext();

        // GET: ExpInfoDetails
        public ActionResult Index()
        {
            return View(db.ExpInfoDetails.ToList());
        }

        // GET: ExpInfoDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfoDetail expInfoDetail = db.ExpInfoDetails.Find(id);
            if (expInfoDetail == null)
            {
                return HttpNotFound();
            }
            return View(expInfoDetail);
        }

        // GET: ExpInfoDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpInfoDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpNo,CollectedTime,Acceleration")] ExpInfoDetail expInfoDetail)
        {
            if (ModelState.IsValid)
            {
                db.ExpInfoDetails.Add(expInfoDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expInfoDetail);
        }

        // GET: ExpInfoDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfoDetail expInfoDetail = db.ExpInfoDetails.Find(id);
            if (expInfoDetail == null)
            {
                return HttpNotFound();
            }
            return View(expInfoDetail);
        }

        // POST: ExpInfoDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpNo,CollectedTime,Acceleration")] ExpInfoDetail expInfoDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expInfoDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expInfoDetail);
        }

        // GET: ExpInfoDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfoDetail expInfoDetail = db.ExpInfoDetails.Find(id);
            if (expInfoDetail == null)
            {
                return HttpNotFound();
            }
            return View(expInfoDetail);
        }

        // POST: ExpInfoDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpInfoDetail expInfoDetail = db.ExpInfoDetails.Find(id);
            db.ExpInfoDetails.Remove(expInfoDetail);
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
