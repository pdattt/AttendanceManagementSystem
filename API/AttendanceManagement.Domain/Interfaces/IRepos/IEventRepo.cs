using AttendanceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IRepos
{
    public interface IEventRepo
    {
        void SaveChanges();

        List<Event> GetAll();

        Event GetById(int id);

        Event GetByLocation(string location);

        void Add(Event newAttendee);

        void Update(Event newAttendee);

        bool Delete(int id);

        bool CheckAvailableEventLocation(Event eve);
    }
}