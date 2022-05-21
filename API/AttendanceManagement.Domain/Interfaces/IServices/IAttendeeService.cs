using AttendanceManagement.Common.Dtos;
using AttendanceManagement.Common.Dtos.AttendeeDTOs;
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

        bool Add(AttendeeCreateDTO newAttendee);

        bool Update(AttendeeUpdateDTO newAttendee, int id);

        bool Delete(int id);
    }
}