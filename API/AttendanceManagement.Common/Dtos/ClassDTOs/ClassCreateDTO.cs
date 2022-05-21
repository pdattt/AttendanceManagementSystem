using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.ClassDTOs
{
    public class ClassCreateDTO
    {
        [Required]
        public string ClassName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ClassStartTime { get; set; }

        [Required]
        public string ClassEndTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ClassDateStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ClassDateEnd { get; set; }

        [Required]
        public string DaysOfWeek { get; set; }
    }
}