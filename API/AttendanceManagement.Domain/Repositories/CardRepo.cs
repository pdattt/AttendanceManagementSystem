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
    public class CardRepo : Repository<Card>, ICardRepo
    {
        public CardRepo(AttendanceManagementDBContext context) : base(context)
        {
        }

        public Card GetCardById(string id)
        {
            return Query().FirstOrDefaultAsync(card => card.CardId == id).Result;
        }
    }
}