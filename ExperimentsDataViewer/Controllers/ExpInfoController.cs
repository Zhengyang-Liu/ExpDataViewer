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
        private ExpInfoContext expInfoContextDb = new ExpInfoContext();
        private RunningExpContext runningExpContextDb = new RunningExpContext();
        private ExpInfoDetailContext expInfoDetailContext = new ExpInfoDetailContext();
        private ExpNoConfig expNoConfig = new ExpNoConfig();

        // GET: ExpInfo
        public ActionResult Index()
        {
            ViewBag.RunningExpList = runningExpContextDb.RunningExp.ToList();
            return View(expInfoContextDb.ExpInfoes.ToList());
        }

        // GET: ExpInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfo expInfo = expInfoContextDb.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            var list = expInfoDetailContext.ExpInfoDetails.ToList().Where(detail => detail.ExpNo == id.Value);
            if (list == null || list.Count() <= 0)
            {
                return View(new List<ExpInfoDetail>());
            }
            return View(list);
        }

        // GET: ExpInfo/Chart/5
        public ActionResult Chart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfo expInfo = expInfoContextDb.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            var list = expInfoDetailContext.ExpInfoDetails.ToList().Where(detail => detail.ExpNo == id.Value);
            if (list == null || list.Count() <= 0)
            {
                return View(new List<ExpInfoDetail>());
            }
            return View(list);
        }

        public ActionResult StartExp()
        {
            if (this.HasRunningExp())
                return RedirectToAction("Index");

            int expNo = this.GetAndUpdateExpNo();

            var startTime = DateTime.Now;
            ExpInfo expInfo = new ExpInfo()
            {
                StartTime = startTime,
                ExpNo = expNo
            };
            this.AddExpInfo(expInfo);

            this.AddRunningExp(
                new RunningExp()
                {
                    StartTime = startTime,
                    ExpNo = expNo
                }
            );

            DataManager.runningExp = true;
            DataManager.expNo = expNo;

            return RedirectToAction("Index");
        }

        public ActionResult FinishExp()
        {
            var runningExp = runningExpContextDb.RunningExp.ToList()[0];
            var expSet = expInfoContextDb.ExpInfoes;

            ExpInfo expInfo = expSet.Find(runningExp.ExpNo);
            expInfo.EndTime = DateTime.Now;
            this.Edit(expInfo);

            runningExpContextDb.RunningExp.Remove(runningExp);
            runningExpContextDb.SaveChanges();

            DataManager.runningExp = false;

            return RedirectToAction("Index");
        }

        private bool HasRunningExp()
        {
            bool result = false;
            var runningExpSet = runningExpContextDb.RunningExp;
            if (runningExpSet.Any())
            {
                result = true;
            }
            return result;
        }

        private void AddExpInfo(ExpInfo expInfo)
        {
            if (ModelState.IsValid)
            {
                expInfoContextDb.ExpInfoes.Add(expInfo);
                expInfoContextDb.SaveChanges();
            }
        }

        private void AddRunningExp(RunningExp runningExp)
        {
            runningExpContextDb.RunningExp.Add(runningExp);
            runningExpContextDb.SaveChanges();
        }

        private int GetAndUpdateExpNo()
        {
            int result;

            if (!expNoConfig.expNo.Any())
            {
                expNoConfig.expNo.Add(new ExpNo
                {
                    NextNo = 0
                });
                expNoConfig.SaveChanges();
            }

            ExpNo[] expNoArray = expNoConfig.expNo.ToArray();

            ExpNo expNo = expNoArray[0];
            result = expNo.NextNo;

            expNo.NextNo += 1;
            expNoConfig.Entry(expNo).State = EntityState.Modified;
            expNoConfig.SaveChanges();

            return result;
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
                expInfoContextDb.ExpInfoes.Add(expInfo);
                expInfoContextDb.SaveChanges();
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
            ExpInfo expInfo = expInfoContextDb.ExpInfoes.Find(id);
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
                expInfoContextDb.Entry(expInfo).State = EntityState.Modified;
                expInfoContextDb.SaveChanges();
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
            ExpInfo expInfo = expInfoContextDb.ExpInfoes.Find(id);
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
            ExpInfo expInfo = expInfoContextDb.ExpInfoes.Find(id);
            expInfoContextDb.ExpInfoes.Remove(expInfo);
            expInfoContextDb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                expInfoContextDb.Dispose();
            }
            base.Dispose(disposing);
        }

        public void AddExpDetail(ExpInfoDetail expInfoDetail)
        {
            this.expInfoDetailContext.ExpInfoDetails.Add(expInfoDetail);
            this.expInfoDetailContext.SaveChanges();
        }

        public void AddExpDetail(ExpInfoDetail[] expInfoDetails)
        {
            this.expInfoDetailContext.ExpInfoDetails.AddRange(expInfoDetails);
            this.expInfoDetailContext.SaveChanges();
        }
    }
}
