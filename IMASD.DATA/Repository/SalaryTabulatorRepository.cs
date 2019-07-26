using IMASD.Base.ENUMS;
using IMASD.Base.Utilities;
using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace IMASD.DATA.Repository
{
    public class SalaryTabulatorRepository : RepositoryBase<SalaryTabulator>, ISalaryTabulatorRepository
    {
        public SalaryTabulatorRepository(MainContext context)
            :base(context)
        {

        }
        private Expression<Func<SalaryTabulator,bool>> BuidlDynamicWhereClause(string searchValue)
        {
            var predicate = PredicateBuilder.True<SalaryTabulator>();
            if (String.IsNullOrWhiteSpace(searchValue)==false)
            {
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
                predicate = predicate.And(s => searchTerms.Any(srch=> s.Key.Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.Job.Name.Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.Hourlywages.ToString().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.AnnualHolidayBonus.ToString().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.AnnualBonusDays.ToString().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.AnnualVacationDays.ToString().Contains(srch)));
            }
            return predicate;
        }
        public IEnumerable<SalaryTabulator> SortOrderAndPaging(string searchVaue, int take, int skip, string sortBy, bool sortDir, out int recordsFiltered)
        {
            var where = BuidlDynamicWhereClause(searchVaue);
            var clause = DataContext.SalaryTabulators.Include("Job").AsQueryable().Where(where);
            recordsFiltered = Count(where);
            DataContext.Database.Log = s => Debug.WriteLine(s);
            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Id";
                sortDir = true;
            }
            string sort = sortDir ? "ascending" : "descending";
            IEnumerable<SalaryTabulator> result = clause
                .OrderBy(sortBy + " " + sort)
                .Skip(skip)
                .Take(take)
                .ToList<SalaryTabulator>();

            DataContext.Database.Log = s => Debug.WriteLine(s);
            return result;
        }
    }
}
