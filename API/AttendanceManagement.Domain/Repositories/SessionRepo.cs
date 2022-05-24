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
        private FirestoreDb db;

        public SessionRepo()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"attendancerfid.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("attendancerfid-a6f84");
        }

        public List<Session> GetSessionsByEventClassId(string id)
        {
            Query qref = db.Collection("Session");
            QuerySnapshot snap = qref.GetSnapshotAsync().Result;
            var sessions = new List<Session>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                string docId = docsnap.Id;
                CollectionReference subCol = db.Collection("Session").Document(docId).Collection(id);
                //CollectionReference subSnap = subDoc;

                foreach (var doc in subCol.GetSnapshotAsync().Result)
                {
                    Session session = doc.ConvertTo<Session>();
                    if (session.ClassId == id || session.EventId == id)
                        sessions.Add(session);
                }
            }

            return sessions;
        }

        public List<Session> GetSessionsByEventClassId(int semesterId, string eve_class_Id)
        {
            Query qref = db.Collection("Session");
            QuerySnapshot snap = qref.GetSnapshotAsync().Result;
            var sessions = new List<Session>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Id == semesterId.ToString())
                {
                    string docId = docsnap.Id;
                    CollectionReference subCol = db.Collection("Session").Document(docId).Collection(eve_class_Id);

                    foreach (var doc in subCol.GetSnapshotAsync().Result)
                    {
                        Session session = doc.ConvertTo<Session>();
                        if (session.ClassId == eve_class_Id || session.EventId == eve_class_Id)
                            sessions.Add(session);
                    }

                    break;
                }
            }

            return sessions;
        }

        public List<Session> GetSessionsBySemesterId(int id)
        {
            throw new NotImplementedException();
        }
    }
}