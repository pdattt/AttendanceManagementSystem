using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.UserDTOs
{
    public class UserReadDTO
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
    }
}