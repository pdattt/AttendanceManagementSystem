using AttendanceManagement.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IServices
{
    public interface IAttendeeService
    {
        List<AttendeeReadDTO> GetAll();
        AttendeeReadDTO GetById(int id);
        void Add(AttendeeCreateDTO newAttendee);
        bool Update(AttendeeUpdateDTO newAttendee);

        bool Delete(int id);
    }
}
