using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
    }
}