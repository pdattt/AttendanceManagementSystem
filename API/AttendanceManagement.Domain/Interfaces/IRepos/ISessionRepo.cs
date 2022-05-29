using AttendanceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IRepos
{
    public interface ISessionRepo
    {
        void Add(List<Session> session, string name, string type, string semesterId, int cls_eve_id, string location, TimeSpan startTime, TimeSpan endTime);

        Task<List<string>> GetAllSemesterIds();

        // Get all classes or events in a semester
        Task<List<string>> GetAllInSemester(string semesterId, string type);

        Task<List<Session>> GetAllAttendanceSession(string semesterId, string type, string cls_eve_id);
    }
}