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

        public Event GetByLocation(string location)
        {
            return Query().FirstOrDefaultAsync().Result;
        }
    }
}