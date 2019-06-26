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

namespace Services
{
    public class SalaryTabulatorService : ISalaryTabulatorService
    {
        private readonly ISalaryTabulatorRepository repository;
        
        public SalaryTabulatorService(ISalaryTabulatorRepository repository)
        {
            this.repository = repository;
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
    }
}
