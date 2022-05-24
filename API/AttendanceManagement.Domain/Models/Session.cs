using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class Session
    {
        public int ClassId { get; set; }
        public int EventId { get; set; }
        public string Location { get; set; }
    }
}