using AttendanceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IRepos
{
    public interface IAttendeeRepo
    {
        void SaveChanges();
        List<Attendee> GetAll();
        Attendee GetById(int id);
        void Add(Attendee newAttendee);
        void Update(Attendee newAttendee);
        void Delete(int id);
    }
}
