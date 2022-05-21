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

        public bool AvailableClassLocation(Class newClass)
        {
            Class cls = Query().Where(c => c.DaysOfWeek.Contains(newClass.DaysOfWeek)
                          && c.Location == newClass.Location
                          && ((c.ClassStartTime >= newClass.ClassStartTime && c.ClassStartTime <= newClass.ClassEndTime)
                          || (c.ClassEndTime >= newClass.ClassStartTime && c.ClassEndTime <= newClass.ClassEndTime)))
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