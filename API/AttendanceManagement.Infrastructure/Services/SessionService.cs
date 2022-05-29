using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepo _sessionRepo;
        private readonly IClassRepo _classRepo;
        private readonly IEventRepo _eventRepo;

        private Dictionary<string, string> dayOfWeek = new Dictionary<string, string>
        {
            { DayOfWeek.Monday.ToString(), "2" },
            { DayOfWeek.Tuesday.ToString(), "3" },
            { DayOfWeek.Wednesday.ToString(), "4" },
            { DayOfWeek.Thursday.ToString(), "5" },
            { DayOfWeek.Friday.ToString(), "6" },
            { DayOfWeek.Saturday.ToString(), "7" },
            { DayOfWeek.Sunday.ToString(), "CN" }
        };

        public SessionService(ISessionRepo sessionRepo, IEventRepo eventRepo, IClassRepo classRepo)
        {
            _sessionRepo = sessionRepo;
            _classRepo = classRepo;
            _eventRepo = eventRepo;
        }

        //public void Add(Session session, string class_event_Id, string semesterId)
        //{
        //    _sessionRepo.Add(session, class_event_Id, semesterId);
        //}

        //public List<Session> GenerateSession(dynamic cls_eve)
        //{
        //    if (cls_eve == null)
        //        return null;

        //    if (cls_eve.DaysOfWeek == null)
        //        return null;

        //    List<Session> sessions = new List<Session>();
        //    var dates = cls_eve.DaysOfWeek.Trim().Split(',').ToList();

        //    for (DateTime date = cls_eve.ClassDateStart; date <= cls_eve.ClassDateEnd; date = date.AddDays(1.0))
        //    {
        //        string day = date.ToString("dddd");

        //        bool checkDay = (dayOfWeek[day.ToString("")]);

        //        if (checkDay)
        //        {
        //            Session addSession = new Session();
        //            addSession.Date = day;
        //            sessions.Add(addSession);
        //        }
        //    }

        //    return sessions;
        //}

        public bool GenerateClassSession(ClassReadDTO cls)
        {
            if (cls == null)
                return false;

            if (cls.DaysOfWeek == null)
                return false;

            List<Session> sessions = new List<Session>();
            var dates = cls.DaysOfWeek.Trim().Split(',').ToList();

            for (DateTime date = cls.ClassDateStart; date <= cls.ClassDateEnd; date = date.AddDays(1.0))
            {
                string day = date.ToString("dddd");

                var checkDay = dates.FirstOrDefault(d => dayOfWeek[day] == d.Trim());

                if (checkDay != null)
                {
                    Session addSession = new Session();
                    addSession.Date = date.ToString("d");
                    sessions.Add(addSession);
                }
            }

            string semesterId = GetSemesterId(cls.ClassDateStart);

            _sessionRepo.Add(sessions, cls.ClassName, "class", semesterId, cls.ClassID, cls.Location, cls.ClassStartTime, cls.ClassEndTime);

            return true;
        }

        public bool GenerateEventSession(EventReadDTO eve)
        {
            if (eve == null)
                return false;

            if (eve.EventDate == null)
                return false;

            List<Session> sessions = new List<Session>();

            Session addSession = new Session();
            addSession.Date = eve.EventDate.ToString("d");
            sessions.Add(addSession);

            string semesterId = GetSemesterId(eve.EventDate);

            _sessionRepo.Add(sessions, eve.EventName, "event", semesterId, eve.EventID, eve.Location, eve.EventStartTime, eve.EventEndTime);

            return true;
        }

        public List<Session> GetAllAttendanceSession(string semesterId, string type, string cls_eve_id)
        {
            return _sessionRepo.GetAllAttendanceSession(semesterId, type, cls_eve_id).Result;
        }

        public List<string> GetAllInSemester(string semesterId, string type)
        {
            if (type != "class" && type != "event")
                return null;

            return _sessionRepo.GetAllInSemester(semesterId, type).Result;
        }

        public List<string> GetAllSemesterIds()
        {
            return _sessionRepo.GetAllSemesterIds().Result;
        }

        private string GetSemesterId(DateTime date)
        {
            int month = date.Month;
            string semester = "";

            switch (month)
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    semester = "1"; break;
                case 2:
                case 3:
                case 4:
                case 5:
                    semester = "2"; break;
                case 12:
                case 1:
                    semester = "3"; break;
                case 6:
                case 7:
                    semester = "4"; break;
            }

            string year = date.Year.ToString();

            return year + semester;
        }
    }
}