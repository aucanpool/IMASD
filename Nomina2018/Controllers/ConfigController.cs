using IMASD.Base.DataTablesDTO;
using IMASD.Base.ENUMS;
using IMASD.Base.Utilities;
using IMASD.DATA.Entities;
using Nomina2018.Mapping;
using Nomina2018.Models;
using Nomina2018.Models.JSON;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nomina2018.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IDepartamentService _departamentService;
        private StringBuilder sb;
        public ConfigController(IPaymentService _paymentService, IDepartamentService _departamentService)
        {
            this._paymentService = _paymentService;
            this._departamentService = _departamentService;
            sb = new StringBuilder();

        }
        // GET: Config
        public ActionResult Payment()
        {
            sb.Clear();
            sb.Append("Retrieve Payments");
            SeriLogHelper.WriteInformation(null, sb.ToString());
            var employees = new List<EmployeeDTO>();
            ViewBag.DepartamentId = new SelectList(AutoMapperConfiguration.Instance.Mapper.Map<IEnumerable<DepartamentDTO>>(_departamentService.GetMany(x => x.Active == true)), "Id", "Name");
            var payments = new List<PaymentDTO>();
            return View(payments);
        }
        [HttpPost]
        public JsonResult CustomServerSideSearchAction(DataTableVM<PaymentSearch> model)
        {
            DataTableOutput<Payment> payments = new DataTableOutput<Payment>();
            DataTableOutput<PaymentDTO> paymentsDTO = new DataTableOutput<PaymentDTO>();
            try
            {
                payments = _paymentService.GetByFilters( model.Filters.Employee ?? null, model.Filters.DepartamentId ??null, model.dataTablesInput ?? null);
                paymentsDTO = AutoMapperConfiguration.Instance.Mapper.Map<DataTableOutput<PaymentDTO>>(payments);
            }
            catch (Exception e)
            {
                sb.Clear();
                sb.Append("An error has occurred when it try to retrieve payments");
                SeriLogHelper.WriteError(e, sb.ToString());
                return new JsonHttpStatusResult(e.Message, HttpStatusCode.BadRequest);

            }
            return Json(paymentsDTO, JsonRequestBehavior.DenyGet);
        }
    }
}