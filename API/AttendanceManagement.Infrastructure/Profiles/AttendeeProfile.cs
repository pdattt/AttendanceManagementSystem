using AttendanceManagement.Common.Dtos;
using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Profiles
{
    public class AttendeeProfile : Profile
    {
        public AttendeeProfile()
        {
            CreateMap<Attendee, AttendeeReadDTO>();
            CreateMap<AttendeeCreateDTO, Attendee>();
            CreateMap<AttendeeUpdateDTO, Attendee>();
        }
    }
}