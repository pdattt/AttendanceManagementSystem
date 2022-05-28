using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.SessionDTOs
{
    public class SessionCreateDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Location { get; set; }
    }
}