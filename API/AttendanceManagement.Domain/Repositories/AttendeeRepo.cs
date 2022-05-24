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
    public class AttendeeRepo : Repository<Attendee>, IAttendeeRepo
    {
        public AttendeeRepo(AttendanceManagementDBContext context) : base(context)
        {
        }

        public override List<Attendee> GetAll()
        {
            return Query().Include(att => att.Events).Include(att => att.Classes).ToListAsync().Result;
        }

        public override Attendee GetById(int id)
        {
            return Query().Include(att => att.Events).Include(att => att.Classes).FirstOrDefaultAsync(att => att.ID == id).Result;
        }

        public bool CheckExistedEmail(string email)
        {
            Attendee attendee = Query().FirstOrDefaultAsync(att => att.Email == email).Result;

            if (attendee == null)
                return false;

            return true;
        }

        public Attendee GetByEmail(string email)
        {
            return Query().FirstOrDefaultAsync(att => att.Email == email).Result;
        }
    }
}