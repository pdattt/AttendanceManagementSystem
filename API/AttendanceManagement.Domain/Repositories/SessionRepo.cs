using AttendanceManagement.Domain.Interfaces.IRepos;
using AttendanceManagement.Domain.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain.Repositories
{
    public class SessionRepo : ISessionRepo
    {
        private readonly string dir = "D:\\VS\\AttendanceManagementSystem\\AttendanceManagementSystem\\API\\AttendanceManagement.Domain\\attendancerfid-a6f84-e951217ecbda.json";
        private readonly string projectId;
        private FirestoreDb db;

        public SessionRepo()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", dir);
            projectId = "attendancerfid-a6f84";
            db = FirestoreDb.Create(projectId);
        }

        public async void Add(List<Session> sessions, string type, string semesterId, int cls_eve_id, string location, TimeSpan startTime, TimeSpan endTime)
        {
            Query qref = db.Collection("Session").Document(semesterId).Collection(type);
            QuerySnapshot snap = qref.GetSnapshotAsync().Result;

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Id == cls_eve_id.ToString())
                    return;
            }

            DocumentReference doc = db.Collection("Session").Document(semesterId).Collection(type).Document(cls_eve_id.ToString());

            Dictionary<string, object> data = new Dictionary<string, object> {
                { "Location", location },
                { "StartTime", startTime.ToString() },
                { "EndTime", endTime.ToString() }
            };

            doc.SetAsync(data);

            CollectionReference col = doc.Collection("Attendace");

            foreach (var session in sessions)
            {
                Dictionary<string, object> map = new Dictionary<string, object> {
                    { "Date", session.Date },
                    { "Time", session.Time }
                };

                await col.AddAsync(map);
            }
        }
    }
}