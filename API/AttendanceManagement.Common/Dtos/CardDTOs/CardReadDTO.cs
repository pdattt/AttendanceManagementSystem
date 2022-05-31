using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.CardDTOs
{
    public class CardReadDTO
    {
        public string CardId { get; set; }

        public bool Status { get; set; }
    }
}