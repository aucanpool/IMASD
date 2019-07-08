using AutoMapper;
using AutoMapper.Execution;
using IMASD.DATA.Entities;
using Nomina2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina2018.Mapping
{
    public sealed class AutoMapperConfiguration
    {
        private readonly static AutoMapperConfiguration _instance = new AutoMapperConfiguration();
        private MapperConfiguration mc;

        private IMapper mapper;

        public IMapper Mapper
        {
            get { return mapper; }
            set { mapper = value; }
        }

        private AutoMapperConfiguration()
        {
            InitializeAutoMapper();
        }
        public static AutoMapperConfiguration Instance
        {
            get
            {
                return _instance;
            }
        }
        private void InitializeAutoMapper() {
            mc= new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<EmployeeDTO, Employee>();
                cfg.CreateMap<Job, JobDTO>()
                .ForMember(x => x.SalaryTabulators, s => s.Ignore());
                cfg.CreateMap<JobDTO, Job>();
                cfg.CreateMap<DepartamentDTO, Departament>();
                cfg.CreateMap<Departament, DepartamentDTO>()
                .ForMember(x => x.Employees, s => s.Ignore());
                
                cfg.CreateMap<SalaryTabulator, SalaryTabulatorDTO>()
                .ForMember(x => x.Employees, s => s.Ignore());

                cfg.CreateMap<SalaryTabulatorDTO, SalaryTabulator>();

            });
            mapper = mc.CreateMapper();
        }
    }
}