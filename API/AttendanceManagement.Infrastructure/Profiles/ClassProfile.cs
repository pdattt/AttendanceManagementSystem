using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassReadDTO>();
            CreateMap<ClassUpdateDTO, Class>();
            CreateMap<ClassCreateDTO, Class>();
        }
    }
}