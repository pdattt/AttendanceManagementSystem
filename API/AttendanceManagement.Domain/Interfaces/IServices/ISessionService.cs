using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IServices
{
    public interface ISessionService
    {
        //List<Session> GenerateSession(dynamic cls_eve);

        bool GenerateClassSession(ClassReadDTO cls);

        bool GenerateEventSession(EventReadDTO eve);

        List<string> GetAllSemesterIds();

        // Get all classes or events in a semester
        List<string> GetAllInSemester(string semesterId, string type);

        List<Session> GetAllAttendanceSession(string semesterId, string type, string cls_eve_id);

        List<CheckInToReturn_Time> GetAllCheckInsInSession(string semesterId, string type, string cls_eve_id, string date);

        CheckIn GetCheckInByCardId(string semesterId, string type, string cls_eve_id, string date, string cardId);

        dynamic CountCheckInsInSemerter(string semesterId, string type, string cls_eve_id);
    }
}