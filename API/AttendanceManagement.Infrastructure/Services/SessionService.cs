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
        private readonly IAttendeeRepo _attendeeRepo;
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

        public SessionService(ISessionRepo sessionRepo, IEventRepo eventRepo, IClassRepo classRepo, IAttendeeRepo attendeeRepo)
        {
            _sessionRepo = sessionRepo;
            _classRepo = classRepo;
            _eventRepo = eventRepo;
            _attendeeRepo = attendeeRepo;
        }

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

        public List<CheckInToReturn_Time> GetAllCheckInsInSession(string semesterId, string type, string cls_eve_id, string date)
        {
            var checkins = _sessionRepo.GetAllCheckInsInSession(semesterId, type, cls_eve_id, date).Result;
            List<Attendee> attendees = new List<Attendee>();

            if (type == "event")
            {
                var eve = _eventRepo.GetById(Int32.Parse(cls_eve_id));
                attendees = _attendeeRepo.GetAll().Where(att => att.Events.Contains(eve)).ToList();
            }
            else
            {
                var cls = _classRepo.GetById(Int32.Parse(cls_eve_id));
                attendees = _attendeeRepo.GetAll().Where(att => att.Classes.Contains(cls)).ToList();
            }

            List<CheckInToReturn_Time> list = new List<CheckInToReturn_Time>();

            foreach (Attendee attendee in attendees)
            {
                CheckInToReturn_Time objectToReturn = new CheckInToReturn_Time()
                {
                    AttendeeId = attendee.ID,
                    AttendeeName = attendee.Name,
                    CardId = attendee.CardId,
                    Role = attendee.Role,
                    Time = "None"
                };

                var check = checkins.FirstOrDefault(c => c.CardId == objectToReturn.CardId);

                if (check != null)
                {
                    objectToReturn.Time = check.Time;
                };

                list.Add(objectToReturn);
            }

            return list;
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

        public CheckIn GetCheckInByCardId(string semesterId, string type, string cls_eve_id, string date, string cardId)
        {
            var checkins = _sessionRepo.GetAllCheckInsInSession(semesterId, type, cls_eve_id, date).Result;

            var checkin = checkins.FirstOrDefault(c => c.CardId == cardId);

            if (checkin == null)
                return null;

            checkin.AttendeeName = _attendeeRepo.GetByCardId(cardId).Name;

            return checkin;
        }

        public dynamic CountCheckInsInSemerter(string semesterId, string type, string cls_eve_id)
        {
            var sessions = _sessionRepo.GetAllAttendanceSession(semesterId, type, cls_eve_id).Result;
            List<CheckInToReturn_Report> list = new List<CheckInToReturn_Report>();

            List<Attendee> attendees = new List<Attendee>();

            if (type == "event")
            {
                var eve = _eventRepo.GetById(Int32.Parse(cls_eve_id));
                attendees = _attendeeRepo.GetAll().Where(att => att.Events.Contains(eve)).ToList();
            }
            else
            {
                var cls = _classRepo.GetById(Int32.Parse(cls_eve_id));
                attendees = _attendeeRepo.GetAll().Where(att => att.Classes.Contains(cls)).ToList();
            }

            foreach (var session in sessions)
            {
                var date = session.Date;
                var checkins = _sessionRepo.GetAllCheckInsInSession(semesterId, type, cls_eve_id, date).Result;

                foreach (Attendee attendee in attendees)
                {
                    CheckInToReturn_Report objectToReturn = new CheckInToReturn_Report()
                    {
                        AttendeeId = attendee.ID,
                        AttendeeName = attendee.Name,
                        CardId = attendee.CardId,
                        Role = attendee.Role
                    };

                    var check = checkins.FirstOrDefault(c => c.CardId == objectToReturn.CardId);

                    if (check != null)
                    {
                        objectToReturn.Count++;
                    };

                    list.Add(objectToReturn);
                }
            }

            var q = list.GroupBy(x => x.CardId)
                        .Select(x => new CheckInToReturn_Report
                        {
                            AttendeeId = x.First().AttendeeId,
                            AttendeeName = x.First().AttendeeName,
                            Count = x.Count(a => a.Count != 0),
                            CardId = x.First().CardId,
                            Role = x.First().Role
                        })
                        .OrderByDescending(x => x.Count);

            return q;
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

            return year.Substring(year.Length - 2) + "3" + semester;
        }

        public async Task<bool> CheckIn(string cardId, string location)
        {
            DateTime getDate = DateTime.Now;
            string semesterId = GetSemesterId(getDate);
            List<string> types = _sessionRepo.GetAllTypes(semesterId).Result;

            if (types.Count < 1 || types == null)
                return false;

            foreach (var type in types)
            {
                var checkIn = await _sessionRepo.CheckIn(semesterId, type, getDate, cardId, location);
                if (checkIn)
                    return true;
            }

            return false;
        }
    }
}