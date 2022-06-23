using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Constants
{
    public static class Constants
    {
        // Setting Json
        public static string Json_AppSettings = "appsettings.json";

        // SQL Database
        public static string ConnectionString = "AMSConnectionString";

        // Firestore Config

        public static string Json_ApiKey_Directory = "D:\\VS\\AttendanceManagementSystem\\AttendanceManagementSystem\\API\\AttendanceManagement.Domain\\attendancerfid-a6f84-e951217ecbda.json";
        public static string EnviromentVariable = "GOOGLE_APPLICATION_CREDENTIALS";
        public static string ProjectId = "attendancerfid-a6f84";

        // Firestore Fields

        public static string Collection_Session = "Session";
        public static string Collection_Attendance = "Attendance";
        public static string Collection_CheckIn = "CheckIn";

        public static string Session_Type_Event = "event";
        public static string Session_Type_Class = "class";
    }
}