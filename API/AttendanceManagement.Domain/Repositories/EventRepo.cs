﻿using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Repositories
{
    public class EventRepo : Repository<Event>, IEventRepo
    {
        public EventRepo(AttendanceManagementDBContext context) : base(context)
        {
        }

        public bool AvailableEventLocation(Event @event)
        {
            Event eve = Query().Where(e => e.EventID != @event.EventID
                                      && e.EventDate == @event.EventDate
                                      && e.Location == @event.Location
                                      && ((e.EventStartTime >= @event.EventStartTime && e.EventStartTime <= @event.EventEndTime)
                                      || (e.EventEndTime >= @event.EventStartTime && e.EventEndTime <= @event.EventEndTime)))
                               .FirstOrDefaultAsync().Result;

            if (eve == null)
                return true;

            return false;
        }

        public Event GetByLocation(string location)
        {
            return Query().FirstOrDefaultAsync(eve => eve.Location == location).Result;
        }
    }
}