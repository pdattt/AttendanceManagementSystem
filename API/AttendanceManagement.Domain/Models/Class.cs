using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DataType(DataType.Time)]
        public TimeSpan ClassStartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan ClassEndTime { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ClassDateStart { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ClassDateEnd { get; set; }

        public string? DaysOfWeek { get; set; }

        public List<Attendee> Attendees { get; set; }
    }
}