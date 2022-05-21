using AttendanceManagement.Common.Dtos.AttendeeDTOs;

namespace AttendanceManagement.Domain.Interfaces.IServices
{
    public interface IEventService
    {
        List<AttendeeReadDTO> GetAll();

        AttendeeReadDTO GetById(int id);

        bool Add(AttendeeCreateDTO newAttendee);

        bool Update(AttendeeUpdateDTO newAttendee);

        bool Delete(int id);
    }
}