using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMASD.DATA.Repository.Interface;
using IMASD.DATA.Repository;
using IMASD.DATA;
using IMASD.DATA.Entities;
using Autofac;
using Services;
using Services.Interface;
using Autofac.Core;

namespace IMASD.Test
{
    [TestClass]
    public class UnitTest1
    {
        private static IContainer Container { get; set; }
        [TestMethod]
        public void TestServiceWithAutoFac()
        {
            try
            {
                // Create your builder.
                var builder = new ContainerBuilder();
                
                
                builder.RegisterType<DepartamentService>().As<IDepartamentService>();

                Container = builder.Build();

                using (var scope = Container.BeginLifetimeScope())
                {
                    var writer = scope.Resolve<IDepartamentService>();
                    var dep = new Departament() { Name = "TI", Description = "Tecnologias de la comunicación" };
                    var oldDep= writer.Get(x => x.Name.Equals("TI"));
                    if (oldDep==null)
                    {

                        writer.Insert(dep);
                        System.Console.Write("AAUP: " + dep.Id);
                    }
                    else
                        System.Console.Write("AAUP siii: " + oldDep.Name);
                }

            }
            catch (Exception e)
            {
                System.Console.Write("AAUP: " + e.Message);

            }
            
        }


        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                MainContext mc = new MainContext();
                IJobRepository ijr = new JobRepository(mc);
                Job j= ijr.Get(x=>x.Key.Equals("056781"));
                if (j == null)
                {
                    Job newjob = new Job() { Key = "056781", Name = "Desarrollador Sr" };
                    ijr.Insert(newjob);
                    System.Console.Write("si se pudo perro");
                }
                else {
                    System.Console.Write("Si se consulto perro jajajjajaj");
                }
                
                
            }
            catch (Exception e)
            {
                System.Console.Write(e.StackTrace.ToString());
            }
            
        }
    }
    
}
