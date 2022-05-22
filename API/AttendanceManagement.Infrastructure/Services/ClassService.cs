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
        private readonly IClassRepo _repo;
        private readonly IMapper _mapper;

        public ClassService(IClassRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public List<ClassReadDTO> GetAll()
        {
            return _mapper.Map<List<ClassReadDTO>>(_repo.GetAll());
        }

        public ClassReadDTO GetById(int id)
        {
            return _mapper.Map<ClassReadDTO>(_repo.GetById(id));
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

            bool checkAvailableLocation = _repo.CheckAvailableClassLocation(cls);

            if (checkAvailableLocation)
            {
                _repo.Add(cls);
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

        public bool Update(ClassUpdateDTO newClass, int id)
        {
            Class cls = _repo.GetById(id);

            if (cls == null)
                return false;

            bool checkAvailableLocation = _repo.CheckAvailableClassLocation(cls);

            if (checkAvailableLocation)
            {
                cls.ClassName = newClass.ClassName;
                cls.DaysOfWeek = newClass.DaysOfWeek;
                cls.Location = newClass.Location;
                cls.ClassStartTime = TimeSpan.Parse(newClass.ClassStartTime);
                cls.ClassEndTime = TimeSpan.Parse(newClass.ClassEndTime);
                cls.ClassDateStart = newClass.ClassDateStart;
                cls.ClassDateEnd = newClass.ClassDateEnd;

                _repo.SaveChanges();
                return true;
            }

            return false;
        }
    }
}