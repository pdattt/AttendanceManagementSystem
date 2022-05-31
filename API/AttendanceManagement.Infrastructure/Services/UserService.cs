using AttendanceManagement.Common;
using AttendanceManagement.Common.Dtos.UserDTOs;
using AttendanceManagement.Common.Services;
using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Models;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public string GenerateToken(UserReadDTO userReadDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.key));
            var credentical = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(type: "Usernam", userReadDTO.Username),
                new Claim(type: "Fullname", userReadDTO.Fullname),
                new Claim(type: "Role", userReadDTO.Role)
            };

            var token = new JwtSecurityToken(JWT.issuer,
                JWT.audience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentical);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserReadDTO DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            UserReadDTO user = new UserReadDTO()
            {
                Username = jwtSecurityToken.Claims.FirstOrDefault(data => data.Type == "Usernam").Value,
                Fullname = jwtSecurityToken.Claims.FirstOrDefault(data => data.Type == "Fullname").Value,
                Role = jwtSecurityToken.Claims.FirstOrDefault(data => data.Type == "Role").Value
            };

            return user;
        }
    }
}