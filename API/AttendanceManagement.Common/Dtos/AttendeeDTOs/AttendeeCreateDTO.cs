using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Dtos.AttendeeDTOs
{
    public class AttendeeCreateDTO
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}