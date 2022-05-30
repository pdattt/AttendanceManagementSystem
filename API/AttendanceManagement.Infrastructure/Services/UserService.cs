using AttendanceManagement.Common.Dtos.UserDTOs;
using AttendanceManagement.Common.Services;
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
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public UserReadDTO Authentication(UserLoginDTO userLogin)
        {
            string passwordHash1 = MD5Hashing.MD5Hash(userLogin.Password);
            string passwordHash2 = MD5Hashing.MD5Hash(passwordHash1);

            User user = _repo.GetUser(userLogin.Username, passwordHash2);

            if (user == null)
                return null;

            return _mapper.Map<UserReadDTO>(user);
        }
    }
}