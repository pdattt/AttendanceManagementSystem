using AttendanceManagement.Common.Dtos.CardDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.AttendeeDTOs
{
    public class AttendeeReadDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CardReadDTO> Cards { get; set; }
        public string Role { get; set; }
    }
}