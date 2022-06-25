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
        bool GenerateClassSession(ClassReadDTO cls);

        bool GenerateEventSession(EventReadDTO eve);

        List<string> GetAllSemesterIds();

        // Get all classes or events in a semester
        List<string> GetAllInSemester(string semesterId, string type);

        List<Session> GetAllAttendanceSession(string semesterId, string type, string cls_eve_id);

        Task<List<CheckInToReturn_Time>> GetAllCheckInsInSession(string semesterId, string type, string cls_eve_id, string date);

        Task<List<CheckInToReturn_Time>> GetAllUnAssignedCheckInsInEvent(string semesterId, string eventID, string date);

        CheckIn GetCheckInByCardId(string semesterId, string type, string cls_eve_id, string date, string cardId);

        Task<dynamic> CountCheckInsInSemerter(string semesterId, string type, string cls_eve_id);

        Task<dynamic> CountUnassignedCheckInsInEvent(string semesterId, string eventID);

        string GetSemesterId(DateTime date);

        Task<bool> CheckIn(string cardId, string location);
    }
}