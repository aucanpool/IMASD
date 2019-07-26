using Autofac;
using Autofac.Integration.Mvc;
using IMASD.DATA;
using IMASD.DATA.Repository;
using IMASD.DATA.Repository.Interface;
using Services;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nomina2018
{
    public class AutoFacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();


            // Register application dependencies.
            builder.RegisterType<JobRepository>().As<IJobRepository>();
            builder.RegisterType<SalaryTabulatorRepository>().As<ISalaryTabulatorRepository>();
            builder.RegisterType<DepartamentRepository>().As<IDepartamentRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
            builder.RegisterType<MainContext>().InstancePerRequest();

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<JobService>().As<IJobService>();
            builder.RegisterType<SalaryTabulatorService>().As<ISalaryTabulatorService>();
            builder.RegisterType<DepartamentService>().As<IDepartamentService>();
            builder.RegisterType<PaymentService>().As<IPaymentService>();


            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}