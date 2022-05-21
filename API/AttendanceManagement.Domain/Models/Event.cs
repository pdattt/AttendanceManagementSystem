using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime EventDate { get; set; }

        public string? Location { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EventStartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EventEndTime { get; set; }

        public List<Attendee> Attendees { get; set; }
    }
}