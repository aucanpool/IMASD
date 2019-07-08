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
using Services.Interface;
using Nomina2018.Mapping;
using Nomina2018.Models;
using Nomina2018.Models.JSON;

namespace Nomina2018.Controllers
{
    public class SalaryTabulatorController : Controller
    {
        private readonly ISalaryTabulatorService salaryTabuladorService;
        private readonly IJobService jobService;
        public SalaryTabulatorController(ISalaryTabulatorService salaryTabuladorService, IJobService jobService)
        {
            this.jobService = jobService;
            this.salaryTabuladorService = salaryTabuladorService;
        }
        // GET: SalaryTabulator
        public ActionResult Index()
        {
            var salaryTabulators = salaryTabuladorService.GetMany(x=>x.Active==true);
            var salaryTabulatorsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<SalaryTabulatorDTO>>(salaryTabulators);
            return View(salaryTabulatorsDTO);
        }

        // GET: SalaryTabulator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var salaryTabulator = salaryTabuladorService.Get(x => x.Id == id && x.Active==true);
            var salaryTabulatorDTO = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulatorDTO>(salaryTabulator);
            if (salaryTabulator == null)
            {
                return HttpNotFound();
            }
            return View(salaryTabulatorDTO);
        }

        // GET: SalaryTabulator/Create
        public ActionResult Create()
        {
            var jobs = jobService.GetMany(x=>x.Active==true);
            var jobsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<JobDTO>>(jobs);
            ViewBag.JobId = new SelectList(jobsDTO, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAjax]
        public JsonResult CreateJson(SalaryTabulatorDTO salaryTabuladorDTO)
        {
            if ((int) salaryTabuladorDTO.TabulatorLevel==0)
            {
                Console.Write("No select Tabulator level");
                var data = new ErrorByKey[1] { new ErrorByKey { key = "TabulatorLevel", errors = new string[1] { "Requerido" } } };
                return new JsonHttpStatusResult(data, HttpStatusCode.BadRequest);
            }
            var employee = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulator>(salaryTabuladorDTO);
            salaryTabuladorService.Insert(employee);
            return Json(employee, JsonRequestBehavior.DenyGet);
        }
        
        // GET: SalaryTabulator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryTabulator salaryTabulator = salaryTabuladorService.Get(x=>x.Id==id && x.Active==true);
            if (salaryTabulator == null)
            {
                return HttpNotFound();
            }
            var salaryTabulatorDTO = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulatorDTO>(salaryTabulator);
            var jobs = jobService.GetMany(x => x.Active == true);
            var jobsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<JobDTO>>(jobs);
            
            ViewBag.JobId = new SelectList(jobsDTO, "Id", "Name", salaryTabulatorDTO.JobId);
            return View(salaryTabulatorDTO);
        }

        [HttpPost]
        [ValidateAjax]
        public JsonResult EditJson(SalaryTabulatorDTO salaryTabulatorDTO)
        {
            if ((int)salaryTabulatorDTO.TabulatorLevel==0)
            {
                Console.Write("No select tabulator level");
                var data = new ErrorByKey[1] { new ErrorByKey { key = "TabulatorLevel", errors = new string[1] { "Requerido" } } };
                return new JsonHttpStatusResult(data, HttpStatusCode.BadRequest);
            }
            var salaryTabulator = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulator>(salaryTabulatorDTO);
            salaryTabuladorService.Update(salaryTabulator);
            return Json(salaryTabulatorDTO,JsonRequestBehavior.DenyGet);
        }
        
        // GET: SalaryTabulator/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryTabulator salaryTabulator = salaryTabuladorService.Get(x=>x.Active==true && x.Id==id);
            if (salaryTabulator == null)
            {
                return HttpNotFound();
            }
            SalaryTabulatorDTO salaryTabulatorDTO = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulatorDTO>(salaryTabulator);
            return View(salaryTabulatorDTO);
        }

        // POST: SalaryTabulator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryTabulator salaryTabulator = salaryTabuladorService.Get(x=>x.Id==id && x.Active==true);
            if (salaryTabulator==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salaryTabuladorService.Delete(salaryTabulator);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
