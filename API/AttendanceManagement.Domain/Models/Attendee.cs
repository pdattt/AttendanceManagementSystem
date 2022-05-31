using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class Attendee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Card> Cards { get; set; }
        public string? Role { get; set; }
        public List<Class> Classes { get; set; }
        public List<Event> Events { get; set; }
    }
}