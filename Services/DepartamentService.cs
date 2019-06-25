using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;
using IMASD.DATA.Repository;

namespace Services
{
    public class DepartamentService : ServiceBase, IDepartamentService
    {
        private readonly IDepartamentRepository _repository;

        public DepartamentService():base()
        {
            this._repository = new DepartamentRepository(context);
        }
        public DepartamentService(IDepartamentRepository _repository)
        {
            this._repository = _repository;
        }

        public Departament Get(Expression<Func<Departament, bool>> where)
        {
            return _repository.Get(where);
        }

        public IEnumerable<Departament> GetAll()
        {
            return _repository.GetAll();
        }

        public Departament GetByID(object id)
        {
            return _repository.GetByID(id);
        }

        public IEnumerable<Departament> GetMany(Expression<Func<Departament, bool>> where)
        {
            return this._repository.GetMany(where);
        }

        public void Insert(Departament entity)
        {
            this._repository.Insert(entity);
        }

        public void Update(Departament entity)
        {
            this._repository.Update(entity);
        }
    }
}
