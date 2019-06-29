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
using System.Linq.Expressions;
using System.Globalization;

namespace Nomina2018.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartamentService _departamentService;
        private readonly ISalaryTabulatorService _salaryTabulatorService;
        public EmployeeController(IEmployeeService _employeeService, IDepartamentService _departamentService, ISalaryTabulatorService _salaryTabulatorService)
        {
            this._employeeService = _employeeService;
            this._departamentService = _departamentService;
            this._salaryTabulatorService = _salaryTabulatorService;
        }
        // GET: Employee
        public ActionResult Index()
        {
            var employees = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<EmployeeDTO>>(_employeeService.GetMany(x=>x.Active==true));
            ViewBag.DepartamentId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<DepartamentDTO>>(_departamentService.GetAll()), "Id", "Name");
            ViewBag.SalaryTabulatorId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<SalaryTabulatorDTO>>(_salaryTabulatorService.GetAll()), "Id", "Key");

            return View(employees);
        }
        [HttpPost]
        public ActionResult Search(SearchEmployee searchEmployee)
        {
            Calendar cal = new UmAlQuraCalendar();

            var many = _employeeService.GetByFilters(searchEmployee.KeyEmplooye, searchEmployee.Name, searchEmployee.DepartamentId, searchEmployee.SalaryTabulatorId);
             
            var employees = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<EmployeeDTO>>(many);
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDTO employeeDTO = AutoMapperConfiguration.Instance.Mapper.Map<EmployeeDTO>(_employeeService.GetByID(id));
            if (employeeDTO == null)
            {
                return HttpNotFound();
            }
            return View(employeeDTO);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            
            ViewBag.DepartamentId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map <IEnumerable <DepartamentDTO> > (_departamentService.GetAll()), "Id", "Name");
            ViewBag.SalaryTabulatorId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map < IEnumerable <SalaryTabulatorDTO>>(_salaryTabulatorService.GetAll()), "Id", "Key");
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,JobNumber,FirstName,LastName,Address,Telefone,Gender,HireDate,Active,DepartamentId,SalaryTabulatorId")] EmployeeDTO employeeDTO)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _employeeService.Insert(AutoMapperConfiguration.Instance.Mapper.Map<Employee>(employeeDTO));
                
        //        return RedirectToAction("Index");
        //    }
        //    var departamentsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<DepartamentDTO>>(_departamentService.GetAll());
        //    var salaryTabulatorsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<SalaryTabulatorDTO>>(_salaryTabulatorService.GetAll());
        //    ViewBag.DepartamentId = new SelectList(departamentsDTO, "Id", "Name", employeeDTO.DepartamentId);
        //    ViewBag.SalaryTabulatorId = new SelectList(salaryTabulatorsDTO, "Id", "Key", employeeDTO.SalaryTabulatorId);
        //    return View(employeeDTO);
        //}
        [HttpPost]
        [ValidateAjax]
        public JsonResult CreateJson(EmployeeDTO employeeDTO)
        {

            var employee = AutoMapperConfiguration.Instance.Mapper.Map<Employee>(employeeDTO);
            _employeeService.Insert(employee);
            return Json(employee, JsonRequestBehavior.AllowGet);

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDTO employeeDTO = AutoMapperConfiguration.Instance.Mapper.Map<EmployeeDTO>(_employeeService.Get(x=>x.Id==id && x.Active==true));
            if (employeeDTO == null)
            {
                return HttpNotFound();
            }
            var departamentsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<DepartamentDTO>>(_departamentService.GetAll());
            var salaryTabulatorsDTO = AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<SalaryTabulatorDTO>>(_salaryTabulatorService.GetAll());

            ViewBag.DepartamentId = new SelectList(departamentsDTO, "Id", "Name", employeeDTO.DepartamentId);
            ViewBag.SalaryTabulatorId = new SelectList(salaryTabulatorsDTO, "Id", "Key", employeeDTO.SalaryTabulatorId);
            return View(employeeDTO);
        }
        [HttpPost]
        [ValidateAjax]
        public JsonResult EditJson(EmployeeDTO employeeDTO)
        {
            
                var employee = AutoMapperConfiguration.Instance.Mapper.Map<Employee>(employeeDTO);
                _employeeService.Update(employee);
                return Json(employee, JsonRequestBehavior.AllowGet);
            
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDTO employeeDTO = AutoMapperConfiguration.Instance.Mapper.Map<EmployeeDTO>(_employeeService.Get(x => x.Id == id && x.Active == true));
            if (employeeDTO == null)
            {
                return HttpNotFound();
            }
            return View(employeeDTO);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = AutoMapperConfiguration.Instance.Mapper.Map<Employee>(_employeeService.GetByID(id));
            _employeeService.Delete(employee);
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
