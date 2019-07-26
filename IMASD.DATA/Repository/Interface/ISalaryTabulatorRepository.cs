using IMASD.DATA.Entities;
using System.Collections.Generic;

namespace IMASD.DATA.Repository.Interface
{
    public interface ISalaryTabulatorRepository : IRepository<SalaryTabulator>
    {
        IEnumerable<SalaryTabulator> SortOrderAndPaging(string searchVaue, int take, int skip, string sortBy, bool sortDir, out int recordsFiltered);
    }
}
