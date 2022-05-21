using AttendanceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IRepos
{
    public interface IClassRepo
    {
        void SaveChanges();

        List<Class> GetAll();

        Class GetById(int id);

        Class GetByLocation(string location);

        void Add(Class newAttendee);

        void Update(Class newAttendee);

        bool Delete(int id);

        bool AvailableClassLocation(Class cls);
    }
}