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

        public async void Add(List<Session> sessions, string name, string type, string semesterId, int cls_eve_id, string location, TimeSpan startTime, TimeSpan endTime)
        {
            DocumentReference d = db.Collection("Session").Document(semesterId);
            Dictionary<string, string> yearSet = new Dictionary<string, string>
            {
                { "Year", semesterId.Substring(0, 4) }
            };

            d.SetAsync(yearSet);

            Query qref = db.Collection("Session").Document(semesterId).Collection(type);
            QuerySnapshot snap = qref.GetSnapshotAsync().Result;

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Id == cls_eve_id.ToString())
                    return;
            }

            DocumentReference doc = db.Collection("Session").Document(semesterId).Collection(type).Document(cls_eve_id.ToString());

            Dictionary<string, object> data = new Dictionary<string, object> {
                { "Name", name},
                { "Number of sessions", sessions.Count }
            };

            doc.SetAsync(data);

            CollectionReference col = doc.Collection("Attendance");

            foreach (var session in sessions)
            {
                Dictionary<string, object> map = new Dictionary<string, object> {
                    { "Date", session.Date },
                    { "Location", location },
                    { "StartTime", startTime.ToString() },
                    { "EndTime", endTime.ToString() }
                };

                await col.AddAsync(map);
            }
        }

        public async Task<List<Session>> GetAllAttendanceSession(string semesterId, string type, string cls_eve_id)
        {
            Query qref = db.Collection("Session").Document(semesterId).Collection(type).Document(cls_eve_id).Collection("Attendance");
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            List<Session> sessions = new List<Session>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                Session session = docsnap.ConvertTo<Session>();
                sessions.Add(session);
            }

            return sessions;
        }

        public async Task<List<CheckIn>> GetAllCheckInsInSession(string semesterId, string type, string cls_eve_id, string date)
        {
            Query qref = db.Collection("Session").Document(semesterId).Collection(type).Document(cls_eve_id).Collection("Attendance");
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            string docId = "";

            foreach (DocumentSnapshot docsnap in snap)
            {
                Session session = docsnap.ConvertTo<Session>();
                if (session.Date == date)
                {
                    docId = docsnap.Id;
                    break;
                }
            }

            qref = db.Collection("Session").Document(semesterId).Collection(type)
                                           .Document(cls_eve_id).Collection("Attendance").Document(docId).Collection("CheckIn");
            snap = await qref.GetSnapshotAsync();

            List<CheckIn> list = new List<CheckIn>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                CheckIn check = docsnap.ConvertTo<CheckIn>();
                list.Add(check);
            }

            return list;
        }

        public async Task<List<string>> GetAllInSemester(string semesterId, string type)
        {
            if (semesterId == null)
                return null;

            Query qref = db.Collection("Session").Document(semesterId).Collection(type);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            List<string> cls_eve_Ids = new List<string>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                cls_eve_Ids.Add(docsnap.Id);
            }

            return cls_eve_Ids;
        }

        public async Task<List<string>> GetAllSemesterIds()
        {
            Query qref = db.Collection("Session");
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            List<string> semesterIds = new List<string>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                semesterIds.Add(docsnap.Id);
            }

            return semesterIds;
        }
    }
}