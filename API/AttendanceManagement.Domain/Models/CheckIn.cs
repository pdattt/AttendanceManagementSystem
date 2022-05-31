using Google.Cloud.Firestore;

namespace AttendanceManagement.Domain.Models
{
    [FirestoreData]
    public class CheckIn
    {
        [FirestoreProperty]
        public string CardId { get; set; }

        [FirestoreProperty]
        public string Time { get; set; }

        public string AttendeeName { get; set; }
    }
}