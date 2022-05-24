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
        List<Session> GetSessionsBySemesterId(int id);

        List<Session> GetSessionsByEventClassId(string id);

        List<Session> GetSessionsByEventClassId(int semesterId, string eve_class_Id);
    }
}