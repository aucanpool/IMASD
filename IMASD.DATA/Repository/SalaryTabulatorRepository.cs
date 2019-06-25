using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;

namespace IMASD.DATA.Repository
{
    public class SalaryTabulatorRepository : RepositoryBase<SalaryTabulator>, ISalaryTabulatorRepository
    {
        public SalaryTabulatorRepository(MainContext context)
            :base(context)
        {

        }
    }
}
