using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.EventDTOs
{
    public class EventCreateDTO
    {
        [Required]
        public string EventName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        [Required]
        public DateTime EventStartTime { get; set; }

        [Required]
        public DateTime EventEndTime { get; set; }
    }
}