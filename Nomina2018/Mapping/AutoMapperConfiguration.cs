using AutoMapper;
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
                cfg.CreateMap<Job, JobDTO>();
                cfg.CreateMap<JobDTO, Job>();
                cfg.CreateMap<DepartamentDTO, Departament>();
                cfg.CreateMap<Departament,DepartamentDTO>();
                cfg.CreateMap<SalaryTabulator, SalaryTabulatorDTO>();
                cfg.CreateMap<SalaryTabulatorDTO, SalaryTabulator>();

            });
            mapper = mc.CreateMapper();
        }
    }
}