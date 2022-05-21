using AttendanceManagement.Domain.Interfaces.IRepos;
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

        public bool AvailableEventLocation(Event newEvent)
        {
            Event eve = Query().Where(e => e.EventDate == newEvent.EventDate
                                      && e.Location == newEvent.Location
                                      && ((e.EventStartTime >= newEvent.EventStartTime && e.EventStartTime <= newEvent.EventEndTime)
                                      || (e.EventEndTime >= newEvent.EventStartTime && e.EventEndTime <= newEvent.EventEndTime)))
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