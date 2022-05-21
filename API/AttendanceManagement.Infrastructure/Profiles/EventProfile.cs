using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventReadDTO>();
            CreateMap<EventCreateDTO, Event>();
            CreateMap<EventUpdateDTO, Event>();
        }
    }
}