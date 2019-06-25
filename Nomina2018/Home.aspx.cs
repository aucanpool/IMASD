using Autofac.Integration.Web.Forms;
using IMASD.DATA.Entities;
using Nomina2018.Mapping;
using Nomina2018.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina2018
{
    public partial class Home : System.Web.UI.Page
    {
        public IEmployeeService employeeService { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            var employees=AutoMapperConfiguration.Instance.Mapper.Map<List<EmployeeDTO>>(employeeService.GetAll().ToList());
             
            employeesGridView.DataSource = employees;
            employeesGridView.DataBind();
            
        }
        protected void Insert(object sender, EventArgs e)
        {
            string jobNumber = "";
            
            /*string name = txtName.Text;
            string key = txtKey.Text;
            txtName.Text = "";
            txtKey.Text = "";

            Employee employee = new Employee() { };
            employeeService.Insert(employee);*/
            this.BindGrid();
        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
                Response.Redirect("index.aspx?id=" + Convert.ToString(e.CommandArgument));
            else if (e.CommandName == "delete")
            {
                Employee emp= employeeService.GetByID(Convert.ToInt32(e.CommandArgument));
                emp.Active = false;
                employeeService.Update(emp);
                this.BindGrid();
            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            employeesGridView.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
    }
}