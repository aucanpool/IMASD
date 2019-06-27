using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMASD.DATA;
using IMASD.DATA.Entities;

namespace Nomina2018.Controllers
{
    public class SalaryTabulatorController : Controller
    {
        private MainContext db = new MainContext();

        // GET: SalaryTabulator
        public ActionResult Index()
        {
            var salaryTabulators = db.SalaryTabulators.Include(s => s.Job);
            return View(salaryTabulators.ToList());
        }

        // GET: SalaryTabulator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryTabulator salaryTabulator = db.SalaryTabulators.Find(id);
            if (salaryTabulator == null)
            {
                return HttpNotFound();
            }
            return View(salaryTabulator);
        }

        // GET: SalaryTabulator/Create
        public ActionResult Create()
        {
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Key");
            return View();
        }

        // POST: SalaryTabulator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Key,JobId,TabulatorLevel,Hourlywages,AnnualHolidayBonus,AnnualBonusDays,AnnualVacationDays,Active")] SalaryTabulator salaryTabulator)
        {
            if (ModelState.IsValid)
            {
                db.SalaryTabulators.Add(salaryTabulator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Key", salaryTabulator.JobId);
            return View(salaryTabulator);
        }

        // GET: SalaryTabulator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryTabulator salaryTabulator = db.SalaryTabulators.Find(id);
            if (salaryTabulator == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Key", salaryTabulator.JobId);
            return View(salaryTabulator);
        }

        // POST: SalaryTabulator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Key,JobId,TabulatorLevel,Hourlywages,AnnualHolidayBonus,AnnualBonusDays,AnnualVacationDays,Active")] SalaryTabulator salaryTabulator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryTabulator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "Key", salaryTabulator.JobId);
            return View(salaryTabulator);
        }

        // GET: SalaryTabulator/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryTabulator salaryTabulator = db.SalaryTabulators.Find(id);
            if (salaryTabulator == null)
            {
                return HttpNotFound();
            }
            return View(salaryTabulator);
        }

        // POST: SalaryTabulator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryTabulator salaryTabulator = db.SalaryTabulators.Find(id);
            db.SalaryTabulators.Remove(salaryTabulator);
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
