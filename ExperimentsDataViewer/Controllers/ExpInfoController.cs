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
        // GET: ExpInfo
        public ActionResult Index()
        {
            ViewBag.RunningExpList = DataManager.runningExpContextDb.RunningExp.ToList();
            return View(DataManager.expInfoContextDb.ExpInfoes.ToList());
        }

        // GET: ExpInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpInfo expInfo = DataManager.expInfoContextDb.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            var list = DataManager.expInfoDetailContext.ExpInfoDetails.ToList().Where(detail => detail.ExpNo == id.Value);
            list = list.OrderBy(o => o.CollectedTime).ToList();
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
            ExpInfo expInfo = DataManager.expInfoContextDb.ExpInfoes.Find(id);
            if (expInfo == null)
            {
                return HttpNotFound();
            }
            IEnumerable<ExpInfoDetail> list = DataManager.expInfoDetailContext.ExpInfoDetails.ToList().Where(detail => detail.ExpNo == id.Value);
            list = list.OrderBy(o => o.CollectedTime).ToList();
            if (list == null || list.Count() <= 0)
            {
                return View(new List<ExpInfoDetail>());
            }
            return View(list);
        }

        public ActionResult StartExp()
        {
            //if (this.HasRunningExp())
            //    return RedirectToAction("Index");

            //var startTime = DateTime.Now;
            //ExpInfo expInfo = new ExpInfo()
            //{
            //    StartTime = startTime
            //};
            //ExpInfo returnExpInfo = this.AddExpInfo(expInfo);

            //this.AddRunningExp(
            //    new RunningExp()
            //    {
            //        StartTime = startTime
            //    }
            //);

            //DataManager.runningExp = true;
            //DataManager.expNo = returnExpInfo.ExpNo;

            Pipe.StartExpt();

            return RedirectToAction("Index");
        }

        public ActionResult FinishExp()
        {
            //var runningExp = DataManager.runningExpContextDb.RunningExp.ToList()[0];
            //var expSet = DataManager.expInfoContextDb.ExpInfoes;

            //ExpInfo expInfo = expSet.Find(runningExp.ExpNo);
            //expInfo.EndTime = DateTime.Now;
            //this.Edit(expInfo);

            //DataManager.runningExpContextDb.RunningExp.Remove(runningExp);
            //DataManager.runningExpContextDb.SaveChanges();

            //DataManager.runningExp = false;

            Pipe.EndExpt();

            return RedirectToAction("Index");
        }

        private bool HasRunningExp()
        {
            bool result = false;
            var runningExpSet = DataManager.runningExpContextDb.RunningExp;
            if (runningExpSet.Any())
            {
                result = true;
            }
            return result;
        }

        private ExpInfo AddExpInfo(ExpInfo expInfo)
        {
            ExpInfo result = null;
            if (ModelState.IsValid)
            {
                result = DataManager.expInfoContextDb.ExpInfoes.Add(expInfo);
                DataManager.expInfoContextDb.SaveChanges();
            }
            return result;
        }

        private void AddRunningExp(RunningExp runningExp)
        {
            DataManager.runningExpContextDb.RunningExp.Add(runningExp);
            DataManager.runningExpContextDb.SaveChanges();
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
                DataManager.expInfoContextDb.ExpInfoes.Add(expInfo);
                DataManager.expInfoContextDb.SaveChanges();
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
            ExpInfo expInfo = DataManager.expInfoContextDb.ExpInfoes.Find(id);
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
                DataManager.expInfoContextDb.Entry(expInfo).State = EntityState.Modified;
                DataManager.expInfoContextDb.SaveChanges();
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
            ExpInfo expInfo = DataManager.expInfoContextDb.ExpInfoes.Find(id);
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
            ExpInfo expInfo = DataManager.expInfoContextDb.ExpInfoes.Find(id);
            DataManager.expInfoContextDb.ExpInfoes.Remove(expInfo);
            DataManager.expInfoContextDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
