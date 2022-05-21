using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.ClassDTOs
{
    public class ClassReadDTO
    {
        public int ClassID { get; set; }

        public string ClassName { get; set; }

        public string Location { get; set; }

        public TimeSpan ClassStartTime { get; set; }

        public TimeSpan ClassEndTime { get; set; }

        public DateTime ClassDateStart { get; set; }

        public DateTime ClassDateEnd { get; set; }

        public string DaysOfWeek { get; set; }
    }
}