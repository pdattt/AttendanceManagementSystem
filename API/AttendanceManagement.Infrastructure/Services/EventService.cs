using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Common.Dtos.EventDTOs;
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
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepo _eventRepo;
        private readonly IAttendeeRepo _attendeeRepo;

        public EventService(IMapper mapper, IEventRepo repo, IAttendeeRepo attendeeRepo)
        {
            _mapper = mapper;
            _eventRepo = repo;
            _attendeeRepo = attendeeRepo;
        }

        public List<EventReadDTO> GetAll()
        {
            return _mapper.Map<List<EventReadDTO>>(_eventRepo.GetAll());
        }

        public EventReadDTO GetById(int id)
        {
            return _mapper.Map<EventReadDTO>(_eventRepo.GetById(id));
        }

        public bool Add(EventCreateDTO newEvent)
        {
            Event eve = new Event()
            {
                EventName = newEvent.EventName,
                EventDate = newEvent.EventDate,
                Location = newEvent.Location,
                EventStartTime = TimeSpan.Parse(newEvent.EventStartTime),
                EventEndTime = TimeSpan.Parse(newEvent.EventEndTime)
            };

            bool checkAvailableLocation = _eventRepo.CheckAvailableEventLocation(eve);

            if (checkAvailableLocation)
            {
                _eventRepo.Add(eve);
                _eventRepo.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            bool checkDelete = _eventRepo.Delete(id);

            if (checkDelete)
            {
                _eventRepo.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(EventUpdateDTO newEvent, int id)
        {
            Event eve = _eventRepo.GetById(id);

            if (eve == null)
                return false;

            bool checkAvailabe = _eventRepo.CheckAvailableEventLocation(eve);

            if (checkAvailabe)
            {
                eve.EventName = newEvent.EventName;
                eve.EventDate = newEvent.EventDate;
                eve.Location = newEvent.Location;
                eve.EventStartTime = TimeSpan.Parse(newEvent.EventStartTime);
                eve.EventEndTime = TimeSpan.Parse(newEvent.EventEndTime);

                _eventRepo.SaveChanges();
                return true;
            }

            return false;
        }

        public void AddAttendee(int eventId, int attendeeId)
        {
            Event eve = _eventRepo.GetById(eventId);
            Attendee att = _attendeeRepo.GetById(attendeeId);

            if (eve == null || att == null)
                return;

            var checkExisted = eve.Attendees.FirstOrDefault(a => a.ID == att.ID);

            if (checkExisted != null)
                return;

            eve.Attendees.Add(att);
            _eventRepo.SaveChanges();
        }
    }
}