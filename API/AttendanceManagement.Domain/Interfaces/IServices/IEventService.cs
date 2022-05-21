using AttendanceManagement.Common.Dtos.EventDTOs;

namespace AttendanceManagement.Domain.Interfaces.IServices
{
    public interface IEventService
    {
        List<EventReadDTO> GetAll();

        EventReadDTO GetById(int id);

        bool Add(EventCreateDTO newEvent);

        bool Update(EventUpdateDTO newEvent);

        bool Delete(int id);
    }
}