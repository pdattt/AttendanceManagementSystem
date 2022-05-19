using AttendanceManagement.Common.Dtos;
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
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepo _repo;
        private readonly IMapper _mapper;

        public AttendeeService(IAttendeeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public List<AttendeeReadDTO> GetAll()
        {
            return _mapper.Map<List<AttendeeReadDTO>>(_repo.GetAll());
        }

        public AttendeeReadDTO GetById(int id)
        {
            return _mapper.Map<AttendeeReadDTO>(_repo.GetById(id));
        }
        public void Add(AttendeeCreateDTO newAttendee)
        {
            Attendee attendee = _mapper.Map<Attendee>(newAttendee);
            _repo.Add(attendee);
            _repo.SaveChanges();
        }

        public bool Update(AttendeeUpdateDTO newAttendee)
        {
            Attendee attendee = _repo.GetById(newAttendee.ID);

            if(attendee != null)
            {
                attendee.Name = newAttendee.Name;
                attendee.Email = newAttendee.Email;
                attendee.Role = newAttendee.Role;
                _repo.SaveChanges();
                return true;
            }

            return false;
        }
        public bool Delete(int id)
        {
            Attendee attendee = _repo.GetById(id);

            if (attendee != null)
            {
                _repo.Delete(id);
                return true;
            }

            return false;
        }
    }
}
