using AttendanceManagement.Common.Constants;
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
        private readonly string dir = Constants.Json_ApiKey_Directory;
        private readonly string projectId;
        private FirestoreDb db;

        public SessionRepo()
        {
            Environment.SetEnvironmentVariable(Constants.EnviromentVariable, dir);
            projectId = Constants.ProjectId;
            db = FirestoreDb.Create(projectId);
        }

        public async void Add(List<Session> sessions, string name, string type, string semesterId,
            int cls_eve_id, string location, TimeSpan startTime, TimeSpan endTime)
        {
            DocumentReference d = db.Collection(Constants.Collection_Session).Document(semesterId);
            Dictionary<string, string> yearSet = new Dictionary<string, string>
            {
                { "Year", semesterId.Substring(0, 4) }
            };

            d.SetAsync(yearSet);

            Query qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type);
            QuerySnapshot snap = qref.GetSnapshotAsync().Result;

            DocumentReference doc = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type).Document(cls_eve_id.ToString());

            Dictionary<string, object> data = new Dictionary<string, object> {
                { "Name", name},
                { "Number of sessions", sessions.Count }
            };

            doc.SetAsync(data);

            foreach (var session in sessions)
            {
                Query subqref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type);
                QuerySnapshot subsnap = subqref.GetSnapshotAsync().Result;

                bool existDate = false;

                foreach (DocumentSnapshot subdoc in subsnap)
                {
                    var ses = subdoc.ConvertTo<Session>();

                    if (ses.Date == session.Date)
                    {
                        existDate = true;
                        break;
                    }
                }

                if (existDate)
                    continue;

                Dictionary<string, object> map = new Dictionary<string, object> {
                    { "Date", session.Date },
                    { "Location", location },
                    { "StartTime", startTime.ToString() },
                    { "EndTime", endTime.ToString() }
                };

                CollectionReference col = doc.Collection(Constants.Collection_Attendance);
                await col.AddAsync(session);
            }
        }

        public async Task<string> CheckIn(string semesterId, string type, DateTime getDate, string cardId, string location)
        {
            Query qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (var docsnap in snap)
            {
                Query sub_qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type)
                    .Document(docsnap.Id).Collection(Constants.Collection_Attendance);
                QuerySnapshot sub_snap = await sub_qref.GetSnapshotAsync();

                foreach (var sub_docsnap in sub_snap)
                {
                    Session session = sub_docsnap.ConvertTo<Session>();

                    string dateNow = getDate.ToString("d");

                    if (dateNow != session.Date)
                        continue;

                    if (location != session.Location)
                        continue;

                    TimeSpan time = getDate.TimeOfDay;
                    TimeSpan startTime = TimeSpan.Parse(session.StartTime);
                    TimeSpan endTime = TimeSpan.Parse(session.EndTime);

                    if (time >= startTime.Subtract(new TimeSpan(0, 30, 0)) && time <= endTime)
                    {
                        Query checkin_qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type)
                            .Document(docsnap.Id).Collection(Constants.Collection_Attendance).Document(sub_docsnap.Id).Collection(Constants.Collection_CheckIn);
                        QuerySnapshot checkin_snap = await checkin_qref.GetSnapshotAsync();

                        foreach (var check in checkin_snap)
                        {
                            CheckIn checkin = check.ConvertTo<CheckIn>();
                            if (checkin.CardId == cardId)
                                return null;
                        }

                        Dictionary<string, string> map = new Dictionary<string, string>()
                        {
                            { "CardId", cardId },
                            { "Time", time.ToString("hh':'mm':'ss") }
                        };

                        CollectionReference col = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type)
                            .Document(docsnap.Id).Collection(Constants.Collection_Attendance).Document(sub_docsnap.Id).Collection(Constants.Collection_CheckIn);

                        col.AddAsync(map);
                        return docsnap.Id;
                    }
                }
            }

            return null;
        }

        public async Task<List<Session>> GetAllAttendanceSession(string semesterId, string type, string cls_eve_id)
        {
            Query qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type)
                .Document(cls_eve_id).Collection(Constants.Collection_Attendance);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            List<Session> sessions = new List<Session>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                Session session = docsnap.ConvertTo<Session>();
                sessions.Add(session);
            }

            sessions = sessions.OrderBy(session => DateTime.Parse(session.Date)).ToList();

            return sessions;
        }

        public async Task<List<CheckIn>> GetAllCheckInsInSession(string semesterId, string type, string cls_eve_id, string date)
        {
            Query qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type).Document(cls_eve_id)
                                           .Collection(Constants.Collection_Attendance);
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

            qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type)
                                           .Document(cls_eve_id).Collection(Constants.Collection_Attendance)
                                           .Document(docId).Collection(Constants.Collection_CheckIn);
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

            Query qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type);
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
            Query qref = db.Collection(Constants.Collection_Session);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            List<string> semesterIds = new List<string>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                semesterIds.Add(docsnap.Id);
            }

            return semesterIds;
        }

        public async Task<List<string>> GetAllSessionDates(string semesterId, string type, string cls_eve_id)
        {
            Query qref = db.Collection(Constants.Collection_Session).Document(semesterId).Collection(type)
                .Document(cls_eve_id).Collection(Constants.Collection_Attendance);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            List<string> sessionDates = new List<string>();

            foreach (DocumentSnapshot doc in snap)
            {
                Session session = doc.ConvertTo<Session>();
                sessionDates.Add(session.Date);
            }

            return sessionDates;
        }

        public async Task<List<string>> GetAllTypes(string semesterId)
        {
            List<string> types = new List<string>();

            List<CollectionReference> list = db.Collection(Constants.Collection_Session).Document(semesterId).ListCollectionsAsync().ToListAsync().Result;

            foreach (var col in list)
            {
                string type = col.Id;
                types.Add(type);
            }

            return types;
        }
    }
}