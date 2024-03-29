﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMASD.DATA.Repository.Interface;
using IMASD.DATA.Repository;
using IMASD.DATA;
using IMASD.DATA.Entities;
using Autofac;
using Services;
using Services.Interface;
using Autofac.Core;
using IMASD.Base.Utilities;
using System.Diagnostics;
using System.Collections.Generic;

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
                builder.RegisterType<DepartamentRepository>().As<IDepartamentRepository>();
                builder.RegisterType<MainContext>().InstancePerRequest();

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

        [TestMethod]
        public void TestSeriLog()
        {
            try
            {
                SeriLogHelper.WriteDebug(null, "Debug ");
                Debug.WriteLine("Debug");
                SeriLogHelper.WriteWarning(null, "Warning ");
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                SeriLogHelper.WriteError(e, "Error");
                SeriLogHelper.WriteFatal(e, "Fatal");
                SeriLogHelper.WriteVerbose(e, "Verbose");
                throw;
            }

        }


        [TestMethod]
        public void TestInsertPayments() {
            MainContext context = new MainContext();
            PaymentRepository repository = new PaymentRepository(context);
            var payments = GetPayments();
            foreach (var payment in payments)
            {
                repository.Insert(payment);
            }
        }


        private List<Payment> GetPayments()
        {
            DateTime d = new DateTime(2019, 06, 01);
            var payments = new List<Payment>();
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                
                EmployeeId = 1
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate =d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 1
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 1
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(15),
                ProcessedDate = d.AddDays(16),
                EmployeeId = 1
            });
            /// end employee 1
            /// start employee 2
            
            d = new DateTime(2019, 06, 01);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays( 14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 2
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 2
            });
            d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate =d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 2
            });
            d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(15),
                ProcessedDate = d.AddDays(1),
                EmployeeId = 2
            });
            //end employee 2
            /// start employee 3
            d = new DateTime(2019,06,01);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 3
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 3
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 3
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(15),
                ProcessedDate = d.AddDays(16),
                EmployeeId = 3
            });
            //end employee 3

            /// start employee 4
            d = new DateTime(2019,06,01);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 4
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 4
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(14),
                ProcessedDate = d.AddDays(15),
                EmployeeId = 4
            });
            d = d.AddDays(15);
            payments.Add(new Payment
            {
                FrequencyofPayments = Base.ENUMS.FrequencyofPayments.Biweekly,
                StarDate = d,
                EndDate = d.AddDays(15),
                ProcessedDate = d.AddDays(1),
                EmployeeId = 4
            });
            /// end employee 4
            return payments;
        }
    }

    }
