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
    public class ClassRepo : Repository<Class>, IClassRepo
    {
        public ClassRepo(AttendanceManagementDBContext context) : base(context)
        {
        }

        public override List<Class> GetAll()
        {
            return Query().Include(cls => cls.Attendees).ToListAsync().Result;
        }

        public override Class GetById(int id)
        {
            return Query().Include(cls => cls.Attendees).FirstOrDefaultAsync(cls => cls.ClassID == id).Result;
        }

        public bool CheckAvailableClassLocation(Class @class)
        {
            Class cls = Query().Where(c => c.ClassID != @class.ClassID
                                      && c.DaysOfWeek.Contains(@class.DaysOfWeek)
                                      && c.Location == @class.Location
                                      && ((c.ClassStartTime >= @class.ClassStartTime && c.ClassStartTime <= @class.ClassEndTime)
                                      || (c.ClassEndTime >= @class.ClassStartTime && c.ClassEndTime <= @class.ClassEndTime)))
                               .FirstOrDefaultAsync().Result;

            if (cls == null)
                return true;

            return false;
        }

        public Class GetByLocation(string location)
        {
            return Query().FirstOrDefaultAsync(cls => cls.Location == location).Result;
        }
    }
}