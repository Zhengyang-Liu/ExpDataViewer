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
    public class ExpInfoController : Controller
    {
        private ExpInfoContext db = new ExpInfoContext();

        // GET: ExpInfo
        public ActionResult Index()
        {
            return View(db.ExpInfoes.ToList());
        }

        // GET: ExpInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfo expInfo = db.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            return View(expInfo);
        }

        // GET: ExpInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpNo,StartTime,EndTime,ExpResultCount,Status")] ExpInfo expInfo)
        {
            if (ModelState.IsValid)
            {
                db.ExpInfoes.Add(expInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expInfo);
        }

        // GET: ExpInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfo expInfo = db.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            return View(expInfo);
        }

        // POST: ExpInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpNo,StartTime,EndTime,ExpResultCount,Status")] ExpInfo expInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expInfo);
        }

        // GET: ExpInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfo expInfo = db.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            return View(expInfo);
        }

        // POST: ExpInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpInfo expInfo = db.ExpInfoes.Find(id);
            db.ExpInfoes.Remove(expInfo);
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
