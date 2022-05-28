using AttendanceManagement.Common.Dtos.SessionDTOs;
using AttendanceManagement.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<SessionCreateDTO, Session>();
        }
    }
}