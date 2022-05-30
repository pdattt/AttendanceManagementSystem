using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Repositories
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        public UserRepo(AttendanceManagementDBContext context) : base(context)
        {
        }

        public User GetUser(string username, string password)
        {
            return Query().FirstOrDefaultAsync(u => u.Username == username && u.Password == password).Result;
        }
    }
}