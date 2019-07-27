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
    public class ExpDataSummaryInfoController : Controller
    {
        private ExpDataSummaryInfoContext db = new ExpDataSummaryInfoContext();

        // GET: ExpDataSummaryInfo
        public ActionResult Index()
        {
            return View(db.ExpDataSummaryInfoes.ToList());
        }

        // GET: ExpDataSummaryInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpDataSummaryInfo expDataSummaryInfo = db.ExpDataSummaryInfoes.Find(id);
            if (expDataSummaryInfo == null)
            {
                return HttpNotFound();
            }
            return View(expDataSummaryInfo);
        }

        // GET: ExpDataSummaryInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpDataSummaryInfo/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpNo,StartTime,EndTime,ExpResultCount,Status")] ExpDataSummaryInfo expDataSummaryInfo)
        {
            if (ModelState.IsValid)
            {
                db.ExpDataSummaryInfoes.Add(expDataSummaryInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expDataSummaryInfo);
        }

        // GET: ExpDataSummaryInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpDataSummaryInfo expDataSummaryInfo = db.ExpDataSummaryInfoes.Find(id);
            if (expDataSummaryInfo == null)
            {
                return HttpNotFound();
            }
            return View(expDataSummaryInfo);
        }

        // POST: ExpDataSummaryInfo/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpNo,StartTime,EndTime,ExpResultCount,Status")] ExpDataSummaryInfo expDataSummaryInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expDataSummaryInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expDataSummaryInfo);
        }

        // GET: ExpDataSummaryInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpDataSummaryInfo expDataSummaryInfo = db.ExpDataSummaryInfoes.Find(id);
            if (expDataSummaryInfo == null)
            {
                return HttpNotFound();
            }
            return View(expDataSummaryInfo);
        }

        // POST: ExpDataSummaryInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpDataSummaryInfo expDataSummaryInfo = db.ExpDataSummaryInfoes.Find(id);
            db.ExpDataSummaryInfoes.Remove(expDataSummaryInfo);
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
