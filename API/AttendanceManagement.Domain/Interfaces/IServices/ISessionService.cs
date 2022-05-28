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
    }
}