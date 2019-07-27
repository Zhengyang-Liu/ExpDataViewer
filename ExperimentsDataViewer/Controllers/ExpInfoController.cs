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
        private RunningExpContext runningExpContextDb = new RunningExpContext();

        // GET: ExpInfo
        public ActionResult Index()
        {
            ViewBag.RunningExpList = runningExpContextDb.RunningExp.ToList();
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

        public ActionResult StartExp()
        {
            if (this.HasRunningExp())
                return RedirectToAction("Index");

            var startTime = DateTime.Now;
            ExpInfo expInfo = new ExpInfo()
            {
                StartTime = startTime
            };
            this.AddExpInfo(expInfo);

            this.AddRunningExp(
                new RunningExp()
                {
                    StartTime = startTime
                }
            );
            return RedirectToAction("Index");
        }

        public ActionResult FinishExp()
        {
            var runningExp = runningExpContextDb.RunningExp.ToList()[0];
            var expSet = db.ExpInfoes;

            ExpInfo expInfo = expSet.Find(runningExp.ExpNo);
            expInfo.EndTime = DateTime.Now;
            this.Edit(expInfo);

            runningExpContextDb.RunningExp.Remove(runningExp);
            runningExpContextDb.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool HasRunningExp()
        {
            var runningExpSet = runningExpContextDb.RunningExp;
            if (runningExpSet == null || runningExpSet.Count() <= 0)
            {
                return false;
            }
            return true;
        }

        private void AddExpInfo(ExpInfo expInfo)
        {
            if (ModelState.IsValid)
            {
                db.ExpInfoes.Add(expInfo);
                db.SaveChanges();
            }
        }

        private void AddRunningExp(RunningExp runningExp)
        {
            runningExpContextDb.RunningExp.Add(runningExp);
            runningExpContextDb.SaveChanges();
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
