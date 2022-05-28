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
        void Add(List<Session> session, string type, string semesterId, int cls_eve_id, string location, TimeSpan startTime, TimeSpan endTime);
    }
}