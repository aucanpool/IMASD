using Autofac;
using Autofac.Integration.Web;
using IMASD.DATA.Repository;
using IMASD.DATA.Repository.Interface;
using Nomina2018.App_Start;
using Services;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Nomina2018
{
    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {
        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();
            /*builder.RegisterType<DepartamentRepository>().As<IDepartamentRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<JobRepository>().As<IJobRepository>();
            builder.RegisterType<SalaryTabulatorRepository>().As<ISalaryTabulatorRepository>();
            */
            builder.RegisterType<DepartamentService>().As<IDepartamentService>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<JobService>().As<IJobService>();
            builder.RegisterType<SalaryTabulatorService>().As<ISalaryTabulatorService>();

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}