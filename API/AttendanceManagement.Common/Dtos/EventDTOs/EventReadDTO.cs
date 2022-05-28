using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.EventDTOs
{
    public class EventReadDTO
    {
        public int EventID { get; set; }
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        public TimeSpan EventStartTime { get; set; }

        public TimeSpan EventEndTime { get; set; }
        public List<AttendeeReadDTO> Attendees { get; set; }
    }
}