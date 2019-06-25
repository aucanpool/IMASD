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
    public class JobService : ServiceBase, IJobService
    {
        private readonly IJobRepository repository;
        public JobService():base()
        {
            this.repository = new JobRepository(context);
        }
        public JobService(IJobRepository repository)
        {
            this.repository = repository;
        }
        public Job Get(Expression<Func<Job, bool>> where)
        {
            return this.repository.Get(where);
        }

        public IEnumerable<Job> GetAll()
        {
            return this.repository.GetAll();
        }

        public Job GetByID(object id)
        {
            return this.repository.GetByID(id);
        }

        public IEnumerable<Job> GetMany(Expression<Func<Job, bool>> where)
        {
            return this.repository.GetMany(where);
        }

        public void Insert(Job entity)
        {
            this.repository.Insert(entity);
        }

        public void Update(Job entity)
        {
            this.repository.Update(entity);
        }
    }
}
