using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    [FirestoreData]
    public class CheckIn
    {
        [FirestoreProperty]
        public string CardId { get; set; }

        [FirestoreProperty]
        public string Time { get; set; }
    }
}