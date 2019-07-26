using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMASD.DATA.Entities;
using System.Linq.Expressions;
using IMASD.DATA.Repository.Interface;
using IMASD.Base.DataTablesDTO;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository repository;
        public PaymentService(IPaymentRepository repository)
        {
            this.repository = repository;
        }
        public void Delete(object id)
        {
            //we can´t delete a payment
            var payment= GetByID(id);
            payment.VoidDate = DateTime.Now;
            repository.Update(payment);
        }

        public void Delete(Payment entity)
        {
            entity.VoidDate = DateTime.Now;
            repository.Update(entity);
        }

        public Payment Get(Expression<Func<Payment, bool>> where)
        {
            return repository.Get(where);
        }

        public IEnumerable<Payment> GetAll()
        {
            return repository.GetAll();
        }

        public DataTableOutput<Payment> GetByFilters(string employee, int? departamentId, DataTableInput input)
        {
            var output = new DataTableOutput<Payment>();
            var query = repository.GetPredicateByFilters( employee, departamentId);
            string sortBy = "";
            bool sortDir = true;

            if (input.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = input.columns[input.order[0].column].data;
                sortDir = input.order[0].dir.ToLower() == "asc";
            }
            IEnumerable<Payment> result = repository.SortOrderAndPaging(query, input.length, input.start, sortBy, sortDir);

            if (result == null)
            {
                output.data = new List<Payment>();
            }
            output.data = result.ToList();
            output.recordsFiltered = repository.Count(query);
            output.recordsTotal = repository.Count(x => 1 == 1);
            return output;
        }

        public Payment GetByID(object id)
        {
            return repository.GetByID(id);
        }

        public IEnumerable<Payment> GetMany(Expression<Func<Payment, bool>> where)
        {
            return repository.GetMany(where);
        }

        public void Insert(Payment entity)
        {
            repository.Insert(entity);
        }

        public void Update(Payment entity)
        {
            repository.Update(entity);
        }
    }
}
