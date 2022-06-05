﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class CheckInToReturn_Report
    {
        public int AttendeeId { get; set; }
        public string AttendeeName { get; set; }
        public string CardId { get; set; }
        public string Role { get; set; }
        public int Count { get; set; } = 0;
    }
}