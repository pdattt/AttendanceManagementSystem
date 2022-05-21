using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.EventDTOs
{
    public class EventUpdateDTO
    {
        [Required]
        public string EventName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/mm/yyyy")]
        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "HH:mm:ss")]
        public DateTime EventStartTime { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "HH:mm:ss")]
        public DateTime EventEndTime { get; set; }
    }
}