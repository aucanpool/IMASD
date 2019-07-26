using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMASD.DATA.Entities;
using System.Linq.Expressions;
using IMASD.DATA.Repository.Interface;
using IMASD.DATA.Repository;
using IMASD.Base.DataTablesDTO;

namespace Services
{
    public class SalaryTabulatorService : ISalaryTabulatorService
    {
        private readonly ISalaryTabulatorRepository repository;
        
        public SalaryTabulatorService(ISalaryTabulatorRepository repository)
        {
            this.repository = repository;
        }

        public void Delete(object id)
        {
            var entity = GetByID(id);
            entity.Active = false;
            this.repository.Update(entity);
        }

        public void Delete(SalaryTabulator entity)
        {
            entity.Active = false;
            this.repository.Update(entity);
        }

        public SalaryTabulator Get(Expression<Func<SalaryTabulator, bool>> where)
        {
            return this.repository.Get(where);
        }

        public IEnumerable<SalaryTabulator> GetAll()
        {
            return this.repository.GetAll();
        }

        public SalaryTabulator GetByID(object id)
        {
            return this.repository.GetByID(id);
        }

        public IEnumerable<SalaryTabulator> GetMany(Expression<Func<SalaryTabulator, bool>> where)
        {
            return this.repository.GetMany(where);
        }

        public void Insert(SalaryTabulator entity)
        {
            this.repository.Insert(entity);
        }

        public void Update(SalaryTabulator entity)
        {
            this.repository.Update(entity);
        }
        public DataTableOutput<SalaryTabulator> GetSalarysTabulators(DataTableInput input) {
            var output = new DataTableOutput<SalaryTabulator>();
            var searchBy = (input.search != null) ? input.search.value : null;
            var take = input.length;
            var skip = input.start;
            var recordsFiltered = 0;
            string sortBy = "";
            bool sortDir = true;

            if (input.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = input.columns[input.order[0].column].data;
                sortDir = input.order[0].dir.ToLower() == "asc";
            }
            IEnumerable<SalaryTabulator> result = repository.SortOrderAndPaging(searchBy,take,skip,sortBy,sortDir,out recordsFiltered);
            if (result==null)
            {

                output.data = new List<SalaryTabulator>();
            }
            output.data = result.ToList();
            output.recordsFiltered = recordsFiltered ;
            output.recordsTotal = repository.Count(x=>1==1);
            return output;
        }
    }
}
