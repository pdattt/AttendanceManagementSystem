using AttendanceManagement.Common.Dtos.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Interfaces.IServices
{
    public interface IUserService
    {
        UserReadDTO Authentication(UserLoginDTO userLogin);
    }
}