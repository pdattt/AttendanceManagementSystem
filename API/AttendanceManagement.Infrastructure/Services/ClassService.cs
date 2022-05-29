using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Infrastructure.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepo _classRepo;
        private readonly IAttendeeRepo _attendeeRepo;
        private readonly IMapper _mapper;

        public ClassService(IClassRepo repo, IMapper mapper, IAttendeeRepo attendeeRepo)
        {
            _classRepo = repo;
            _mapper = mapper;
            _attendeeRepo = attendeeRepo;
        }

        public List<ClassReadDTO> GetAll()
        {
            return _mapper.Map<List<ClassReadDTO>>(_classRepo.GetAll());
        }

        public ClassReadDTO GetById(int id)
        {
            return _mapper.Map<ClassReadDTO>(_classRepo.GetById(id));
        }

        public bool Add(ClassCreateDTO newClass)
        {
            Class cls = new Class()
            {
                ClassName = newClass.ClassName,
                DaysOfWeek = newClass.DaysOfWeek,
                Location = newClass.Location,
                ClassStartTime = TimeSpan.Parse(newClass.ClassStartTime),
                ClassEndTime = TimeSpan.Parse(newClass.ClassEndTime),
                ClassDateStart = newClass.ClassDateStart,
                ClassDateEnd = newClass.ClassDateEnd
            };

            bool checkAvailableLocation = _classRepo.CheckAvailableClassLocation(cls);

            if (checkAvailableLocation)
            {
                _classRepo.Add(cls);
                _classRepo.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            bool checkDelete = _classRepo.Delete(id);

            if (checkDelete)
            {
                _classRepo.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(ClassUpdateDTO newClass, int id)
        {
            Class cls = _classRepo.GetById(id);

            if (cls == null)
                return false;

            bool checkAvailableLocation = _classRepo.CheckAvailableClassLocation(cls);

            if (checkAvailableLocation)
            {
                cls.ClassName = newClass.ClassName;
                cls.DaysOfWeek = newClass.DaysOfWeek;
                cls.Location = newClass.Location;
                cls.ClassStartTime = TimeSpan.Parse(newClass.ClassStartTime);
                cls.ClassEndTime = TimeSpan.Parse(newClass.ClassEndTime);
                cls.ClassDateStart = newClass.ClassDateStart;
                cls.ClassDateEnd = newClass.ClassDateEnd;

                _classRepo.SaveChanges();
                return true;
            }

            return false;
        }

        public void AddAttendee(int classId, int attendeeId)
        {
            Class eve = _classRepo.GetById(classId);
            Attendee att = _attendeeRepo.GetById(attendeeId);

            if (eve == null || att == null)
                return;

            var checkExisted = eve.Attendees.FirstOrDefault(a => a.ID == att.ID);

            if (checkExisted != null)
                return;

            eve.Attendees.Add(att);
            _classRepo.SaveChanges();
        }

        public List<AttendeeReadDTO> GetAvailableAttendeesInClass(int id)
        {
            Class cls = _classRepo.GetById(id);

            if (cls == null)
                return null;

            var attendees = _attendeeRepo.GetAll();
            List<Attendee> availableAttendees = new List<Attendee>();

            foreach (var att in attendees)
            {
                if (cls.Attendees.Contains(att))
                    continue;

                availableAttendees.Add(att);
            }

            return _mapper.Map<List<AttendeeReadDTO>>(availableAttendees);
        }

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
    }
}