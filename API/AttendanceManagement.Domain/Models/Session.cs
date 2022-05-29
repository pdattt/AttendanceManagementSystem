using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Models
{
    [FirestoreData]
    public class Session
    {
        [FirestoreProperty]
        public string Date { get; set; }

        [FirestoreProperty]
        public string EndTime { get; set; }

        [FirestoreProperty]
        public string StartTime { get; set; }

        [FirestoreProperty]
        public string Location { get; set; }
    }
}