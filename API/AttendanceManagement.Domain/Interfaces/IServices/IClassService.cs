using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Common.Dtos.ClassDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IServices
{
    public interface IClassService
    {
        List<ClassReadDTO> GetAll();

        ClassReadDTO GetById(int id);

        bool Add(ClassCreateDTO newClass);

        bool Update(ClassUpdateDTO newClass, int id);

        bool Delete(int id);

        void AddAttendee(int classId, int id);

        List<AttendeeReadDTO> GetAvailableAttendeesInClass(int id);

        void RemoveAttendee(int classId, int attendeeId);

        string GetDaysOfWeek(string days);
    }
}