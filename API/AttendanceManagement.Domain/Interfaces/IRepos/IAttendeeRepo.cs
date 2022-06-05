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

        Attendee GetByEmail(string email);

        void Add(Attendee newAttendee);

        void Update(Attendee newAttendee);

        bool Delete(int id);

        bool CheckExistedEmail(string email);

        bool CheckExistedEmail(string email, int id);

        Attendee GetByCardId(string cardId);
    }
}