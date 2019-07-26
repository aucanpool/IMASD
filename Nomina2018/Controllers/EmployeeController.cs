using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using IMASD.DATA.Entities;
using Services.Interface;
using Nomina2018.Mapping;
using Nomina2018.Models;
using Nomina2018.Models.JSON;
using IMASD.Base.Utilities;
using System.Text;
using IMASD.Base.DataTablesDTO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nomina2018.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartamentService _departamentService;
        private readonly ISalaryTabulatorService _salaryTabulatorService;
        private StringBuilder sb;
        public EmployeeController(IEmployeeService _employeeService, IDepartamentService _departamentService, ISalaryTabulatorService _salaryTabulatorService)
        {
            this._employeeService = _employeeService;
            this._departamentService = _departamentService;
            this._salaryTabulatorService = _salaryTabulatorService;
            sb = new StringBuilder();
        }
        // GET: Employee
        public ActionResult Index()
        {
            sb.Clear();
            sb.Append("Retrieve employees");
            SeriLogHelper.WriteInformation(null,sb.ToString());
            var employees = new List<EmployeeDTO>();
            ViewBag.DepartamentId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<DepartamentDTO>>(_departamentService.GetMany(x=>x.Active==true)), "Id", "Name");
            ViewBag.SalaryTabulatorId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<SalaryTabulatorDTO>>(_salaryTabulatorService.GetMany(x => x.Active == true)), "Id", "Key");

            return View(employees);
        }
        [HttpPost]
        public JsonResult CustomServerSideSearchAction(DataTableVM<SearchEmployee> model)
        {
            DataTableOutput<Employee> employees = new DataTableOutput<Employee>();
            DataTableOutput<EmployeeDTO> employeesDTO = new DataTableOutput<EmployeeDTO>();
            try
            {
                employees = _employeeService.GetByFilters(model.Filters.KeyEmplooye?? null, 
                    model.Filters.Name ?? null, model.Filters.DepartamentId??null, model.Filters.SalaryTabulatorId??null, model.dataTablesInput??null);
                employeesDTO = AutoMapperConfiguration.Instance.Mapper.Map<DataTableOutput<EmployeeDTO>>(employees);
            }
            catch (Exception e)
            {
                sb.Clear();
                sb.Append("An error has occurred when it try to retrieve employees");
                SeriLogHelper.WriteError(e,sb.ToString());
                return new JsonHttpStatusResult( e.Message,HttpStatusCode.BadRequest);
                
            }
            Debug.Write(JsonConvert.SerializeObject(employeesDTO));
            return Json(employeesDTO, JsonRequestBehavior.DenyGet );
        }
        //GET: Employee/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {

                sb.Clear();
                sb.Append("BadRequest: It needs the Id for request the Employee ");
                SeriLogHelper.WriteError(null, sb.ToString());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeDTO employeeDTO = AutoMapperConfiguration.Instance.Mapper.Map<EmployeeDTO>(_employeeService.GetByID(id));
            if (employeeDTO == null)
            {
                sb.Clear();
                sb.AppendFormat("Not found the employee with id {0}", id);
                SeriLogHelper.WriteWarning(null, sb.ToString());
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
        [HttpPost]
        [ValidateAjax]
        public JsonResult CreateJson(EmployeeDTO employeeDTO)
        {
            if ((int)employeeDTO.Gender == 0)
            {
                sb.Clear();
                sb.Append("Is required a gender to create a employee");
                SeriLogHelper.WriteWarning(null, sb.ToString());
                var data = new ErrorByKey[1] { new ErrorByKey { key = "Gender", errors = new string[1] { "Requerido" } } };
                return new JsonHttpStatusResult(data, HttpStatusCode.BadRequest);
            }
                

            var employee = AutoMapperConfiguration.Instance.Mapper.Map<Employee>(employeeDTO);
            _employeeService.Insert(employee);
            return Json(employee, JsonRequestBehavior.DenyGet);

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                sb.Clear();
                sb.Append("BadRequest: It needs the Id for edit a Employee details");
                SeriLogHelper.WriteError(null, sb.ToString());

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _employeeService.Get(x => x.Id == id);
            if (employee == null)
            {
                sb.Clear();
                sb.AppendFormat("Employee do not found with the id {0}",id);
                SeriLogHelper.WriteWarning(null, sb.ToString());
                return HttpNotFound();
            }
            EmployeeDTO employeeDTO = AutoMapperConfiguration.Instance.Mapper.Map<EmployeeDTO>(employee);
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
            if ((int)employeeDTO.Gender == 0)
            {
                sb.Clear();
                sb.Append("It needs a gender to edit a employee");
                SeriLogHelper.WriteWarning(null, sb.ToString());
                var data = new ErrorByKey[1] { new ErrorByKey { key = "Gender", errors = new string[1] { "Requerido" } } };
                return new JsonHttpStatusResult(data, HttpStatusCode.BadRequest);
            }

            var employee = AutoMapperConfiguration.Instance.Mapper.Map<Employee>(employeeDTO);
            _employeeService.Update(employee);
            return Json(employeeDTO, JsonRequestBehavior.DenyGet);
            
        }

        // GET: Employee/Active/true/5
        [HttpDelete]
        [Route("Employee/Active/{active}/{id}")]
        public ActionResult Delete(bool active, int? id)
        {
            if (id == null)
            {

                sb.Clear();
                sb.Append("It needes a id to delete a employee");
                SeriLogHelper.WriteError(null, sb.ToString());

                return new JsonHttpStatusResult(sb.ToString(), HttpStatusCode.BadRequest);
            }
            try
            {
                var employee = _employeeService.Get(x => x.Id == id && x.Active == !active);

                if (employee == null)
                {
                    sb.Clear();
                    sb.AppendFormat("Employe do not found with the id {0}", id);
                    SeriLogHelper.WriteError(null, sb.ToString());
                    return new JsonHttpStatusResult(sb.ToString(), HttpStatusCode.NotFound);
                }
                employee.Active = active;
                _employeeService.Update(employee);
                EmployeeDTO employeeDTO = AutoMapperConfiguration.Instance.Mapper.Map<EmployeeDTO>(employee);
                
                return Json(employeeDTO, JsonRequestBehavior.DenyGet);

            }
            catch (Exception)
            {
                return new JsonHttpStatusResult(sb.ToString(), HttpStatusCode.InternalServerError);
            }
                    

        }


        
    }
}
