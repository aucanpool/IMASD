using IMASD.DATA;

namespace Services
{
    public class ServiceBase
    {
        internal MainContext context;
        public ServiceBase()
        {
            this.context = new MainContext();
        }

    }
}
