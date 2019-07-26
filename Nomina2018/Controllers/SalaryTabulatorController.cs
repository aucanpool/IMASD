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
using System.Text;
using IMASD.Base.Utilities;
using IMASD.Base.DataTablesDTO;

namespace Nomina2018.Controllers
{
    public class SalaryTabulatorController : Controller
    {
        private readonly ISalaryTabulatorService salaryTabuladorService;
        private readonly IJobService jobService;
        private StringBuilder sb;
        public SalaryTabulatorController(ISalaryTabulatorService salaryTabuladorService, IJobService jobService)
        {
            this.jobService = jobService;
            this.salaryTabuladorService = salaryTabuladorService;
            sb = new StringBuilder();
        }
        // GET: SalaryTabulator
        public ActionResult Index()
        {
            sb.Clear();
            sb.Append("Retrieve Salary tabulators");
            SeriLogHelper.WriteInformation(null,sb.ToString());
            var salaryTabulatorsDTO = new List<SalaryTabulatorDTO>();
            return View(salaryTabulatorsDTO);
        }
        [HttpPost]
        public JsonResult getAll(DataTableInput input)
        {
            DataTableOutput<SalaryTabulator> salaryTabulators = new DataTableOutput<SalaryTabulator>();
           DataTableOutput<SalaryTabulatorDTO> salaryTabulatorsDTO = new DataTableOutput<SalaryTabulatorDTO>();
            try
            {
                salaryTabulators = salaryTabuladorService.GetSalarysTabulators(input);
                salaryTabulatorsDTO = AutoMapperConfiguration.Instance.Mapper.Map<DataTableOutput<SalaryTabulatorDTO>>(salaryTabulators);

            }
            catch (Exception e)
            {
                sb.Clear();
                sb.Append("An error has ocurred when it try to retrieve Salary Tabulators");
                SeriLogHelper.WriteError(e, sb.ToString());
                return new JsonHttpStatusResult(e.Message, HttpStatusCode.InternalServerError);
            }
            return Json(salaryTabulatorsDTO, JsonRequestBehavior.DenyGet);
        }

        // GET: SalaryTabulator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                sb.Clear();
                sb.Append("BadRequest: It needs the Id for request the SalaryTabulator details");
                SeriLogHelper.WriteError(null, sb.ToString());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var salaryTabulator = salaryTabuladorService.Get(x => x.Id == id );
            var salaryTabulatorDTO = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulatorDTO>(salaryTabulator);
            if (salaryTabulatorDTO == null)
            {
                sb.Clear();
                sb.AppendFormat("Not found the salary tabulator with id {0}", id);
                SeriLogHelper.WriteWarning(null, sb.ToString());
                return HttpNotFound();
            }
            return View(salaryTabulatorDTO);
        }

        // GET: SalaryTabulator/Create
        [ValidateRequestAjax]
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
                sb.Clear();
                sb.Append("Is required a tabulator level to create a salary tabulator");
                SeriLogHelper.WriteWarning(null, sb.ToString());
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
                sb.Clear();
                sb.Append("BadRequest: It needs the Id for edit a Salary Tabulator");
                SeriLogHelper.WriteError(null, sb.ToString());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryTabulator salaryTabulator = salaryTabuladorService.Get(x=>x.Id==id );
            if (salaryTabulator == null)
            {
                sb.Clear();
                sb.AppendFormat("Salary tabulator  do not found with the id {0}", id);
                SeriLogHelper.WriteWarning(null, sb.ToString());
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
                sb.Clear();
                sb.Append("It needs a tabulator level to edit a salary tabulator");
                SeriLogHelper.WriteWarning(null, sb.ToString());
                var data = new ErrorByKey[1] { new ErrorByKey { key = "TabulatorLevel", errors = new string[1] { "Requerido" } } };
                return new JsonHttpStatusResult(data, HttpStatusCode.BadRequest);
            }
            var salaryTabulator = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulator>(salaryTabulatorDTO);
            salaryTabuladorService.Update(salaryTabulator);
            return Json(salaryTabulatorDTO,JsonRequestBehavior.DenyGet);
        }

        // GET: SalaryTabulator/Active/true/5
        [HttpDelete]
        [Route("SalaryTabulator/Active/{active}/{id}")]
        public ActionResult Delete(bool active,int? id)
        {
            if (id == null)
            {
                sb.Clear();
                sb.Append("It needes a Id to delete a Salary tabulator");
                SeriLogHelper.WriteError(null, sb.ToString());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                SalaryTabulator salaryTabulator = salaryTabuladorService.Get(x => x.Id == id && x.Active == !active);
                if (salaryTabulator == null)
                {
                    return HttpNotFound();
                }
                salaryTabulator.Active = active;
                salaryTabuladorService.Update(salaryTabulator);
                SalaryTabulatorDTO salaryTabulatorDTO = AutoMapperConfiguration.Instance.Mapper.Map<SalaryTabulatorDTO>(salaryTabulator);
                return Json(salaryTabulatorDTO, JsonRequestBehavior.DenyGet);

            }
            catch (Exception e)
            {
                sb.Clear();
                sb.Append(e.ToString());
                return new JsonHttpStatusResult(sb.ToString(), HttpStatusCode.InternalServerError);
            }
            
            
            
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
