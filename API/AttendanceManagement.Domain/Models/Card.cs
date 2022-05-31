using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    public class Card
    {
        [Key]
        public string CardId { get; set; }

        public bool Status { get; set; } = true;
    }
}