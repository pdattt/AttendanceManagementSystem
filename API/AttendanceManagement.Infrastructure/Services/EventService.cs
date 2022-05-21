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
        private readonly IEventRepo _repo;

        public EventService(IMapper mapper, IEventRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public List<EventReadDTO> GetAll()
        {
            return _mapper.Map<List<EventReadDTO>>(_repo.GetAll());
        }

        public EventReadDTO GetById(int id)
        {
            return _mapper.Map<EventReadDTO>(_repo.GetById(id));
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

            bool checkAvailabe = _repo.AvailableEventLocation(eve);

            if (checkAvailabe)
            {
                _repo.Add(eve);
                _repo.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            bool checkDelete = _repo.Delete(id);

            if (checkDelete)
            {
                _repo.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(EventUpdateDTO newEvent)
        {
            throw new NotImplementedException();
        }
    }
}