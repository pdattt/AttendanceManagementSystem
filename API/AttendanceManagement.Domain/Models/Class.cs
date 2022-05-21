using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class Class
    {
        public int ClassID { get; set; }

        public string ClassName { get; set; }

        public string? Location { get; set; }

        public DateTime ClassStartTime { get; set; }

        public DateTime ClassEndTime { get; set; }

        public DateTime ClassDateStart { get; set; }

        public DateTime ClassDateEnd { get; set; }

        public string? DaysOfWeek { get; set; }

        public List<Attendee> Attendees { get; set; }
    }
}