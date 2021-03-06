using AttendanceManagement.Common.Dtos;
using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Models;
using AutoMapper;

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

        public bool Add(AttendeeCreateDTO newAttendee)
        {
            if (_repo.CheckExistedEmail(newAttendee.Email))
                return false;

            Attendee attendee = _mapper.Map<Attendee>(newAttendee);
            _repo.Add(attendee);
            _repo.SaveChanges();
            return true;
        }

        public bool Update(AttendeeUpdateDTO newAttendee, int id)
        {
            Attendee attendee = _repo.GetById(id);

            if (attendee != null)
            {
                if (_repo.CheckExistedEmail(newAttendee.Email))
                    return false;

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
            bool checkDelete = _repo.Delete(id);

            if (checkDelete)
            {
                _repo.SaveChanges();
                return true;
            }

            return false;
        }
    }
}