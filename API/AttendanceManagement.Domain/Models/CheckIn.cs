using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class CheckIn
    {
        public string CardId { get; set; }
        public TimeOnly CheckInTime { get; set; }
    }
}